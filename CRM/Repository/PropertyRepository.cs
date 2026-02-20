using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace BuildExeServices.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ProductContext _dbContext;

        public PropertyRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            GetUnits = 10,
            GetRentalCategories = 11,
            GetActiveTenents = 12,
            GetProperties = 13,
            GetVacantUnits =14
        }

        public async Task<IEnumerable<Validation>> InsertProperty(string jsonData)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0") { SqlDbType = SqlDbType.Int };
                var jsonParam = new SqlParameter("@JsonData", jsonData) { SqlDbType = SqlDbType.NVarChar, Size = -1 };
                var Action = new SqlParameter("@Action", Actions.Insert);
                var result = await _dbContext.tbl_validation
                    .FromSqlRaw("EXEC Stpro_PropertyRental @Id, @JsonData, @Action", Id, jsonParam, Action)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> UpdateProperty(string jsonData)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0") { SqlDbType = SqlDbType.Int };
                var jsonParam = new SqlParameter("@JsonData", jsonData) { SqlDbType = SqlDbType.NVarChar, Size = -1 };
                var Action = new SqlParameter("@Action", Actions.Update);
                var result = await _dbContext.tbl_validation
                    .FromSqlRaw("EXEC Stpro_PropertyRental @Id, @JsonData, @Action", Id, jsonParam, Action)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetPropertyRentalCategories(int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PropertyRental";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@JsonData", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetRentalCategories });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 0)
                        return "[]";
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetPropertyUnitDetails(int ProjectId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PropertyRental";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@JsonData", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetUnits });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 0)
                        return "[]";
                    //var list = dataTable.AsEnumerable().Select(row => dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col]));
                    //return JsonConvert.SerializeObject(list, Formatting.Indented);
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetActiveTenants(int CompanyId, int BranchId)
        {
            try
            {
                var myObject = new
                {
                    CompanyId = CompanyId,
                    BranchId = BranchId
                };

               
                string jsonData = JsonConvert.SerializeObject(myObject);
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PropertyRental";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@JsonData", SqlDbType.NVarChar) { Value = jsonData });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetActiveTenents });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 0)
                        return "[]";
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetProperties(int CompanyId, int BranchId)
        {
            try
            {
                var myObject = new
                {
                    CompanyId = CompanyId,
                    BranchId = BranchId
                };


                string jsonData = JsonConvert.SerializeObject(myObject);
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PropertyRental";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@JsonData", SqlDbType.NVarChar) { Value = jsonData });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetProperties });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 0)
                        return "[]";
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetVacantUnits(int CompanyId, int BranchId)
        {
            try
            {
                var myObject = new
                {
                    CompanyId = CompanyId,
                    BranchId = BranchId
                };


                string jsonData = JsonConvert.SerializeObject(myObject);
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PropertyRental";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@JsonData", SqlDbType.NVarChar) { Value = jsonData });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetVacantUnits });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 0)
                        return "[]";
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




    }
}
