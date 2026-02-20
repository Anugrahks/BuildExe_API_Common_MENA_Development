using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly ProductContext _dbContext;
        public ReturnRepository(ProductContext dbContext)
        {
            _dbContext=dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectForEdit = 4,
            SelectForApproval = 5,
            SelectById = 6,
            SelectForReport = 7,
            LatestCreditNote = 8
        }

        #region Data Manipulation

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ReturnMaster> returns)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(returns));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var returnList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Return @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return returnList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ReturnMaster> returns)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(returns));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var returnList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Return @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return returnList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", "{}");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var returnList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Return @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return returnList;
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

            cmd.CommandText = "Stpro_Return";
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
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        public async Task<string> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_Return";
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
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        public async Task<string> GetById(int Id)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_Return";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectById });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        public async Task<string> GetForReport(BillSearch billSearch)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_Return";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForReport });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        public int GetLatestCreditNote(int CompanyId, int BranchId, int FinancialYearId)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_Return";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.LatestCreditNote });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            int creditNo = (int)cmd.ExecuteScalar();
            return creditNo;
        }

        public async Task<string> GetSpecificsForReturn(ReturnMaster returnMaster)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_GetSpecificsForReturn";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = returnMaster.Id });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = returnMaster.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = returnMaster.BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(returnMaster) });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = returnMaster.FinancialYearId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        public async Task<string> GetSpecificsForReturnAmount(ReturnMaster returnMaster)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_GetSpecificsForReturn";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = returnMaster.Id });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = returnMaster.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = returnMaster.BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(returnMaster) });
            cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = returnMaster.FinancialYearId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = await cmd.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            string returnList = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                returnList += dataTable.Rows[i][0].ToString();
            }

            if (returnList == "")
                returnList = "[]";
            return returnList;
        }

        #endregion
    }
}
