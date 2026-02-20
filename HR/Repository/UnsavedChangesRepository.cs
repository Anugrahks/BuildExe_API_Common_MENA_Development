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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class UnsavedChangesRepository : IUnsavedChangesRepository
    {
        private readonly HRContext _dbContext;

        public UnsavedChangesRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> UnsavedChanges(int id, int purpose)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_UnsavedChanges";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@Purpose", SqlDbType.Int) { Value = purpose });
                cmd.Parameters.Add(new SqlParameter("@Dateworked", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@isGroup", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> UnsavedChangesAdd(int purpose, string dateworked, int projectid)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_UnsavedChanges";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Purpose", SqlDbType.Int) { Value = purpose });
                cmd.Parameters.Add(new SqlParameter("@Dateworked", SqlDbType.NVarChar) { Value = dateworked });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectid });
                cmd.Parameters.Add(new SqlParameter("@isGroup", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> UnsavedChangesAddnew(int purpose, DateTime fromdate, DateTime todate, int branchid, int isGroup)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_UnsavedChanges";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@Purpose", SqlDbType.Int) { Value = purpose });
                cmd.Parameters.Add(new SqlParameter("@Dateworked", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromdate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = todate });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@isGroup", SqlDbType.Int) { Value = isGroup });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
