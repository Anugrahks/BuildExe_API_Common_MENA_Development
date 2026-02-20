using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace BuildExeHR.Repository
{
    public class SalaryItemHeadRepository:ISalaryItemHeadRepository 
    {
        private readonly HRContext _dbContext;

        public SalaryItemHeadRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            Selectvaryinghead = 6,
            selectnotvarying = 7,
            selectFacilityHeads = 9
        }
        public async Task<IEnumerable<Validation>> Insert(SalaryItemHead salaryItemHead)
        {
            try
            {
                if (salaryItemHead.Remarks == null)
                    salaryItemHead.Remarks = "";
                if (salaryItemHead.UpperLimit == null)
                    salaryItemHead.UpperLimit = 0;
                if (salaryItemHead.EmployerContribution == null)
                    salaryItemHead.EmployerContribution = 0;
                var Id = new SqlParameter("@Id", "0");
                var AccountHeadid = new SqlParameter("@AccountHeadid", salaryItemHead.AccountHeadid);
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", salaryItemHead.SalaryHeadTypeId);
                var HeadName = new SqlParameter("@HeadName", salaryItemHead.HeadName);
                var CalculateOn = new SqlParameter("@CalculateOn", salaryItemHead.CalculateOn);

                var Active = new SqlParameter("@Active", salaryItemHead.Active);
                var Remarks = new SqlParameter("@Remarks", salaryItemHead.Remarks);
                var CalculationMode = new SqlParameter("@CalculationMode", salaryItemHead.CalculationMode);
                var VaryingHead = new SqlParameter("@VaryingHead", salaryItemHead.VaryingHead);
                var UpperLimit = new SqlParameter("@UpperLimit", salaryItemHead.UpperLimit);
                var DeductLeave = new SqlParameter("@DeductLeave", salaryItemHead.DeductLeave);

                var CompanyId = new SqlParameter("@CompanyId", salaryItemHead.CompanyId);
                var BranchId = new SqlParameter("@BranchId", salaryItemHead.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", salaryItemHead.FinancialYearId);
                var UserId = new SqlParameter("@UserId", salaryItemHead.UserId);
                var employercontribution = new SqlParameter("@EmployerContribution", salaryItemHead.EmployerContribution);
                var employercontributiontype = new SqlParameter("@EmployerContributionType", salaryItemHead.EmployerContributionType);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(salaryItemHead));
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(SalaryItemHead salaryItemHead  )
        {
            try
            {
                if (salaryItemHead.Remarks == null)
                    salaryItemHead.Remarks = "";
                if (salaryItemHead.UpperLimit == null)
                    salaryItemHead.UpperLimit = 0;
                if (salaryItemHead.EmployerContribution == null)
                    salaryItemHead.EmployerContribution = 0;
                var Id = new SqlParameter("@Id", salaryItemHead.Id);
                var AccountHeadid = new SqlParameter("@AccountHeadid", salaryItemHead.AccountHeadid);
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", salaryItemHead.SalaryHeadTypeId);
                var HeadName = new SqlParameter("@HeadName", salaryItemHead.HeadName);
                var CalculateOn = new SqlParameter("@CalculateOn", salaryItemHead.CalculateOn);

                var Active = new SqlParameter("@Active", salaryItemHead.Active);
                var Remarks = new SqlParameter("@Remarks", salaryItemHead.Remarks);
                var CalculationMode = new SqlParameter("@CalculationMode", salaryItemHead.CalculationMode);
                var VaryingHead = new SqlParameter("@VaryingHead", salaryItemHead.VaryingHead);
                var UpperLimit = new SqlParameter("@UpperLimit", salaryItemHead.UpperLimit);
                var DeductLeave = new SqlParameter("@DeductLeave", salaryItemHead.DeductLeave);

                var CompanyId = new SqlParameter("@CompanyId", salaryItemHead.CompanyId);
                var BranchId = new SqlParameter("@BranchId", salaryItemHead.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", salaryItemHead.FinancialYearId);
                var UserId = new SqlParameter("@UserId", salaryItemHead.UserId);
                var employercontribution = new SqlParameter("@EmployerContribution", salaryItemHead.EmployerContribution);
                var employercontributiontype = new SqlParameter("@EmployerContributionType", salaryItemHead.EmployerContributionType);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(salaryItemHead));
                var Action = new SqlParameter("@Action", Actions.Update);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryItemHead >> Get(int companyid,int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

            var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
            var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId","0");
            var HeadName = new SqlParameter("@HeadName", "0");
            var CalculateOn = new SqlParameter("@CalculateOn", "0");

            var Active = new SqlParameter("@Active", "0");
            var Remarks = new SqlParameter("@Remarks", "0");
            var CalculationMode = new SqlParameter("@CalculationMode", "0");
            var VaryingHead = new SqlParameter("@VaryingHead", "0");
            var UpperLimit = new SqlParameter("@UpperLimit", "0");
            var DeductLeave = new SqlParameter("@DeductLeave", "0");

            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.SelectAll);


            var _product =await _dbContext.tbl_SalaryItemHead.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();

            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryItemHead>> NotVarying(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

                var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", "0");
                var HeadName = new SqlParameter("@HeadName", "0");
                var CalculateOn = new SqlParameter("@CalculateOn", "0");

                var Active = new SqlParameter("@Active", "0");
                var Remarks = new SqlParameter("@Remarks", "0");
                var CalculationMode = new SqlParameter("@CalculationMode", "0");
                var VaryingHead = new SqlParameter("@VaryingHead", "0");
                var UpperLimit = new SqlParameter("@UpperLimit", "0");
                var DeductLeave = new SqlParameter("@DeductLeave", "0");

                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);

                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.selectnotvarying);


                var _product = await _dbContext.tbl_SalaryItemHead.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryItemHead>> FacilityHeads(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

                var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", "0");
                var HeadName = new SqlParameter("@HeadName", "0");
                var CalculateOn = new SqlParameter("@CalculateOn", "0");

                var Active = new SqlParameter("@Active", "0");
                var Remarks = new SqlParameter("@Remarks", "0");
                var CalculationMode = new SqlParameter("@CalculationMode", "0");
                var VaryingHead = new SqlParameter("@VaryingHead", "0");
                var UpperLimit = new SqlParameter("@UpperLimit", "0");
                var DeductLeave = new SqlParameter("@DeductLeave", "0");

                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);

                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.selectFacilityHeads);


                var _product = await _dbContext.tbl_SalaryItemHead.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution, employercontributiontype, json, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        


        public async Task<IEnumerable<SalaryItemHead>> Getvaryinghead(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

                var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", "0");
                var HeadName = new SqlParameter("@HeadName", "0");
                var CalculateOn = new SqlParameter("@CalculateOn", "0");

                var Active = new SqlParameter("@Active", "0");
                var Remarks = new SqlParameter("@Remarks", "0");
                var CalculationMode = new SqlParameter("@CalculationMode", "0");
                var VaryingHead = new SqlParameter("@VaryingHead", "0");
                var UpperLimit = new SqlParameter("@UpperLimit", "0");
                var DeductLeave = new SqlParameter("@DeductLeave", "0");

                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);

                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.Selectvaryinghead);


                var _product = await _dbContext.tbl_SalaryItemHead.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryItemHead >> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);

            var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
            var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", "0");
            var HeadName = new SqlParameter("@HeadName", "0");
            var CalculateOn = new SqlParameter("@CalculateOn", "0");

            var Active = new SqlParameter("@Active", "0");
            var Remarks = new SqlParameter("@Remarks", "0");
            var CalculationMode = new SqlParameter("@CalculationMode", "0");
            var VaryingHead = new SqlParameter("@VaryingHead", "0");
            var UpperLimit = new SqlParameter("@UpperLimit", "0");
            var DeductLeave = new SqlParameter("@DeductLeave", "0");

            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
           
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.Select);

            var _product = await _dbContext.tbl_SalaryItemHead.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Validation>> Delete(int id, int userId)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);

                var AccountHeadid = new SqlParameter("@AccountHeadid", "0");
                var SalaryHeadTypeId = new SqlParameter("@SalaryHeadTypeId", "0");
                var HeadName = new SqlParameter("@HeadName", "0");
                var CalculateOn = new SqlParameter("@CalculateOn", "0");

                var Active = new SqlParameter("@Active", "0");
                var Remarks = new SqlParameter("@Remarks", "0");
                var CalculationMode = new SqlParameter("@CalculationMode", "0");
                var VaryingHead = new SqlParameter("@VaryingHead", "0");
                var UpperLimit = new SqlParameter("@UpperLimit", "0");
                var DeductLeave = new SqlParameter("@DeductLeave", "0");

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");

                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var employercontribution = new SqlParameter("@EmployerContribution", "0");
                var employercontributiontype = new SqlParameter("@EmployerContributionType", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_SalaryItemHead @Id, @AccountHeadid, @SalaryHeadTypeId, @HeadName, @CalculateOn, @Active, @Remarks, @CalculationMode, @VaryingHead, @UpperLimit, @DeductLeave, @CompanyId, @BranchId, @FinancialYearId,@UserId,@EmployerContribution,@EmployerContributionType,@json,@Action", Id, AccountHeadid, SalaryHeadTypeId, HeadName, CalculateOn, Active, Remarks, CalculationMode, VaryingHead, UpperLimit, DeductLeave, CompanyId, BranchId, FinancialYearId, UserId, employercontribution,employercontributiontype, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSalaryItemHeadEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetdetailsbyID(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_SalaryItemHead";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@AccountHeadid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SalaryHeadTypeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@HeadName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CalculateOn", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CalculationMode", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@VaryingHead", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UpperLimit", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DeductLeave", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@EmployerContribution", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@EmployerContributionType", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
