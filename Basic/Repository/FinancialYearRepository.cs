using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly BasicContext _dbContext;

        public FinancialYearRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            
            LogOutValidation =5,
            CreateNewValidation = 6,
            SetActive = 7,
            SetClosed = 8
        }
        public async Task DeleteFinancilaYear(int financialYearId)
        {
            try
            {
                var FinancialYear = await _dbContext.tbl_FinancialYear.FindAsync(financialYearId);

                _dbContext.tbl_FinancialYear.Remove(FinancialYear);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFinancilaYearByID(int financialYearId)
        {
            try
            {
                var result = await (from a in _dbContext.tbl_FinancialYear
                                    join b in _dbContext.tbl_Companies on a.CompanyId equals b.CompanyId
                                    where a.FinancialYearId == financialYearId
                                    select new FinancialYear
                                    {
                                        FinancialYearId = a.FinancialYearId,
                                        Financial_Year = a.Financial_Year,
                                        BranchId = a.BranchId,
                                        CompanyId = a.CompanyId,
                                        Status = a.Status,
                                        Active = a.Active,
                                        start_date = a.start_date,
                                        end_date = a.end_date,
                                        YearNo = a.YearNo,
                                        TaxType = b.TaxType
                                    }).ToListAsync();

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<FinancialYear>> Get(int CompanyId, int branchId)
        {
            try
            {
                var result = await (from a in _dbContext.tbl_FinancialYear
                                    join b in _dbContext.tbl_Companies on a.CompanyId equals b.CompanyId
                                    where a.CompanyId == CompanyId && a.BranchId == branchId
                                    select new FinancialYear
                                    {
                                        FinancialYearId = a.FinancialYearId,
                                        Financial_Year = a.Financial_Year,
                                        BranchId = a.BranchId,
                                        CompanyId = a.CompanyId,
                                        Status = a.Status,
                                        Active = a.Active,
                                        start_date = a.start_date,
                                        end_date = a.end_date,
                                        YearNo = a.YearNo,
                                        TaxType = b.TaxType
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<FinancialYear>> GetActiveFinancialYear(int CompanyId, int BranchId)
        {
            try
            {
                var result = await (from a in _dbContext.tbl_FinancialYear
                              join b in _dbContext.tbl_Companies on a.CompanyId equals b.CompanyId
                              where a.CompanyId == CompanyId && a.BranchId == BranchId && a.Status == "ACTIVE"
                              select new FinancialYear
                              {
                                  FinancialYearId = a.FinancialYearId,
                                  Financial_Year = a.Financial_Year,
                                  BranchId = a.BranchId,
                                  CompanyId = a.CompanyId,
                                  Status = a.Status,
                                  Active = a.Active,
                                  start_date = a.start_date,
                                  end_date = a.end_date,
                                  YearNo = a.YearNo,
                                  TaxType = b.TaxType
                              }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<FinancialYear>> GetFinancilaYear()
        {
            try
            {
                return await (from a in _dbContext.tbl_FinancialYear
                                    join b in _dbContext.tbl_Companies on a.CompanyId equals b.CompanyId
                                    select new FinancialYear
                                    {
                                        FinancialYearId = a.FinancialYearId,
                                        Financial_Year = a.Financial_Year,
                                        BranchId = a.BranchId,
                                        CompanyId = a.CompanyId,
                                        Status = a.Status,
                                        Active = a.Active,
                                        start_date = a.start_date,
                                        end_date = a.end_date,
                                        YearNo = a.YearNo,
                                        TaxType = b.TaxType
                                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //
        public async Task<IEnumerable<Validation>> getvalidation1(FinancialYear financialyear)
        {
            try
            {


                var CompanyId = new SqlParameter("@CompanyId", financialyear.CompanyId);
                var BranchId = new SqlParameter("@BranchId", financialyear.BranchId);
                var FinYearId = new SqlParameter("@FinancialYearId", financialyear.FinancialYearId);
                var FinYear = new SqlParameter("@FinYear", financialyear.Financial_Year);
                var Status = new SqlParameter("@Stat", financialyear.Status);
                var action = new SqlParameter("@Action", 1);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_FinancialYearValidation  @CompanyId,@BranchId,@FinancialYearId,@FinYear,@Stat, @Action", CompanyId, BranchId, FinYearId, FinYear, Status, action).ToListAsync();


                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> getvalidation2(FinancialYear financialyear)
        {
            try
            {


                var CompanyId = new SqlParameter("@CompanyId", financialyear.CompanyId);
                var BranchId = new SqlParameter("@BranchId", financialyear.BranchId);
                var FinYearId = new SqlParameter("@FinancialYearId", financialyear.FinancialYearId);
                var FinYear = new SqlParameter("@FinYear", financialyear.Financial_Year);
                var Status = new SqlParameter("@Stat", financialyear.Status);
                var action = new SqlParameter("@Action", 2);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_FinancialYearValidation  @CompanyId,@BranchId,@FinancialYearId,@FinYear,@Stat, @Action", CompanyId, BranchId, FinYearId, FinYear, Status, action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Getregeneratebalance(int companyid, int branchid, int financialyearid)
        {
            try
            {


                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var FinYearId = new SqlParameter("@FinancialYearId", financialyearid);
                var Action = new SqlParameter("@Action", 2);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GenerateClosingBalance  @CompanyId,@BranchId,@FinancialYearId, @Action", CompanyId, BranchId, FinYearId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        //

        public async Task<IEnumerable<Validation>> InsertFinancilaYear(FinancialYear financialYear)
        {
            try
            {
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Financial_Year = new SqlParameter("@Financial_Year", financialYear.Financial_Year);
                var CompanyId = new SqlParameter("@CompanyId", financialYear.CompanyId);
                var BranchId = new SqlParameter("@BranchId", financialYear.BranchId);
                var Active = new SqlParameter("@Active", "Y");
                var Status = new SqlParameter("@Status", financialYear.Status);
                var start_date = new SqlParameter("@start_date", financialYear.start_date);
                var end_date = new SqlParameter("@end_date", financialYear.end_date);
                var Action = new SqlParameter("@Action", Actions.Insert);
               var _product= await _dbContext.tbl_validation.FromSqlRaw("Stpro_FinancialYear @FinancialYearId,@Financial_Year,@CompanyId,@BranchId,@Active,@Status,@start_date,@end_date,@Action", FinancialYearId, Financial_Year, CompanyId, BranchId, Active, Status, start_date, end_date, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task UpdateFinancilaYear(FinancialYear financialYear)
        {
            try
            {
                var FinancialYearId = new SqlParameter("@FinancialYearId", financialYear.FinancialYearId);
                var Financial_Year = new SqlParameter("@Financial_Year", financialYear.Financial_Year);
                var CompanyId = new SqlParameter("@CompanyId", financialYear.CompanyId);
                var BranchId = new SqlParameter("@BranchId", financialYear.BranchId);
                var Active = new SqlParameter("@Active", financialYear.Active);
                var Status = new SqlParameter("@Status", financialYear.Status);
                var start_date = new SqlParameter("@start_date", financialYear.start_date);
                var end_date = new SqlParameter("@end_date", financialYear.end_date);
                var Action = new SqlParameter("@Action", 2);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_FinancialYear @FinancialYearId,@Financial_Year,@CompanyId,@BranchId,@Active,@Status,@start_date,@end_date,@Action", FinancialYearId, Financial_Year, CompanyId, BranchId, Active, Status, start_date, end_date, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            //
        }


        #region Financial Year Process

        public async Task<IEnumerable<Validation>> FinancialYearStatusChange(int companyid, int branchid, int financialYearId, int type)
        {
            try
            {
                var FinancialYearId = new SqlParameter("@FinancialYearId", financialYearId);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = type == 0 ? new SqlParameter("@Action", Actions.SetActive) : new SqlParameter("@Action", Actions.SetClosed);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_FinancialYearStatusUpdate @FinancialYearId, @CompanyId,@BranchId,@Action", FinancialYearId, CompanyId, BranchId, Action).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> FinancialYearValidation(int companyid, int branchid, int type)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var FinYearId = new SqlParameter("@FinancialYearId", "0");
                var FinYear = new SqlParameter("@FinYear", "");
                var Status = new SqlParameter("@Stat", "");
                var action = type == 0 ? new SqlParameter("@Action", Actions.LogOutValidation): new SqlParameter("@Action", Actions.CreateNewValidation);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_FinancialYearValidation  @CompanyId,@BranchId,@FinancialYearId,@FinYear,@Stat, @Action", CompanyId, BranchId, FinYearId, FinYear, Status, action).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        #endregion
    }
}
