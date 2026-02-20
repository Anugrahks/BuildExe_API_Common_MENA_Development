using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class AdvanceBalanceRepository : IAdvanceBalanceRepository
    {
        private readonly HRContext _dbContext;
        public AdvanceBalanceRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            SelectBalance = 1,
            Select = 2,
            Selectall = 3,
            tdsbalance = 2,
            RetensionBalance = 3

        }
        public async Task<IEnumerable<AdvanceBalance>> Get(int projectId, int unitId, int blockId, int floorId, int companyId, int Branchid, int employeeId)
        {
            try
            {
                var Balance = new SqlParameter("@Balance", "0");
                var Type = new SqlParameter("@Type", "COMPANY LABOUR");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var UnitId = new SqlParameter("@UnitId", unitId);
                var EmployeeId = new SqlParameter("@EmployeeId", employeeId);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", Branchid);
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
        public async Task<decimal> GetBalance(int ProjectId, int UnitId, int BlockId, int FloorId, int CompanyId, int Branchid, int employeeId)
        {
            decimal id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_AdvanceBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "LABOUR" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
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

        public async Task<string> tdsBalance(int Employeeid, int projectId, int Unitid, int BlockID, int Floorid)
        {
            string id = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_TdseBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "EMPLOYEE" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockID });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = Floorid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = Employeeid });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.tdsbalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //await cmd.ExecuteNonQueryAsync();
                //id = (decimal)cmd.Parameters["@Balance"].Value;
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

        public async Task<string> RetensionBalance(int Employeeid, int projectId, int Unitid, int BlockID, int Floorid)
        {
            string id = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_TdseBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "EMPLOYEE" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockID });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = Floorid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = Employeeid });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.RetensionBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //await cmd.ExecuteNonQueryAsync();
                //id = (decimal)cmd.Parameters["@Balance"].Value;

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
    }
}
