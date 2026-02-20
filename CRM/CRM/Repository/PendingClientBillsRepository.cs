using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class PendingClientBillsRepository : IPendingClientBillsRepository
    {
        private readonly ProductContext _dbContext;
        
        public PendingClientBillsRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
 /*       public async Task<IEnumerable<PendingClientBills>> GetPendingClientBills(int type, int projectId, int unitId, int blockid, int floorId)
        {
            try
            {
                var Projectid = new SqlParameter("@ProjectId", projectId);
                var Unitid = new SqlParameter("@Unitid", unitId);
                var Blockid = new SqlParameter("@Blockid", blockid);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var TypeId = new SqlParameter("@TypeId", type);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Id = new SqlParameter("@Id", "0");
                var Action = new SqlParameter("@Action", "0");

                var _product = await _dbContext.tbl_PendingClientBills.FromSqlRaw("Stpro_GetBillforReciept @ProjectId,@Unitid,@Blockid,@FloorId,@TypeId,@FinancialYearId,@Id,@Action ", Projectid, Unitid, Blockid, FloorId, TypeId, FinancialYearId, Id, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

*/
        public async Task<string> GetPendingClientBills(int type, int projectId, int unitId, int blockid, int floorId, int divisionId)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetBillforReciept";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@Unitid", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@Blockid", SqlDbType.Int) { Value = blockid });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = type });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = divisionId });
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





        /*      public async Task<IEnumerable<PendingClientBills>> GetPendingClientBillsEdit(int type, int projectId, int unitId, int blockid, int floorId,int id)
              {
                  try
                  {
                      var Projectid = new SqlParameter("@ProjectId", projectId);
                      var Unitid = new SqlParameter("@Unitid", unitId);
                      var Blockid = new SqlParameter("@Blockid", blockid);
                      var FloorId = new SqlParameter("@FloorId", floorId);
                      var TypeId = new SqlParameter("@TypeId", type);
                      var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                      var Id = new SqlParameter("@Id", id);
                      var Action = new SqlParameter("@Action", "0");

                      var _product = await _dbContext.tbl_PendingClientBills.FromSqlRaw("Stpro_GetBillforReciept @ProjectId,@Unitid,@Blockid,@FloorId,@TypeId,@FinancialYearId,@Id,@Action ", Projectid, Unitid, Blockid, FloorId, TypeId, FinancialYearId, Id, Action).ToListAsync();

                      return _product;
                  }
                  catch (Exception ex)
                  {
                      Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                      throw;
                  }
              }
      */

        public async Task<string> GetPendingClientBillsEdit(int type, int projectId, int unitId, int blockid, int floorId, int divisionId, int id)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetBillforReciept";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@Unitid", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@Blockid", SqlDbType.Int) { Value = blockid });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = type });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = divisionId });
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
