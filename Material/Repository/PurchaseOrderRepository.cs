using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectforReport = 6,
            SelectMaxOrderNo = 7,
            SelectMaxOrderNofin = 8,
            SelectpendingPurchaseOrder = 9,
            SelectpendingPurchaseOrderdetails = 10,
            Selectforview = 11,
        }

        public PurchaseOrderRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<PurchaseOrder> purchaseOrder)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchaseOrder));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int Userid)
        {
            try
            {
                var PId = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", Userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<PurchaseOrder> purchaseOrder)
        {
            try
            {
                var PId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchaseOrder));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId,@UserId, @Action", PId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrder>> GetCombo(int ProjectId, int UnitId, int BlockId, int FloorId, int SupplierId)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_PurchaseOrderMaster.Where(x => x.IsDeleted == 0).Where(x => x.ProjectId == ProjectId).Where(x => x.UnitId == UnitId).Where(x => x.BlockId == BlockId).Where(x => x.FloorId == FloorId).Where(x => x.SupplierPreffered == SupplierId).Where(x => x.ApprovedStatus == 1).Where(x => x.PurchaseFlag == 0).ToListAsync();
                return purchaselist;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PurchaseOrder>> Get(int CompanyId, int Branchid)
        {
            try
            {
                if (Branchid == 0)
                {
                    var purchaselist = await _dbContext.tbl_PurchaseOrderMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_PurchaseOrderDetails.Where(x => x.PurchaseOrderId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
                else
                {
                    var purchaselist = await _dbContext.tbl_PurchaseOrderMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_PurchaseOrderDetails.Where(x => x.PurchaseOrderId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //var PId = new SqlParameter("@Id", "0");
            //var item = new SqlParameter("@item", "");
            //var CompanyId = new SqlParameter("@CompanyId", "0");
            //var BranchId = new SqlParameter("@BranchId", "0");
            //var Action = new SqlParameter("@Action", Actions.SelectAll);

            //var _product = _dbContext.tbl_PurchaseOrderMaster.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId, @Action", PId, item, CompanyId, BranchId, Action).ToList();
            //return _product;
        }
        public async Task<IEnumerable<PurchaseOrder>> GetbyID(int Id)
        {
            try
            {
                // List<Material> nestedpurchase = new List<Material>();
                var purchaselist = await _dbContext.tbl_PurchaseOrderMaster.Where(x => x.Id == Id).ToListAsync();
                var purchasedetaillist = await _dbContext.tbl_PurchaseOrderDetails.Where(x => x.PurchaseOrderId == Id).ToListAsync();
                return purchaselist;
                //var PId = new SqlParameter("@Id", Id);
                //var item = new SqlParameter("@item", "");
                //var CompanyId = new SqlParameter("@CompanyId", "0");
                //var BranchId = new SqlParameter("@BranchId", "0");
                //var Action = new SqlParameter("@Action", Actions.Select);

                //var _product = _dbContext.tbl_PurchaseOrderMaster.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId, @Action", PId, item, CompanyId, BranchId, Action).ToList();
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrderList>> GetforApproval(int companyId, int branchid, int UserID, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);

                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_PurchaseOrderMasterlist.FromSqlRaw("Stpro_PurchaseorderForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PurchaseOrderList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_PurchaseOrderMasterlist.FromSqlRaw("Stpro_PurchaseorderForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrderList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userid);

                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_PurchaseOrderMasterlist.FromSqlRaw("Stpro_PurchaseorderForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PurchaseOrderList>> Getforview(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");

                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_PurchaseOrderMasterlist.FromSqlRaw("Stpro_PurchaseorderForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDetailsbyid(int PurchaseOrderId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_PurchaseOrderDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.ItemId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  select new
                                  {
                                      purchaseOrderDetailId = a.PurchaseOrderDetailId,
                                      purchaseOrderId = a.PurchaseOrderId,
                                      indentId = a.IndentId,
                                      itemId = a.ItemId,
                                      // materialName = b.MaterialName,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      unitId = b.UnitId,
                                      materialTypeId = b.MaterialTypeId,
                                      //unitShortName = c.UnitShortName,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      quantityOrdered = a.QuantityOrdered,
                                      quantityPurchased = a.QuantityPurchased,
                                      itemRate = a.ItemRate,
                                      disount = a.Disount,
                                      tax = a.Tax,
                                      remarks = a.Remarks,
                                      materialBrandId = a.MaterialBrandId,
                                      materialRemarks = a.MaterialRemarks,
                                      materialCategoryId = a.MaterialCategoryId,
                                      coefficientFactorValue = a.CoefficientFactorValue,
                                      conversionQuantity = a.ConversionQuantity,
                                      conversionUnitName = a.ConversionUnitName,
                                      totalAmount = a.TotalAmount,

                                  }).Where(x => x.purchaseOrderId == PurchaseOrderId).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetReport(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforReport });
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
                //var Id = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                //var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId );
                //var UserId = new SqlParameter("@UserId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectforReport );

                //var _product = _dbContext.tbl_PurchaseOrderMasterlist.FromSqlRaw("Stpro_PurchaseOrder @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action);
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetpendingpurchaseOrder(int ProjectId, int UnitId, int BlockId,
            int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrderForpurchase";
                cmd.CommandType = CommandType.StoredProcedure;
                string[] data = PurchaseDate.ToString().Split(" ");
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Supplierid", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OrderCategoryId", SqlDbType.Int) { Value = OrderCategoryId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId ", SqlDbType.Int) { Value = WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@PurchaseDate ", SqlDbType.Date) { Value = data[0] });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectpendingPurchaseOrder });
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

        public async Task<string> GetpendingpurchaseOrder(int ProjectId, int UnitId, int BlockId,
    int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrderForpurchase";
                cmd.CommandType = CommandType.StoredProcedure;
                string[] data = PurchaseDate.ToString().Split(" ");
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = MaterialTypeId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Supplierid", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OrderCategoryId", SqlDbType.Int) { Value = OrderCategoryId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId ", SqlDbType.Int) { Value = WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@PurchaseDate ", SqlDbType.Date) { Value = data[0] });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 12 });
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

        public async Task<string> DeliveryOrder(int ProjectId, int UnitId, int BlockId,
int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrderForpurchase";
                cmd.CommandType = CommandType.StoredProcedure;
                string[] data = PurchaseDate.ToString().Split(" ");
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = MaterialTypeId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Supplierid", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OrderCategoryId", SqlDbType.Int) { Value = OrderCategoryId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId ", SqlDbType.Int) { Value = WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@PurchaseDate ", SqlDbType.Date) { Value = data[0] });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 13 });
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


        public async Task<string> DeliveryOrderForPurchase(int ProjectId, int UnitId, int BlockId,
int FloorId, int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrderForpurchase";
                cmd.CommandType = CommandType.StoredProcedure;
                string[] data = PurchaseDate.ToString().Split(" ");
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = MaterialTypeId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Supplierid", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OrderCategoryId", SqlDbType.Int) { Value = OrderCategoryId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId ", SqlDbType.Int) { Value = WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@PurchaseDate ", SqlDbType.Date) { Value = data[0] });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 14 });
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




        public async Task<string> GetpendingpurchaseOrderdetails(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrderForpurchase";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@unitid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Supplierid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@OrderCategoryId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId ", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@PurchaseDate ", SqlDbType.Date) { Value = "2022-01-01" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectpendingPurchaseOrderdetails });
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
                return purcasedetails;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<int> GetMaxOrderId(int CompanyId, int Branchid, int financialyear)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = financialyear });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectMaxOrderNofin });
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


        public async Task<int> GetMaxOrderIdDelivery(int CompanyId, int Branchid, int financialyear)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = financialyear });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 9 });
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


        public async Task<int> GetMaxOrderId(int CompanyId, int Branchid)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectMaxOrderNo });
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

        public async Task<string> GetPONo(int companyId, int branchid)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseorderForApproval";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });

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


