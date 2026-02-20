using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class AdvanceBalanceRepository : IAdvanceBalanceRepository
    {

        private readonly ProductContext _dbContext;
        public AdvanceBalanceRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            SelectBalance = 3,
            Select = 4,
            tdsBaalnce = 1

        }
        public async Task<string> Get(int projectId, int Unitid, int BlockID, int Floorid)
        {
            string id = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_AdvanceBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "CLIENT" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockID });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = Floorid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //await cmd.ExecuteNonQueryAsync();
                //id = cmd.Parameters["@Balance"].Value.ToString();

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                id = "0";
            }
            return id;
        }
        public async Task<IEnumerable<AdvanceBalance>> GetDetail(int projectId, int Unitid, int BlockID, int Floorid)
        {
            try
            {
                var Balance = new SqlParameter("@Balance", "0");
                var Type = new SqlParameter("@Type", "CLIENT");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var BlockId = new SqlParameter("@BlockId", BlockID);
                var FloorId = new SqlParameter("@FloorId", Floorid);
                var UnitId = new SqlParameter("@UnitId", Unitid);
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);

                var _product = await _dbContext.tbl_advanceBalance.FromSqlRaw("Stpro_AdvanceBalance  @Balance,@Type,@ProjectId,@BlockId,@FloorId,@UnitId,@EmployeeId, @CompanyId, @BranchId, @Action", Balance, Type, ProjectId, BlockId, FloorId, UnitId, EmployeeId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> tdsBalance(int projectId, int Unitid, int BlockID, int Floorid)
        {
            string id = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_TdseBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.varc) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "CLIENT" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockID });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = Floorid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.tdsBaalnce });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;


                //await cmd.ExecuteNonQueryAsync();
                //id = String.Format("{0:.##}", cmd.Parameters["@Balance"].Value);
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = "0";
            }
            return id;
        }
    }
}
