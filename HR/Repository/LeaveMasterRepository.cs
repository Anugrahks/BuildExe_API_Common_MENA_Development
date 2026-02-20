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
using System.Data.Common;
using System.Data;
using System.ComponentModel.Design;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class LeaveMasterRepository : ILeaveMasterRepository
    {
        private readonly HRContext _dbContext;
        public LeaveMasterRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectForedit = 4,
            SelectById = 5,
            SalaryPerDay =6,
            selectwithholiday=7,
            selectwithemployee=8
        }
        public async Task<IEnumerable<Validation>> Delete(int ID, int userId)
        {
            try
            {
                var Id = new SqlParameter("@Id", ID);
                var json = new SqlParameter("@json", string.Empty);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var delete = await _dbContext.tbl_validation.FromSqlRaw("stpro_LeaveMaster @Id,@json, @CompanyId, @BranchId,@UserId, @Action", Id, json, CompanyId, BranchId, UserId, Action).ToListAsync();
                return delete;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByID(int ID)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = ID });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectById });
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


        public async Task<string> AutoFetch(int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });
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
        
        public async Task<IEnumerable<LeaveMaster>> Get(int companyid, int branchid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_LeaveMaster.Where(x => x.CompanyId == companyid).ToListAsync();
                else
                    return await _dbContext.tbl_LeaveMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getleave(int companyid, int branchid, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForedit });
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


        public async Task<string> Getleavewithholiday(int companyid, int branchid, int EmployeeId, DateTime DateWorked)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { DateWorked = DateWorked });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectwithholiday });
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


        public async Task<string> Getleavewithemployee(int companyid, int branchid, int EmployeeId, int MonthId, int YearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { MonthId = MonthId, YearId = YearId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectwithemployee });
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

        public async Task<IEnumerable<Validation>> Insert(LeaveMaster leaveMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(leaveMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var inserted = await _dbContext.tbl_validation.FromSqlRaw("stpro_LeaveMaster @Id,@json, @CompanyId, @BranchId,@UserId, @Action", Id, json, CompanyId, BranchId, UserId, Action).ToListAsync();
                return inserted;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(LeaveMaster leaveMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(leaveMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var updated = await _dbContext.tbl_validation.FromSqlRaw("stpro_LeaveMaster @Id,@json, @CompanyId, @BranchId,@UserId, @Action", Id, json, CompanyId, BranchId, UserId, Action).ToListAsync();
                return updated;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> SalaryPerDay(LeaveSettingsDet leaveSettings)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value =  leaveSettings.NoOfLeaves});
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(leaveSettings) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = leaveSettings.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = leaveSettings.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SalaryPerDay });
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
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int branchId)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckLeaveSettingsEditDelete @Id, @BranchId", Id, BranchId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }


        public async Task<string> GetleavewithemployeeMobile(int companyid, int branchid, int EmployeeId, int MonthId, int YearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { MonthId = MonthId, YearId = YearId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectwithemployee });
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


        public async Task<string> SalaryPerDayMobile(LeaveSettingsDet leaveSettings)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_LeaveMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(leaveSettings) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = leaveSettings.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.NVarChar) { Value = leaveSettings.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SalaryPerDay });
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
