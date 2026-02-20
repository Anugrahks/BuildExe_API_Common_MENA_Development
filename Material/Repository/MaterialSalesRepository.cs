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
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class MaterialSalesRepository : IMaterialSalesRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectReport = 6,
            SelectReportJson = 7,
            Selectforview = 8
        }

        public MaterialSalesRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialSales> mat)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(mat));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var SalesDate = new SqlParameter("@SalesDate", DateTime.Now);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSales @Id, @CompanyId, @BranchId,@json,@FinancialYearId,@UserId, @Action, @SalesDate", materialId, CompanyId, BranchId, item, FinancialYearId, UserId, Action, SalesDate).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int UserID)
        {
            try
            {
                var materialId = new SqlParameter("@Id", Id);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var SalesDate = new SqlParameter("@SalesDate", DateTime.Now);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSales @Id, @CompanyId, @BranchId,@json,@FinancialYearId,@UserId, @Action ,@SalesDate", materialId, CompanyId, BranchId, item, FinancialYearId, UserId, Action, SalesDate).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<MaterialSales> mat)
        {
            try
            {
                var materialId = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(mat));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var SalesDate = new SqlParameter("@SalesDate", DateTime.Now);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_MaterialSales @Id, @CompanyId, @BranchId,@json,@FinancialYearId,@UserId, @Action,@SalesDate", materialId, CompanyId, BranchId, item, FinancialYearId, UserId, Action, SalesDate).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
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

        public async Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
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

        public async Task<string> GetById(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
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


        public async Task<string> GetWorkOrder(int CustomerId, DateTime SalesDate, int MaterialTypeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CustomerId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = MaterialTypeId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = SalesDate });
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

        public async Task<string> GetOrdersForSaleOrder(int CustomerId,  int MaterialId, int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = MaterialId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CustomerId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
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
        
        public async Task<string> GetWorkOrderMaterial(int SaleOrderId, int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = SaleOrderId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 10 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
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

        public async Task<string> GetForWareHouse(int BranchId)
  {
      try
      {
          DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

          cmd.CommandText = "dbo.Stpro_MaterialSales";
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
          cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
          cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
          cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
          cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
          cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
          cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
          cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
        public async Task<string> GetforReport(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });

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

        public async Task<string> GetPresentStock(int StockPoint, int CustomerId, DateTime Date)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialSales";
                cmd.CommandType = CommandType.StoredProcedure;

                var item = JsonConvert.SerializeObject(new
                {
                    StockPoint,
                    CustomerId,
                    Date
                });

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = item });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SalesDate", SqlDbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 12 });

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

    }
}
