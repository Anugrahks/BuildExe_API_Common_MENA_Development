using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BuildExeServices.Repository;
using Newtonsoft.Json;
using System.Data.SqlClient;

using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Security.Cryptography;

namespace BuildExeServices.Repository
{
    public class QCRepository : IQCRepository
    {
        private readonly ProductContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4
        }

        public QCRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<QC> mat)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@StageName", "");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(mat));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var JobId = new SqlParameter("@JobId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", "2");
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_QCUpdation @Id, @StageName, @BranchId,@json,@FinancialYearId,@JobId,@UserId, @Action", materialId, CompanyId, BranchId, item, FinancialYearId, JobId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

 
        public async Task<string> GetbyBranch(BillSearch projectWorkSetting)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_QCUpdation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = projectWorkSetting.DataId });
                cmd.Parameters.Add(new SqlParameter("@StageName", SqlDbType.NVarChar) { Value = projectWorkSetting.StageName });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = projectWorkSetting.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = projectWorkSetting.OrderId });
                cmd.Parameters.Add(new SqlParameter("@JobId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = projectWorkSetting.ActionButton });
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
                if (purcasedetails == "")
                    purcasedetails = "[]";
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
