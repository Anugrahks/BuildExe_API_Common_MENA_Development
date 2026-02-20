using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class MaterialSalesReturnRepository : IMaterialSalesReturnRepository
    {
        private readonly MaterialContext _dbContext;
        public MaterialSalesReturnRepository(MaterialContext dbContext)
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
            SelectById = 6,
            SelectForReport = 7,
            LatestCreditNote = 8
        }

        #region Data Manipulation

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialSalesReturn> salesReturns)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(salesReturns));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var returnList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSalesReturn @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
                return returnList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<MaterialSalesReturn> salesReturns)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(salesReturns));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var returnList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSalesReturn @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
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
                var returnList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSalesReturn @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();
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

            cmd.CommandText = "Stpro_MaterialSalesReturn";
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

            cmd.CommandText = "Stpro_MaterialSalesReturn";
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

            cmd.CommandText = "Stpro_MaterialSalesReturn";
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

        public async Task<string> GetForReport(MaterialSearch materialSearch)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "Stpro_MaterialSalesReturn";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
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

            cmd.CommandText = "Stpro_MaterialSalesReturn";
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

        #endregion

    }
}
