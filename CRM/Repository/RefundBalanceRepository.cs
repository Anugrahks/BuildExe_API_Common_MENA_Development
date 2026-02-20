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
    public class RefundBalanceRepository:IRefundBalanceRepository
    {
        private readonly ProductContext _dbContext;
        public RefundBalanceRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            SelectBalance = 1,
            Select = 2,
           

        }
        public async Task<decimal> Get(int type,int projectId, int Unitid, int BlockID, int Floorid)
        {
            decimal id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_RefundBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = type });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockID });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = Floorid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
          
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
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
        public async Task<IEnumerable<RefundBalance>> GetDetail(int type,int projectId, int Unitid, int BlockID, int Floorid)
        {
            try
            {
                var Balance = new SqlParameter("@Balance", "0");
                var Type = new SqlParameter("@Type", type);
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var BlockId = new SqlParameter("@BlockId", BlockID);
                var FloorId = new SqlParameter("@FloorId", Floorid);
                var UnitId = new SqlParameter("@UnitId", Unitid);
              
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);

                var _product = await _dbContext.tbl_RefundBalance.FromSqlRaw("Stpro_RefundBalance  @Balance,@Type,@ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", Balance, Type, ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
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
