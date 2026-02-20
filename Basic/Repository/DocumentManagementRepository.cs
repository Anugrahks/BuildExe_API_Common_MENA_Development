using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildExeBasic.Repository
{
    public class DocumentManagementRepository : IDocumentManagementRepository
    {
        private readonly BasicContext _dbContext;
        public DocumentManagementRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectById =4,
            SelectByCompanyAndBranch =5,
            DeleteFileById = 6,
            SelectByFormName = 7
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

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentManagement @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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

                cmd.CommandText = "dbo.StPro_DocumentManagement";
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

                cmd.CommandText = "dbo.StPro_DocumentManagement";
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

        public async Task<string> GetForDashboard(int Branchid, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentManagement";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
        //public async Task<string> Get(int CompanyId, int Branchid, int UserId)
        //{
        //    try
        //    {
        //        DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

        //        cmd.CommandText = "dbo.StPro_DocumentManagement";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
        //        cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
        //        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
        //        cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
        //        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
        //        cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByCompanyAndBranch });
        //        if (cmd.Connection.State != ConnectionState.Open)
        //        {
        //            cmd.Connection.Open();
        //        }

        //        DbDataReader reader = await cmd.ExecuteReaderAsync();

        //        var dataTable = new DataTable();
        //        dataTable.Load(reader);
        //        string documentTypeDet = "";
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            documentTypeDet = documentTypeDet + dataTable.Rows[i][0].ToString();
        //        }
        //        return documentTypeDet;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<string> Get(int CompanyId, int Branchid, int UserId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.StPro_DocumentManagement";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByCompanyAndBranch });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }

        public async Task<IEnumerable<Validation>> Insert(DocumentManagement documentManagement)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(documentManagement));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentManagement @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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

        public async Task<IEnumerable<Validation>> Update(DocumentManagement documentManagement)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(documentManagement));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("StPro_DocumentManagement @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFileID(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentManagement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.DeleteFileById });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string document = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    document = document + dataTable.Rows[i][0].ToString();
                }
                return document;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            //try
            //{
            //    return await _dbContext.tbl_DocumentFiles.Where(s => s.Id == id).ToListAsync();
            //}
            //catch (Exception)
            //{ throw; }
        }

        public async Task<string> Get(string formName, int masterId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_DocumentManagement";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = masterId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = formName });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByFormName });
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

                if(documentTypeDet == "")
                    documentTypeDet = "[]";
                return documentTypeDet;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
