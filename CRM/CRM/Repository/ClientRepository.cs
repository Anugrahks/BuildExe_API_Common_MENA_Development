using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class ClientRepository:IClientRepository 
    {
        private readonly ProductContext _dbContext;

        public ClientRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            Selectbycompany = 6,
        }
        public async Task<IEnumerable<ClientMaster>> Get()
        {
            try { 
            var Projectid = new SqlParameter("@ProjectId", "0");
            var Unitid = new SqlParameter("@Unitid", "0");
                var json = new SqlParameter("@json", "{}");
            var Action = new SqlParameter("@Action", Actions.SelectAll);
            var _product =await  _dbContext.tbl_getClientMaster.FromSqlRaw("Stpro_GetClient @ProjectId,@Unitid,@json,@Action ", Projectid, Unitid, json, Action).ToListAsync ();

            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ClientMaster>> GetClient(int CompanyId, int BranchId)
        {
            try
            {
              
                    var Projectid = new SqlParameter("@ProjectId", CompanyId);
                    var Unitid = new SqlParameter("@Unitid", BranchId);
                    var json = new SqlParameter("@json","{}");
                    var Action = new SqlParameter("@Action", Actions.Selectbycompany);
                    var _product = await _dbContext.tbl_getClientMaster.FromSqlRaw("Stpro_GetClient @ProjectId,@Unitid,@json,@Action ", Projectid, Unitid, json, Action).ToListAsync();

                    return _product;
              

                //  return await _dbContext.tbl_getClientMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ClientMaster >> GetClientMasters ( int projectId, int unitId)
        {
            try { 
            var Projectid = new SqlParameter("@ProjectId", projectId);
            var Unitid = new SqlParameter("@Unitid", unitId);
                var json = new SqlParameter("@json", "{}");
            var Action = new SqlParameter("@Action", Actions.Select );
            var _product = await _dbContext.tbl_getClientMaster.FromSqlRaw("Stpro_GetClient @ProjectId,@Unitid,@json,@Action ", Projectid, Unitid, json, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetClientMastersnew(int projectId, int unitId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetClient";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@Unitid", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetUniqueNames(int ProjectId, int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetClient";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@Unitid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(new { CompanyId, BranchId }) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
