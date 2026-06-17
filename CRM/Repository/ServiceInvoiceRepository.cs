using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class ServiceInvoiceRepository : IServiceInvoiceRepository
    {
        private readonly ProductContext _dbContext;

        public ServiceInvoiceRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetPendingServiceInvoices(int jobId, int companyId, int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_GetPendingServiceInvoices";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobId", SqlDbType.Int) { Value = jobId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                string result = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    result += dataTable.Rows[i][0].ToString();

                if (result == "") result = "[]";
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetPendingServiceInvoicesEdit(int jobId, int companyId, int branchId, int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_GetPendingServiceInvoices";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobId", SqlDbType.Int) { Value = jobId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                string result = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    result += dataTable.Rows[i][0].ToString();

                if (result == "") result = "[]";
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetServiceInvoiceCustomer(int jobId, int companyId, int branchId, int customerId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_GetServiceInvoiceCustomer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@JobId", SqlDbType.Int) { Value = jobId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = customerId });


                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                string result = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    result += dataTable.Rows[i][0].ToString();

                if (result == "") result = "[]";
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}