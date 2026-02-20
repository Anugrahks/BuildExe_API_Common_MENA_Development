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
    public class MaterialRepository:IMaterialRepository 
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            SelectReport = 6,
            SelectStockReport = 7,
            SelectByMaterialId = 8
        }

        public MaterialRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Material > material)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(material));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id,int userId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", userId);
            var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Material> material)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(material));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Material>> Get(int CompanyId, int Branchid)
        {
            try
            {


                var purchaselist = await _dbContext.tbl_MaterialMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var purdetail in purchaselist)
                {
                    var purchasedetaillist = await _dbContext.tbl_OpeningStock.Where(x => x.MaterialId == purdetail.Id).ToListAsync();
                }
                return purchaselist;
                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Material>> Get(int CompanyId, int Branchid, int UserId)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_MaterialMaster.Where(x => x.CompanyId == CompanyId).
                    Where(x => x.BranchId == Branchid).Where(x=>x.UserId == UserId).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var purdetail in purchaselist)
                {
                    var purchasedetaillist = await _dbContext.tbl_OpeningStock.Where(x => x.MaterialId == purdetail.Id).ToListAsync();
                }
                return purchaselist;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Material>> GetMaterialWithBrand(int companyId, int branchid)
        {
            try
            {

                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 9);

                var materials = _dbContext.tbl_MaterialMaster.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId, @UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToList();
                return materials;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Material>> MaterialWithBrandCategory(int companyId, int branchid,int MaterialTypeId)
        {
            try
            {

                var materialId = new SqlParameter("@materialId", MaterialTypeId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 13);

                var materials = _dbContext.tbl_MaterialMaster.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId, @UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToList();
                return materials;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        

        public async Task<IEnumerable<Material>> GetMaterialWithBrandtype(int companyId, int branchid, int MaterialTypeId)
        {
            try
            {

                var materialId = new SqlParameter("@materialId", MaterialTypeId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 10);

                var materials = _dbContext.tbl_MaterialMaster.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId, @UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToList();
                return materials;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetbyMaterialId(int CompanyId, int Branchid, string MaterialId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Materialmaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@materialId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = MaterialId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectByMaterialId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string itemdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    itemdetails = itemdetails + dataTable.Rows[i][0].ToString();
                }

                return itemdetails;
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
                //List<Material> nestedpurchase = new List<Material>();
                //var purchaselist =await _dbContext.tbl_MaterialMaster.Where(x => x.Id == Id).ToListAsync();
                //var purchasedetaillist =await _dbContext.tbl_OpeningStock.Where(x => x.MaterialId == Id).ToListAsync();
                //return purchaselist;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Materialmaster";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@materialId", SqlDbType.NVarChar) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
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
            //var materialId = new SqlParameter("@materialId", Id);
            //var item = new SqlParameter("@item", "");
            //var CompanyId = new SqlParameter("@CompanyId", "0");
            //var BranchId = new SqlParameter("@BranchId", "0");
            //var Action = new SqlParameter("@Action", Actions.Select);

            //var _product = _dbContext.tbl_MaterialMaster.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
            //return _product;
        }

        public async Task<string> getwithfinancialId(int Id, int FinancialYearId)
            {
                try
                {
                    //List<Material> nestedpurchase = new List<Material>();
                    //var purchaselist =await _dbContext.tbl_MaterialMaster.Where(x => x.Id == Id).ToListAsync();
                    //var purchasedetaillist =await _dbContext.tbl_OpeningStock.Where(x => x.MaterialId == Id).ToListAsync();
                    //return purchaselist;

                    DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                    cmd.CommandText = "dbo.Stpro_Materialmaster";
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@materialId", SqlDbType.NVarChar) { Value = Id });
                    cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = FinancialYearId });
                    cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 12 });
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
            //var materialId = new SqlParameter("@materialId", Id);
            //var item = new SqlParameter("@item", "");
            //var CompanyId = new SqlParameter("@CompanyId", "0");
            //var BranchId = new SqlParameter("@BranchId", "0");
            //var Action = new SqlParameter("@Action", Actions.Select);

            //var _product = _dbContext.tbl_MaterialMaster.FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
            //return _product;
        }

        public async Task<string> GetReport(MaterialSearch materialSearches )
        {
            try
            {
                //    var materialId = new SqlParameter("@materialId", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearches));
                //var CompanyId = new SqlParameter("@CompanyId", materialSearches.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", materialSearches.BranchId );
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectReport );
                //var _product = await _dbContext.tbl_MaterialMasterReport .FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                //return _product;
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Materialmaster";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@materialId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearches) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearches.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearches.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });
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

        public async Task<IEnumerable<MaterialStockReport>> GetStockReport(MaterialSearch materialSearches)
        {
            try
            {
                if (materialSearches.FinancialYearId == null)
                    return null;

                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearches));
                var CompanyId = new SqlParameter("@CompanyId", materialSearches.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearches.BranchId);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectStockReport);
                var _product = await _dbContext.tbl_MaterialStockReport .FromSqlRaw("Stpro_Materialmaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //public async Task<string> GetStockReport(MaterialSearch materialSearch)
        //{
        //    try
        //    {
        //        DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

        //        cmd.CommandText = "dbo.Stpro_Materialmaster";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@materialId", SqlDbType.Int) { Value = 0 });
        //        cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
        //        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
        //        cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
        //        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
        //     cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectStockReport });
        //        if (cmd.Connection.State != ConnectionState.Open)
        //        {
        //            cmd.Connection.Open();
        //        }

        //        DbDataReader reader = await cmd.ExecuteReaderAsync();

        //        var dataTable = new DataTable();
        //        dataTable.Load(reader);
        //        string purcasedetails = "";
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
        //        }
        //        return purcasedetails;


        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckMaterialRegistrationEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetReport(MismatchSearch mismatchSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialRecieve";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(mismatchSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = mismatchSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = mismatchSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<int> TransferId(int BranchId, int FinancialYearId)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialMaxNo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }

        public async Task<int> ReceiveId(int BranchId, int FinancialYearId)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialMaxNo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }

        public async Task<int> ConsumptionId(int BranchId, int FinancialYearId)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialMaxNo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }
        public async Task<int> QuotationId(int BranchId, int FinancialYearId)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_MaterialMaxNo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }

    }
}
