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
    public class StockRepository:IStockRepository 
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }

        public StockRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Insert(IEnumerable<Stock> stock)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(stock));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action", materialId, item, CompanyId, BranchId, FinancialYearId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Delete(int Id)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Action = new SqlParameter("@Action", Actions.Delete);
           await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action", materialId, item, CompanyId, BranchId, FinancialYearId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Update(IEnumerable<Stock> stock)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(stock));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);
          await  _dbContext.Database.ExecuteSqlRawAsync("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action", materialId, item, CompanyId, BranchId, FinancialYearId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Stock>> Get()
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectAll);

            var _product =await _dbContext.tbl_Stock.FromSqlRaw("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action", materialId, item, CompanyId, BranchId, FinancialYearId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Stock>> GetbyID(int Id)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Action = new SqlParameter("@Action", Actions.Select);

            var _product =await _dbContext.tbl_Stock.FromSqlRaw("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action", materialId, item, CompanyId, BranchId, FinancialYearId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> StockReport(StockSearch stockSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Stock";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@StockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(stockSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = stockSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = stockSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });
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
