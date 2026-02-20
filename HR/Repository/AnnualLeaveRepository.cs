using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class AnnualLeaveRepository : IAnnualLeaveRepository
    {
        private readonly HRContext _dbContext;
        public AnnualLeaveRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectForEdit = 4,
            SelectForApproval = 5,
            SelectSettlementsById = 6,
            SelectLeaveSurrendersById = 7
        }

        #region Data Manipulation

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<AnnualLeaveMaster> annualLeaves)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(annualLeaves));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var resultList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AnnualLeave @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return resultList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<AnnualLeaveMaster> annualLeaves)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(annualLeaves));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var resultList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AnnualLeave @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return resultList;
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
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", "{}");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var resultList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AnnualLeave @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return resultList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion

        #region Grids & Reports
        public async Task<string> GetForEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_AnnualLeave";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForEdit });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string resultList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                resultList += dataTable.Rows[i][0].ToString();
            }

            if (resultList == "")
                resultList = "[]";
            return resultList;
        }

        public async Task<string> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_AnnualLeave";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForApproval });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string resultList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                resultList += dataTable.Rows[i][0].ToString();
            }

            if (resultList == "")
                resultList = "[]";
            return resultList;
        }

        public async Task<string> GetSettlementsById(int Id)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_AnnualLeave";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectSettlementsById });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string resultList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                resultList += dataTable.Rows[i][0].ToString();
            }

            if (resultList == "")
                resultList = "[]";
            return resultList;
        }

        public async Task<string> GetLeaveSurrendersById(int Id)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_AnnualLeave";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectLeaveSurrendersById });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string resultList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                resultList += dataTable.Rows[i][0].ToString();
            }

            if (resultList == "")
                resultList = "[]";
            return resultList;
        }

        #endregion

    }
}
