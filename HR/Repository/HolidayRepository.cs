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
using System.Data;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            SelectbyDate = 6,
            SelectbyMonth = 7

        }

        public HolidayRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        //public async Task Insert(Holiday holiday )
        //{
        //    try
        //    {
        //       await _dbContext.AddAsync(holiday);
        //       await _dbContext.SaveChangesAsync ();

        //        if (holiday.Id > 0)
        //        {
        //            UserLogs userLogs = new UserLogs();
        //            userLogs.MasterId = holiday.Id;
        //            userLogs.UserId = Convert.ToInt16(holiday.UserId);
        //            userLogs.FormName = "HOLIDAY SETTING";
        //            userLogs.EntryDate = DateTime.Now;
        //            userLogs.Action = Convert.ToInt32(1);
        //            await _dbContext.AddAsync(userLogs);
        //            await _dbContext.SaveChangesAsync();
        //        }

        //    }
        //    catch (Exception)
        //    { throw; }
        //}
        //public async Task Update(Holiday holiday)
        //{
        //    try
        //    {
        //        _dbContext.Entry(holiday).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();

        //    if (holiday.Id > 0)
        //    {
        //        UserLogs userLogs = new UserLogs();
        //        userLogs.MasterId = holiday.Id;
        //        userLogs.UserId = Convert.ToInt16(holiday.UserId);
        //        userLogs.FormName = "HOLIDAY SETTING";
        //        userLogs.EntryDate = DateTime.Now;
        //        userLogs.Action = Convert.ToInt32(2);
        //        await _dbContext.AddAsync(userLogs);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    }
        //    catch (Exception)
        //    { throw; }
        //}
        //public async Task Delete(int id,int userid)
        //{
        //    try
        //    {
        //        var employeeDepartment =await _dbContext.tbl_Holiday.FindAsync(id);

        //        if (employeeDepartment != null)
        //        {
        //            _dbContext.tbl_Holiday.Remove(employeeDepartment);
        //           await _dbContext.SaveChangesAsync();

        //            UserLogs userLogs = new UserLogs();
        //            userLogs.MasterId = id;
        //            userLogs.UserId = Convert.ToInt16(userid);
        //            userLogs.FormName = "HOLIDAY SETTING";
        //            userLogs.EntryDate = DateTime.Now;
        //            userLogs.Action = Convert.ToInt32(3);
        //            await _dbContext.AddAsync(userLogs);
        //            await _dbContext.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception)
        //    { throw; }
        //}

        public async Task<IEnumerable<Validation>> Insert(Holiday holidays)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var holiday = new SqlParameter("@holiday", holidays.date);
                var FinancialYearId = new SqlParameter("@FinancialYearId", holidays.FinancialYearId);
                var description = new SqlParameter("@description", holidays.Description);

                var CompanyId = new SqlParameter("@CompanyId", holidays.CompanyId);
                var BranchId = new SqlParameter("@BranchId", holidays.BranchId);
                var UserId = new SqlParameter("@UserId", holidays.UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int userid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", id);
                var holiday = new SqlParameter("@holiday", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var description = new SqlParameter("@description", "0");

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(Holiday holidays)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", holidays.Id);
                var holiday = new SqlParameter("@holiday", holidays.date);
                var FinancialYearId = new SqlParameter("@FinancialYearId", holidays.FinancialYearId);
                var description = new SqlParameter("@description", holidays.Description);

                var CompanyId = new SqlParameter("@CompanyId", holidays.CompanyId);
                var BranchId = new SqlParameter("@BranchId", holidays.BranchId);
                var UserId = new SqlParameter("@UserId", holidays.UserId);
                var Action = new SqlParameter("@Action", Actions.Update);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Holiday>> Get(int compid, int branchid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");

                var holiday = new SqlParameter("@holiday", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var description = new SqlParameter("@description", "0");

                var CompanyId = new SqlParameter("@CompanyId", compid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);

                var _product = await _dbContext.tbl_Holiday.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Holiday>> GetbyDate(int companyid, int branchId,DateTime date)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");

                var holiday = new SqlParameter("@holiday", date);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var description = new SqlParameter("@description", "0");

                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectbyDate);

                var purchaseList = await _dbContext.tbl_Holiday.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Holiday>> Get(int compid, int branchid,int monthId,int Financialyearid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");

                var holiday = new SqlParameter("@holiday", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", Financialyearid);
                var description = new SqlParameter("@description", "0");

                var CompanyId = new SqlParameter("@CompanyId", compid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", monthId);
                var Action = new SqlParameter("@Action", Actions.SelectbyMonth);

                var _product = await _dbContext.tbl_Holiday.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Holiday>> GetbyID(int Id)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);

                var holiday = new SqlParameter("@holiday", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var description = new SqlParameter("@description", "0");

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);

                var purchaseList = await _dbContext.tbl_Holiday.FromSqlRaw("Stpro_Holiday @materialId,@holiday,@FinancialYearId,@description, @CompanyId, @BranchId,@UserId, @Action", materialId, holiday, FinancialYearId, description, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(DateTime date, int branchId)
        {
            try
            {
                var Date = new SqlParameter("@Date",date);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckHolidaySettingsEditDelete @Date, @BranchId", Date, BranchId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
