using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BuildExeHR.Repository
{
    public class MonthlyVaryingHeadSettingRepository : IMonthlyVaryingHeadSettingRepository
    {
        private readonly HRContext _dbContext;
        public MonthlyVaryingHeadSettingRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            GetByMonth = 4,
            GetAll = 5

        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var Json = new SqlParameter("@Json", JsonConvert.SerializeObject(monthlyVaryingHeadSettingsMasters));
                var CompanyId = new SqlParameter("@CompanyId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().CompanyId);
                var BranchId = new SqlParameter("@BranchId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().BranchId);
                var MonthId = new SqlParameter("@MonthId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().MonthId);
                var YearId = new SqlParameter("@YearId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().YearId);
                var UserId = new SqlParameter("@UserId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MonthlyVaryingHeadSettings @Id,@Json,@CompanyId, @BranchId," +
                    " @MonthId,@YearId,@UserId, @Action", Id, Json, CompanyId, BranchId,MonthId, YearId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var Json = new SqlParameter("@Json", JsonConvert.SerializeObject(monthlyVaryingHeadSettingsMasters));
                var CompanyId = new SqlParameter("@CompanyId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().CompanyId);
                var BranchId = new SqlParameter("@BranchId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().BranchId);
                var MonthId = new SqlParameter("@MonthId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().MonthId);
                var YearId = new SqlParameter("@YearId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().YearId);
                var UserId = new SqlParameter("@UserId", monthlyVaryingHeadSettingsMasters.FirstOrDefault().UserId);
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MonthlyVaryingHeadSettings @Id,@Json,@CompanyId, @BranchId," +
                    " @MonthId,@YearId,@UserId, @Action", Id, Json, CompanyId, BranchId, MonthId, YearId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int companyId, int branchId, int userId, int monthId, int yearId,int EmployeeId, int DurationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MonthlyVaryingHeadSettings";
                cmd.CommandType = CommandType.StoredProcedure;
                var json = JsonConvert.SerializeObject(new { EmployeeId= EmployeeId ,DurationId = DurationId, FromDate = FromDate, ToDate = ToDate });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = monthId });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = yearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByMonth });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetByUser(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MonthlyVaryingHeadSettings";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetAll });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetByApproval(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MonthlyVaryingHeadSettings";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string varyingHeadDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    varyingHeadDetails = varyingHeadDetails + dataTable.Rows[i][0].ToString();
                }
                return varyingHeadDetails;


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
                var Id = new SqlParameter("@Id", id);
                var Json = new SqlParameter("@Json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var MonthId = new SqlParameter("@MonthId", "0");
                var YearId = new SqlParameter("@YearId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MonthlyVaryingHeadSettings @Id,@Json,@CompanyId, @BranchId," +
                    " @MonthId,@YearId,@UserId, @Action", Id, Json, CompanyId, BranchId, MonthId, YearId, UserId, Action).ToListAsync();
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckMonthlyVaryingHeadSettingsEditDelete @Id", Id).ToListAsync();
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
