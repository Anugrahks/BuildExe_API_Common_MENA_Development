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
using System.Data;
using System.Data.Common;
using System.ComponentModel.Design;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class RetensionPaymentRepository : IRetensionPaymentRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            WorkOrderDetails = 1,
            RetensionAmounts =2
        }

        public RetensionPaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<string> Getbyproject(int ProjectId, int UnitId, int BlockId, int FloorId,int SubContractorId)
        {
            //try
            //{
            //    var list =await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.ProjectId  == ProjectId).Where(x => x.UnitId  == UnitId ).
            //    Where(x => x.BlockId  == BlockId).Where(x => x.FloorId  == FloorId).Where(x => x.SubContractorId  == SubContractorId ).Where(x => x.ApprovalStatus  == 1).Where(x => x.IsDeleted == 0).Where(x => x.WorkOrderStatus == 0).OrderByDescending(i=>i.Id).ToListAsync();
            //return list;
            //}
            //catch (Exception)
            //{ throw; }

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_RetensionPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@SubContractorId", SqlDbType.Int) { Value = SubContractorId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.WorkOrderDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = string.Empty;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;


                //var Id = new SqlParameter("@ProjectId", ProjectId);
                //var unit = new SqlParameter("@UnitId", UnitId);
                //var block = new SqlParameter("@BlockId", BlockId);
                //var floor = new SqlParameter("@FloorId", FloorId);
                //var subCont = new SqlParameter("@SubContractorId", SubContractorId);
                //var Action = new SqlParameter("@Action", Actions.WorkOrderDetails);
                //var _product = await _dbContext.tbl_SubContractorWorkOrderMaster.FromSqlRaw("Stpro_RetensionPayment @ProjectId,@UnitId,@BlockId,@FloorId,@SubContractorId,@Action", Id, unit, block, floor, subCont, Action).ToListAsync();
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetAmounts(int companyId, int branchId, int userId, int menuId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_RetensionAmounts";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Item", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = menuId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.RetensionAmounts });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = string.Empty;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
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
