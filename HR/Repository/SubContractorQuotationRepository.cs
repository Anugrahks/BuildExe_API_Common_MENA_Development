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
using Microsoft.VisualBasic;
using System.ComponentModel.Design;

namespace BuildExeHR.Repository
{
    public class SubContractorQuotationRepository : ISubContractorQuotationRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            GetForEdit = 4,
            GetForApproval = 5,
            GetById = 6,
            GetQuotationNo = 7,
            GetQuotationId = 8,
            GetForIndent = 9
        }
        public SubContractorQuotationRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Post(IEnumerable<SubContractorQuotation> subContractorQuotation)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var MaterialType = new SqlParameter("@MaterialType", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(subContractorQuotation));
                var Action = new SqlParameter("@Action", Actions.Insert);

                var finishedgoodsList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorQuotation @Id,@CompanyId,@BranchId,@FinancialYearId,@UserId,@MaterialType,@json,@Action", Id, CompanyId, BranchId, FinancialYearId, UserId, MaterialType, item, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Put(IEnumerable<SubContractorQuotation> subContractorQuotation)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var MaterialType = new SqlParameter("@MaterialType", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(subContractorQuotation));
                var Action = new SqlParameter("@Action", Actions.Update);

                var finishedgoodsList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorQuotation @Id,@CompanyId,@BranchId,@FinancialYearId,@UserId,@MaterialType,@json,@Action", Id, CompanyId, BranchId, FinancialYearId, UserId, MaterialType, item, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Delete(int Id, int UserId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Delete });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetForEdit(int CompanyId, int BranchId, int FinacialYearId, int UserId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetForEdit });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetForApproval(int CompanyId, int BranchId, int FinacialYearId, int UserId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetForApproval });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetById(int Id)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions. GetById});
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetQuotationNo(int CompanyId, int BranchId, int FinacialYearId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetQuotationNo });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetQuotationId(int BranchId, int FinacialYearId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetQuotationId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetForIndent(int ProjectId, int BranchId, int FinacialYearId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinacialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetForIndent });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
