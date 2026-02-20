using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.Common;

using System.Data;
using System.Reflection;
namespace BuildExeMaterialServices.Repository
{
    public class FinishedGoodsRepository : IFinishedGoodsRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Get=4
           
        }

        public FinishedGoodsRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<FinishedGoods> finishedGoods)
        {
            try
            {
               
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(finishedGoods));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var finishedgoodsList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_FinishedGoods @item,@CompanyId, @BranchId,  @Action", item, CompanyId, BranchId, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<FinishedGoods> finishedGoods)
        {
            try
            {
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(finishedGoods));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var finishedgoodsList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_FinishedGoods @item, @CompanyId, @BranchId, @Action",  item, CompanyId, BranchId, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int CompanyId, int BranchId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_FinishedGoods";
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Get });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetbyID(int Id)
        {
            try
            {
             

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_FinishedGoods";
                cmd.CommandType = CommandType.StoredProcedure;


              
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Id });
                
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
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



        //public async Task<IEnumerable<Validation>> PostData(IEnumerable<MaterialIssue> issue)
        //{
        //    try
        //    {
        //        var Id = new SqlParameter("@Id", "0");
        //        var CompanyId = new SqlParameter("@CompanyId", "0");
        //        var BranchId = new SqlParameter("@BranchId", "0");
        //        var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
        //        var UserId = new SqlParameter("@UserId", "0");
        //        var item = new SqlParameter("@json", JsonConvert.SerializeObject(issue));
        //        var Action = new SqlParameter("@Action", Actions.Insert);

        //        var finishedgoodsList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialIssue @Id,@CompanyId,@BranchId,@FinancialYearId,@UserId,@json,@Action", Id,  CompanyId, BranchId,FinancialYearId, UserId, item, Action).ToListAsync();
        //        return finishedgoodsList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}
    }
}
