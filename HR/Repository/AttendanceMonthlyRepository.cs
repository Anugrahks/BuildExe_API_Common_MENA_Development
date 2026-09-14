using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeHR.Repository
{
    public class AttendanceMonthlyRepository:IAttendanceMonthlyRepository 
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            getdetails=6,
            showdetails=7,
            getleave=8,
            Insert1 = 10,
        }

        public AttendanceMonthlyRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<AttendanceMonthly> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var EmplId = new SqlParameter("@EmplId", "");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Attendance_Monthly @materialId,@item, @CompanyId, @BranchId,@UserId,@EmplId, @Action", materialId, item, CompanyId, BranchId, UserId, EmplId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int Id, int userId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var EmplId = new SqlParameter("@EmplId", "");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Attendance_Monthly @materialId,@item, @CompanyId, @BranchId,@UserId, @EmplId, @Action", materialId, item, CompanyId, BranchId, UserId, EmplId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<AttendanceMonthly> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var EmplId = new SqlParameter("@EmplId", "");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Attendance_Monthly @materialId,@item, @CompanyId, @BranchId,@UserId,@EmplId, @Action", materialId, item, CompanyId, BranchId, UserId,EmplId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<AttendanceMonthly>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_AttendanceMaster_Monthly.ToListAsync();
            foreach (var detail in list)
            {
                var detaillist =await _dbContext.tbl_AttendanceMaster_Monthly_Employee.Where(x => x.AttendanceMonthlyId == detail.Id).ToListAsync();
            }
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<AttendanceMonthly>> GetbyID(int Id)
        {
            try
            {
                var list =await _dbContext.tbl_AttendanceMaster_Monthly.Where(x => x.Id ==Id).ToListAsync();
            var detaillist = await _dbContext.tbl_AttendanceMaster_Monthly_Employee.Where(x => x.AttendanceMonthlyId == Id).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<AttendanceMonthlyList>> GetforEdit(int companyId, int Branchid, int menuId, int userID, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", menuId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
            var BranchId = new SqlParameter("@BranchId", Branchid);
            var userid = new SqlParameter("@userid", userID);
                var EmplId = new SqlParameter("@EmplId", "");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit );
            var _product =await _dbContext.tbl_AttendanceMaster_MonthlyList .FromSqlRaw("Stpro_Attendance_Monthly @materialId,@item, @CompanyId, @BranchId,@userid,@EmplId, @Action", materialId, item, CompanyId, BranchId, userid,EmplId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<AttendanceMonthlyList>> GetforApproval(int companyId, int Branchid,int userID ,int menuId, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", menuId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
            var BranchId = new SqlParameter("@BranchId", Branchid);
            var userid = new SqlParameter("@userid", userID);
                var EmplId = new SqlParameter("@EmplId", "");
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
            var _product =await _dbContext.tbl_AttendanceMaster_MonthlyList.FromSqlRaw("Stpro_Attendance_Monthly @materialId,@item, @CompanyId, @BranchId,@userid,@EmplId, @Action", materialId, item, CompanyId, BranchId, userid, EmplId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetDetailsbyid(int id, int employeeid, int financialyearid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance_Monthly";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = financialyearid });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@EmplId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.getdetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Showdetails(int monthid,int yearId, int companyid, int branchid, int financialyearid, int DurationId, DateTime FromDate, DateTime ToDate, int EmployeeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance_Monthly";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { YearId = yearId, DurationId = DurationId, FromDate = FromDate, ToDate = ToDate, EmployeeId = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = monthid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = financialyearid });
                cmd.Parameters.Add(new SqlParameter("@EmplId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.showdetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Datevalidation(int monthid, int financialyearid, int branchid, int DurationId, DateTime FromDate, DateTime ToDate, int employeeId)
        {
            try
            {
                var jsontext = JsonConvert.SerializeObject(new { DurationId = DurationId, FromDate = FromDate, ToDate = ToDate, EmployeeId = employeeId });
                var date = new SqlParameter("@FromDate", DateTime.Now);
                var EmployeeId = new SqlParameter("@EmployeeId", financialyearid);
                var LeaveId = new SqlParameter("@LeaveId", branchid);
                var Userid = new SqlParameter("@UserId", monthid);
                var durationId = new SqlParameter("@durationId", "0");
                var json = new SqlParameter("@json", jsontext);
                var Action = new SqlParameter("@Action", 5);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ValidationsinPayroll @FromDate,@EmployeeId,@LeaveId,@UserId,@durationId, @json, @Action", date, EmployeeId, LeaveId, Userid, durationId,json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> Getleavebyid(int id, int employeeid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance_Monthly";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = employeeid });
                cmd.Parameters.Add(new SqlParameter("@EmplId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.getleave });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Validation(IEnumerable<AttendanceMonthly> attendances)
        {
            try
            {

                var date = new SqlParameter("@FromDate", "1999-01-01");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var LeaveId = new SqlParameter("@LeaveId", "0");
                var Userid = new SqlParameter("@UserId", "0");
                var durationId = new SqlParameter("@durationId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(attendances));
                var EmplId = new SqlParameter("@EmplId", attendances.FirstOrDefault().EmployeeMasterId);
                var Action = new SqlParameter("@Action", 5);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ValidationsinPayroll @FromDate,@EmployeeId,@LeaveId,@UserId,@durationId, @json,@EmplId, @Action", date, EmployeeId, LeaveId, Userid, durationId, json, EmplId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetData(IEnumerable<AttendanceMonthly> attendances)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance_Monthly";
                cmd.CommandType = CommandType.StoredProcedure;

                var json = JsonConvert.SerializeObject(attendances);
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = attendances.FirstOrDefault().MonthId});
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = attendances.FirstOrDefault().CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = attendances.FirstOrDefault().BranchId});
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = attendances.FirstOrDefault().FinancialYearId});
                var EmplId = JsonConvert.SerializeObject(attendances.FirstOrDefault().EmployeeMasterId);
                cmd.Parameters.Add(new SqlParameter("@EmplId", SqlDbType.NVarChar) { Value = EmplId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.showdetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
