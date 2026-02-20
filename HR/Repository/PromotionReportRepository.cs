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
using System.Data.Common;
using System.Data;
using System.Reflection;
using Newtonsoft.Json.Serialization;


namespace BuildExeHR.Repository
{
    public class PromotionReportRepository : IPromotionReportRepository
    {
        private readonly HRContext _dbContext;

        public PromotionReportRepository(HRContext dbContext)
        {
            _dbContext = dbContext;


        }

        public enum Actions
        {
            SelectReport = 1
            
        }

        public async Task<string> Promotion(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_PromotionReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId  });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId  });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    await cmd.Connection.OpenAsync();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details += dataTable.Rows[i][0].ToString();
                }

                if (string.IsNullOrWhiteSpace(details))
                    details = "[]";

                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
