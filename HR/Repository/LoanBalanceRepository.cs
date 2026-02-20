using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class LoanBalanceRepository : ILoanBalanceRepository
    {
        private readonly HRContext _dbContext;
        public LoanBalanceRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            SelectBalance = 1,
            Select = 2,


        }
        public async Task<IEnumerable<LoanBalance>> Get(int companyId, int Branchid, int employeeId)
        {
            try
            {
                var Balance = new SqlParameter("@Balance", "0");
                var Type = new SqlParameter("@Type", "COMPANY LABOUR");

                var EmployeeId = new SqlParameter("@EmployeeId", employeeId);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var Action = new SqlParameter("@Action", Actions.Select);

                var _product = await _dbContext.tbl_LoanBalance.FromSqlRaw("Stpro_LoanBalance  @Balance,@Type,@EmployeeId, @CompanyId, @BranchId, @Action", Balance, Type, EmployeeId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<decimal> GetBalance(int CompanyId, int Branchid, int employeeId)
        {
            decimal id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_LoanBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "LOAN" });

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                await cmd.ExecuteNonQueryAsync();
                id = (decimal)cmd.Parameters["@Balance"].Value;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                id = 0;
            }
            return id;
        }

    }
}
