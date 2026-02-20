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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class AttendancePayRollRepository : IAttendancePayRollRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Selectforedit=4,
            selectedit=5
        }

        public AttendancePayRollRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<AttendancePayroll> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendancePayRoll @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<AttendancePayroll> attendances)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendancePayRoll @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
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
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_AttendancePayRoll @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetPayRollAttendanceDetail(int id, int EmployeeId,  int Isdetail)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_AttendancePayroll";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = Isdetail });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Selectforedit });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetPayRollAttendancebyid(AttendancePayrollList attendanceList)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_AttendancePayroll";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = attendanceList.Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(attendanceList) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = attendanceList.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = attendanceList.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectedit });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> Getvalidationpayroll(DateTime dateworked, int companyid, int branchid, int departmentId)
        {
            try
            {
                var date = new SqlParameter("@FromDate", dateworked);
                var EmployeeId = new SqlParameter("@EmployeeId", companyid);
                var LeaveId = new SqlParameter("@LeaveId", branchid);
                var Userid = new SqlParameter("@UserId", departmentId);
                var durationId = new SqlParameter("@durationId", "0");
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", 4);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ValidationsinPayroll @FromDate,@EmployeeId,@LeaveId,@UserId,@durationId, @json, @Action", date, EmployeeId, LeaveId, Userid, durationId,json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> LatePenaltyDetails(DateTime dateworked, int EmployeeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_AttendancePayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { DateWorked = dateworked });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

    }

}

