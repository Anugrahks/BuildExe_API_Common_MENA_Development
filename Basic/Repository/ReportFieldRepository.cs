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
    public class ReportFieldRepository:IReportFieldRepository 
    {
        private readonly BasicContext _dbContext;
        public ReportFieldRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task  <IEnumerable<ReportField >> GetByID(int meniid)
        {
            try {
                var menuId = new SqlParameter("@MenuId", meniid);
                var type = new SqlParameter("@Type", "Fields");
                var action = new SqlParameter("@Action", Actions.GetById);
                return await _dbContext.tbl_ReportFields.FromSqlRaw("Stpro_GetReportFieldAndFilters @MenuId, @Type, @Action", menuId, type, action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetPrintableByID(int meniid)
        {
            try
            {
                //var menuId = new SqlParameter("@MenuId", meniid);
                //var type = new SqlParameter("@Type", "Fields");
                //var action = new SqlParameter("@Action", Actions.GetById);
                //return await _dbContext.tbl_ReportFields.FromSqlRaw("Stpro_GetReportFieldAndFilters @MenuId, @Type, @Action", menuId, type, action).ToListAsync();
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetReportFieldAndFilters";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.NVarChar) { Value = meniid });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = "Fields" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetPrintableByID });
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
            catch (Exception)
            { throw; }
        }



        public enum Actions
        {
            GetAll = 1,
            GetById = 2,
            GetPrintableByID = 3,
        }

        public async Task<IEnumerable<ReportField>> Get()
        {
            try
            {
                var menuId = new SqlParameter("@MenuId", 0);
                var type = new SqlParameter("@Type", "Fields");
                var action = new SqlParameter("@Action", Actions.GetAll);
                return await _dbContext.tbl_ReportFields.FromSqlRaw("Stpro_GetReportFieldAndFilters @MenuId, @Type, @Action", menuId, type, action).ToListAsync();
            }
            catch (Exception)
            { throw; }


        }
    }
}
