using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;


namespace BuildExeBasic.Repository
{
    public class ReleaseNotesRepository : IReleaseNotesRepository
    {
        private readonly BasicContext _dbContext;
        public ReleaseNotesRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> Get()
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_ReleaseNotes";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@VersionNumber", SqlDbType.NVarChar) { Value = "" });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(string VersionNumber)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_ReleaseNotes";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 1 });
                cmd.Parameters.Add(new SqlParameter("@VersionNumber", SqlDbType.NVarChar) { Value = VersionNumber });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string releasenotes = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    releasenotes = releasenotes + dataTable.Rows[i][0].ToString();
                }
                return releasenotes;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
