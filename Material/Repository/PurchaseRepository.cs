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
    public class PurchaseRepository : IPurchaseRepository
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
            SelectReportJson =7,
            Selectforview = 8
        }

        public PurchaseRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Purchase> purchase)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchase));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseMaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
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
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseMaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Purchase> purchase)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchase));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_PurchaseMaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Purchase>> Get(int CompanyId, int Branchid)
        {
            try
            {
                if (Branchid == 0)
            {
                var purchaselist =await _dbContext.tbl_PurchaseMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).ToListAsync ();
                foreach (var purdetail in purchaselist)
                {
                    var purchasedetaillist =await _dbContext.tbl_PurchaseDetails.Where(x => x.PurchaseId == purdetail.Id).ToListAsync();
                }

                return purchaselist;
            }
            else
            {
                var purchaselist =await _dbContext.tbl_PurchaseMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).ToListAsync();
                foreach (var purdetail in purchaselist)
                {
                    var purchasedetaillist = await _dbContext.tbl_PurchaseDetails.Where(x => x.PurchaseId == purdetail.Id).ToListAsync();
                }

                return purchaselist;
            }

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }
        public async Task<IEnumerable<Purchase>> GetbyID(int Id)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_PurchaseMaster.Where(x => x.Id == Id).Where(x => x.IsDeleted == 0).ToListAsync();
            var purchasedetaillist =await _dbContext.tbl_PurchaseDetails.Where(x => x.PurchaseId == Id).ToListAsync();
            return purchaselist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


            //try
            //{
            //    var materialId = new SqlParameter("@materialId", Id);
            //    var item = new SqlParameter("@item", "");
            //    var CompanyId = new SqlParameter("@CompanyId", "0");
            //    var BranchId = new SqlParameter("@BranchId","0");
            //    var UserId = new SqlParameter("@UserId", "0");
            //    var Action = new SqlParameter("@Action", 10);
            //    var _product = _dbContext.tbl_PurchaseMaster.FromSqlRaw("Stpro_PurchaseMaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action);
            //    return _product;
            //}
            //catch
            //{
            //    return null;
            //}


        }

        //public async Task<string> GetDetailsbyid(int PurchaseId)
        //{
        //    try
        //    {
        //        var data = await (from a in _dbContext.tbl_PurchaseDetails
        //                          join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
        //                          from b in bs.DefaultIfEmpty()
        //                          join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
        //                          from c in cs.DefaultIfEmpty()
        //                          join d in _dbContext.tbl_PurchaseOrderDetails on a.PurchaseOrderDetailsId equals d.PurchaseOrderDetailId into ps
        //                          from d in ps.DefaultIfEmpty()
        //                          join e in _dbContext.tbl_PurchaseOrderMaster on d.PurchaseOrderId equals e.Id into pss
        //                          from e in pss.DefaultIfEmpty()
        //                          select new
        //                          {
        //                              purchaseDetailId = a.PurchaseDetailId,
        //                              purchaseId = a.PurchaseId,
        //                              materialId = a.MaterialId,
        //                              //materialName=b.MaterialName ,
        //                              materialName = b == null ? String.Empty : b.MaterialName,
        //                              unitId = b.UnitId,
        //                              materialTypeId =  b.MaterialTypeId,
        //                              //  unitShortName = c.UnitShortName  ,
        //                              unitLongName = c == null ? String.Empty : c.UnitLongName,
        //                              unitShortName = c == null ? String.Empty : c.UnitShortName,
        //                              quantity = a.Quantity,
        //                              rate = a.Rate,
        //                              discount = a.Discount,
        //                              tax = a.Tax,
        //                              purchaseOrderDetailsId = a.PurchaseOrderDetailsId,
        //                              orderDate = e == null ? String.Empty : Convert.ToString(e.DateOrdered),
        //                              kFC_Per = a.KFC_Per

        //                          }).Where(x => x.purchaseId == PurchaseId).ToListAsync();
        //        string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
        //        return jsonString;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}


        public async Task<string> GetDetailsbyid(int PurchaseId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_PurchaseDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  join d in _dbContext.tbl_PurchaseOrderDetails on a.PurchaseOrderDetailsId equals d.PurchaseOrderDetailId into ps
                                  from d in ps.DefaultIfEmpty()
                                  join e in _dbContext.tbl_PurchaseOrderMaster on d.PurchaseOrderId equals e.Id into pss
                                  from e in pss.DefaultIfEmpty()
                                  where a.PurchaseId == PurchaseId
                                  orderby a.PurchaseDetailId // or any other column that defines the order
                                  select new
                                  {
                                      purchaseDetailId = a.PurchaseDetailId,
                                      purchaseId = a.PurchaseId,
                                      materialId = a.MaterialId,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      unitId = b.UnitId,
                                      materialTypeId = b.MaterialTypeId,
                                      unitLongName = c == null ? String.Empty : c.UnitLongName,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      quantity = a.Quantity,
                                      rate = a.Rate,
                                      discount = a.Discount,
                                      tax = a.Tax,
                                      purchaseOrderDetailsId = a.PurchaseOrderDetailsId,
                                      orderDate = e == null ? String.Empty : Convert.ToString(e.DateOrdered),
                                      kFC_Per = a.KFC_Per,
                                      childDescription=a.ChildDescription,
                                      materialBrandId = a.MaterialBrandId,
                                      materialCategoryId = a.MaterialCategoryId,
                                      coefficientFactorValue = a.CoefficientFactorValue,
                                      conversionQuantity = a.ConversionQuantity,
                                      conversionUnitName=a.ConversionUnitName
                                  }).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseList>> Getforapproval(int companyId, int branchid,int UserID,int menuid, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var UserId = new SqlParameter("@UserId", UserID);
            var MenuId = new SqlParameter("@MenuId", menuid);
            var Action = new SqlParameter("@Action", Actions.Selectforapproval);

            var _product =await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@MenuId ,@Action", Id, item,  CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid, int menuid)
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var UserId = new SqlParameter("@UserId", "0");
            var MenuId = new SqlParameter("@MenuId", menuid);
            var Action = new SqlParameter("@Action", Actions.SelectforEdit);

            var _product = await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@MenuId,@Action", Id, item, CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid,int userId, int menuid, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var MenuId = new SqlParameter("@MenuId", menuid);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@MenuId,@Action", Id, item, CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseList>> Getforview(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");
                var MenuId = new SqlParameter("@MenuId", materialSearch.MenuId);
                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@MenuId ,@Action", Id, item, CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getforapproval(int companyId, int branchid)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_PurchaseMaster
                        join b in _dbContext.tbl_Suppliers on a.SupplierId equals b.Id
                        join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                        join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                        from d in ds.DefaultIfEmpty()
                        join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                        from e in es.DefaultIfEmpty()
                        join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                        from f in fs.DefaultIfEmpty()

                        select new
                        {
                            Id = a.Id,
                            PurchaseDate = a.PurchaseDate,
                            PurchaseInvoiceNo = a.PurchaseInvoiceNo,
                            PurchaseOrderNo = a.PurchaseOrderNo,
                            SupplierId = a.SupplierId,
                            SupplierName = b.SupplierName,
                            ProjectName = c.ProjectName,
                            ProjectId = a.ProjectId,

                            BlockId = a.BlockId,
                            BlockName = d == null ? " " : d.BlockName,
                            FloorId = a.FloorId,
                            FloorName = e == null ? " " : e.FloorName,
                            UnitId = a.UnitId,
                            UnitName = f == null ? " " : f.UnitId,

                            Remark = a.Remark,
                            Taxarea = a.Taxarea,
                            Category = a.Category,
                            CompanyId = a.CompanyId,
                            BranchId = a.BranchId,
                            ApprovalLevel = a.ApprovalLevel,
                            IsDeleted = a.IsDeleted,
                            ApprovalStatus = a.ApprovalStatus,
                            BillAmount = a.BillAmount,
                            BillAmountBalance = a.BillAmountBalance,
                            AmountPaidAdvance = a.AmountPaidAdvance,
                            billdiscount = a.billdiscount,
                            Roundoff = a.Roundoff,
                            TransportationCharge = a.TransportationCharge,
                            TransportationPer = a.TransportationPer,
                            LoadingUnloadingCharge = a.LoadingUnloadingCharge,
                            LoadingUnloadingPer = a.LoadingUnloadingPer,
                            OtherCharges = a.OtherCharges,
                            OtherChargesPer = a.OtherChargesPer,
                            ReqLoadingTax = a.ReqLoadingTax,
                            ReqTransportTax = a.ReqTransportTax,
                            ReqOtherCharesTax = a.ReqOtherCharesTax,
                            KFCPer = a.KFCPer,
                            GSTPer = a.GSTPer,
                            GSTAmount = a.GSTAmount,
                            KFCAmount = a.KFCAmount,
                            MaterialTypeId = a.MaterialTypeId,
                            PaymentModeId = a.PaymentModeId,
                            SiteManagerId = a.SiteManagerId

                        }).Where(x => x.CompanyId == companyId).Where(x => x.BranchId == branchid).Where(x => x.IsDeleted == 0).Where(x => x.ApprovalStatus == 0).Where(x => x.ApprovalLevel == 0).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
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

                cmd.CommandText = "dbo.Stpro_PurchaseReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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

            //try
            //{
            //    var materialId = new SqlParameter("@materialId", "0");
            //    var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
            //    var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId );
            //    var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId );
            //    var UserId = new SqlParameter("@UserId", "0");
            //    var Action = new SqlParameter("@Action", Actions.SelectReport );
            //    var _product =await _dbContext.tbl_Purchasereport .FromSqlRaw("Stpro_PurchaseMaster @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            //    return _product;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public async Task<string> GetInvoiceNo(int companyId, int branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
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

        public async Task<string> Getjson(MaterialSearch materialSearch)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "dbo.Stpro_PurchaseMaster";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReportJson });
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
            //string jsonString = System.Text.Json.JsonSerializer.Serialize(dataTable.Rows[0][0]);
            //return jsonString;
        }

        public async Task<IEnumerable<MaterialSchedule>> MaterialSchedule (MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", materialSearch.Id);
                var Type = new SqlParameter("@Type", materialSearch.ViewType);
                var json = new SqlParameter("@json", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var Category = new SqlParameter("@Category", "0");
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var fianncialyearid = new SqlParameter("@fianncialyearid","0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var _product = await _dbContext.tbl_materialSchedule.FromSqlRaw("stpro_ScheduleQty @Id,@Type,@json,@ProjectId,@UnitId,@BlockId,@FloorId,@Category, @CompanyId, @BranchId,@fianncialyearid, @Action", Id, Type, json, ProjectId, UnitId, BlockId, FloorId, Category, CompanyId, BranchId, fianncialyearid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> MaterialsSchedule(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_ScheduleQty";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = materialSearch.Id });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = materialSearch.ViewType });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.VarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Category", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Insert });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string materialPurchase = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    materialPurchase = materialPurchase + dataTable.Rows[i][0].ToString();
                }
                return materialPurchase;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReport(AgeingSearch ageingSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseAgeing";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(ageingSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ageingSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = ageingSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
