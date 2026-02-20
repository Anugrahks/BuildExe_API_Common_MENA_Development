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
using System.Data.Common;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class HolidayLeaveRepository:IHolidayLeaveRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            getforedit = 4,
            get = 5

        }

        public HolidayLeaveRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<HolidayLeave> holidayLeaves)
        {
            try
            {
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId ", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(holidayLeaves));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_HolidayLeave @EmployeeId,@MonthId,@FinancialYearId ,@item, @CompanyId, @BranchId,@UserId, @Action", EmployeeId,MonthId, FinancialYearId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<HolidayLeave> holidayLeaves)
        {
            try
            {
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId ", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(holidayLeaves));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_HolidayLeave  @EmployeeId,@MonthId,@FinancialYearId ,@item, @CompanyId, @BranchId,@UserId, @Action", EmployeeId,MonthId, FinancialYearId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int employeeId,int monthid,int finid,int userid)
        {
            try
            {
                var EmployeeId = new SqlParameter("@EmployeeId", employeeId);
                var MonthId = new SqlParameter("@MonthId", monthid);
                var FinancialYearId = new SqlParameter("@FinancialYearId ", finid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId","0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_HolidayLeave @EmployeeId,@MonthId,@FinancialYearId ,@item, @CompanyId, @BranchId,@UserId, @Action", EmployeeId,MonthId, FinancialYearId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<HolidayLeaveList>> GetForEdit(int Companyid, int Branchid, int finid)
        {
            try
            {
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId ", finid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.getforedit);
                var purchaseList = await _dbContext.tbl_HolidayLeavelist.FromSqlRaw("Stpro_HolidayLeave @EmployeeId,@MonthId,@FinancialYearId ,@item, @CompanyId, @BranchId,@UserId, @Action", EmployeeId, MonthId, FinancialYearId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<HolidayLeave>> Get(int employeeId, int Month, int finid)
        {
            try
            {
                var EmployeeId = new SqlParameter("@EmployeeId", employeeId);
                var MonthId = new SqlParameter("@MonthId", Month);
                var FinancialYearId = new SqlParameter("@FinancialYearId ", finid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.get);
                var purchaseList = await _dbContext.tbl_HolidayLeave.FromSqlRaw("Stpro_HolidayLeave @EmployeeId,@MonthId,@FinancialYearId ,@item, @CompanyId, @BranchId,@UserId, @Action", EmployeeId, MonthId, FinancialYearId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
