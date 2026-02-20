using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class DocumentGroupRepository : IDocumentGroupRepository
    {
        private readonly BasicContext _dbContext;
        public DocumentGroupRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectById = 4,
            SelectByCompanyAndBranch = 5
        }
        public async Task<IEnumerable<Validation>> Delete(int id, int userId)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", userId);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetByID(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentGroup";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectById });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string documentTypeDet = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    documentTypeDet = documentTypeDet + dataTable.Rows[i][0].ToString();
                }
                return documentTypeDet;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int CompanyId, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentGroup";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByCompanyAndBranch });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string documentTypeDet = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    documentTypeDet = documentTypeDet + dataTable.Rows[i][0].ToString();
                }
                return documentTypeDet;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int CompanyId, int Branchid, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentGroup";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByCompanyAndBranch });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string documentTypeDet = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    documentTypeDet = documentTypeDet + dataTable.Rows[i][0].ToString();
                }
                return documentTypeDet;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(DocumentGroup documentGroup)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(documentGroup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Update(DocumentGroup documentGroup)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(documentGroup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckDocGroupEditDelete @Id", Id).ToListAsync();
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
