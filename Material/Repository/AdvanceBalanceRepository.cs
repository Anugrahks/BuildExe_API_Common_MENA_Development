using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class AdvanceBalanceRepository:IAdvanceBalanceRepository 
    {
        private readonly MaterialContext _dbContext;
        public AdvanceBalanceRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            SelectBalance = 1,
            Select = 2,
            Selectall = 3,
           
        }
        public async Task <string> Get(int companyId, int Branchid, int SupplierId, int ProjectId)
        {
            string  id = "";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_AdvanceBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "SUPPLIER" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //await cmd.ExecuteNonQueryAsync();
                // id = (decimal)cmd.Parameters["@Balance"].Value;
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

            id = "";
            }
            return id;
        }

        public async Task<string> WithProject(int CompanyId, int Branchid, int SupplierId, int ProjectId)
        {
            string id = "";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_AdvanceBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar) { Value = "SUPPLIER" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //await cmd.ExecuteNonQueryAsync();
                // id = (decimal)cmd.Parameters["@Balance"].Value;
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

                id = "";
            }
            return id;
        }

        public async Task<IEnumerable<AdvanceBalance>> GetDetail(int companyId, int Branchid, int SupplierId, int Projectid)
        {
            try
            {
                var Balance = new SqlParameter("@Balance", "0");
                var Type = new SqlParameter("@Type", "SUPPLIER");
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var EmployeeId = new SqlParameter("@EmployeeId", SupplierId);
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

    }
}
