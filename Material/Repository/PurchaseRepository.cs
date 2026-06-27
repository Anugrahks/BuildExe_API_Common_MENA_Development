using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
            SelectReportJson = 7,
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
                var camelCaseSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                //var debugJson = JsonConvert.SerializeObject(purchase, Formatting.Indented, camelCaseSettings);
                //Logger.ErrorLog(this.GetType().Name, "Insert_DEBUG", new Exception(debugJson));

                var materialId = new SqlParameter("@materialId", "0"); 
                var item = new SqlParameter("@item",JsonConvert.SerializeObject(purchase, camelCaseSettings)
                            );
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
                var jsonParam = new SqlParameter("@json", SqlDbType.NVarChar)
                {
                    Value = DBNull.Value
                };

                var purchaseList = await _dbContext.tbl_validations
                    .FromSqlRaw("Stpro_PurchaseMaster @Id, @json, @CompanyId, @BranchId, @UserId, @Action",
                        new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                        jsonParam,
                        new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 },
                        new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 },
                        new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID },
                        new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Delete })
                    .ToListAsync();

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
                var camelCaseSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchase, camelCaseSettings));
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
                    var purchaselist = await _dbContext.tbl_PurchaseMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_PurchaseDetails.Where(x => x.PurchaseId == purdetail.Id).ToListAsync();
                    }

                    return purchaselist;
                }
                else
                {
                    var purchaselist = await _dbContext.tbl_PurchaseMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == Branchid).ToListAsync();
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
                var purchasedetaillist = await _dbContext.tbl_PurchaseDetails.Where(x => x.PurchaseId == Id).ToListAsync();
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
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_GetPurchaseDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PurchaseId", SqlDbType.Int) { Value = PurchaseId });

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                var ds = new DataSet();
                using (var adapter = new SqlDataAdapter((SqlCommand)cmd))
                {
                    adapter.Fill(ds);
                }

                var dataTable = ds.Tables[0];
                var serviceChargeTable = ds.Tables[1];
                var globalOtherChargesTable = ds.Tables[2];
                var serviceIdsTable = ds.Tables.Count > 3 ? ds.Tables[3] : new DataTable();



                var globalOtherCharges = globalOtherChargesTable.AsEnumerable()
                    .Select(oc => new
                    {
                        chargeName = oc.Field<string>("chargeName"),
                        chargePercentage = oc.Field<decimal?>("chargePercentage"),
                        chargeAmount = oc.Field<decimal?>("chargeAmount"),
                        paymentModeId = oc.Field<int?>("paymentModeId"),
                        supplierId = oc.Field<int?>("supplierId"),
                        withClear = oc.Field<bool?>("withClear") == true ? 1 : 0
                    }).ToList();

                var serviceIdsList = serviceIdsTable.AsEnumerable()
                    .Select(row => row.Field<int>("ServiceId")) 
                    .ToList();

                var result = dataTable.AsEnumerable()
                    .GroupBy(x => x.Field<int?>("Id"))
                    .Select(p =>
                    {
                        var header = p.First();

                        
                        var realPurchaseDetails = p
                            .Where(x => x.Field<int?>("Id") == p.Key)
                            .GroupBy(d => d.Field<int?>("purchaseDetailId"))
                            .Select(dg => new PurchaseDetailDto
                            {
                                purchaseDetailId = dg.Key,
                                purchaseId = p.Key,
                                materialId = dg.First().Field<int?>("materialId") ?? 0,
                                materialName = dg.First().Field<string?>("materialName")??"",
                                materialUnit=dg.First().Field<string?>("materialUnit"),
                                quantity = dg.First().Field<decimal?>("Quantity"),
                                rate = dg.First().Field<decimal?>("Rate"),
                                total = dg.First().Field<decimal?>("Total"),
                                discount = dg.First().Field<decimal?>("Discount"),
                                tax = dg.First().Field<decimal?>("Tax"),
                                kFC_Per = dg.First().Field<decimal?>("KFC_Per"),
                                coefficientFactorValue = dg.First().Field<decimal?>("CoefficientFactorValue"),
                                conversionQuantity = dg.First().Field<decimal?>("ConversionQuantity"),
                                conversionUnitName = dg.First().Field<string?>("ConversionUnitName")??"",
                                materialRemarks = dg.First().Field<string?>("MaterialRemarks")??"",
                                materialCategoryId = dg.First().Field<int?>("MaterialCategoryId"),
                                childDescription = dg.First().Field<string?>("ChildDescription")??"",
                                fCNetAmount = dg.First().Field<decimal?>("FCNetAmount"),
                                lAmount = dg.First().Field<decimal?>("LAmount"),
                                landingCost = dg.First().Field<decimal?>("LandingCost"),
                                amount = dg.First().Field<decimal?>("Total"),
                                purchaseAmount = dg.First().Field<decimal?>("Total"),
                                sgst = 0,
                                cgst = 0,
                                igst = 0,
                                currencyId = dg.First().Field<int?>("CurrencyId"),
                                exchangeRate = dg.First().Field<decimal?>("ExchangeRate"),
                                fCBillAmount = dg.First().Field<decimal?>("FCBillAmount"),
                                isServiceCharge = 0,
                                warrantyDetails = dg
                                    .Where(w => w.Field<int?>("VoucherNumber") != null) 
                                    .Select(w => new
                                    {
                                        serialNo = w.Field<string>("SerialNo"),
                                        warrantyDate = w.Field<DateTime?>("WarrantyDate"),
                                    }).ToList()
                            }).ToList();

                        var serviceChargeDetails = serviceChargeTable.AsEnumerable()
                            .Select(sc => new PurchaseDetailDto
                            {
                                purchaseDetailId = Convert.ToInt32(sc["purchaseDetailId"]),
                                purchaseId = Convert.ToInt32(sc["purchaseId"]),
                                materialId = 0,
                                materialName = Convert.ToString(sc["materialName"]),
                                quantity = 1,
                                rate = Convert.ToDecimal(sc["rate"]),
                                total = Convert.ToDecimal(sc["total"]),
                                discount = 0,
                                tax = 0,
                                kFC_Per = 0,
                                coefficientFactorValue = 0,
                                conversionQuantity = 0,
                                conversionUnitName = "",
                                materialRemarks = "",
                                materialCategoryId = 0,
                                childDescription = null,
                                fCNetAmount = null,
                                lAmount = Convert.ToDecimal(sc["lAmount"]),
                                landingCost = 0,
                                amount = Convert.ToDecimal(sc["amount"]),
                                purchaseAmount = Convert.ToDecimal(sc["purchaseAmount"]),
                                sgst = 0,
                                cgst = 0,
                                igst = 0,
                                currencyId = null,
                                exchangeRate = null,
                                fCBillAmount = null,
                                isServiceCharge = 1,
                                warrantyDetails = new List<object>()
                            }).ToList();

                        
                        var allPurchaseDetails = realPurchaseDetails
                            .Concat(serviceChargeDetails)
                            .ToList();

                        return new
                        {
                            Id = p.Key,
                            purchaseInvoiceNo = header.Field<string>("PurchaseInvoiceNo"),
                            purchaseOrderNo = header.Field<int?>("PurchaseOrderNo"),
                            supplierId = header.Field<int?>("SupplierId"),
                            unitId = header.Field<int?>("UnitId"),
                            blockId = header.Field<int?>("BlockId"),
                            isWareHouse = header.Field<bool?>("IsWareHouse") ?? false,
                            clientUniqueName=header.Field<string?>("ClientUniqueName"),
                            customerId=header.Field<int?>("CustomerId"),
                            floorId = header.Field<int?>("FloorId"),
                            divisionId = header.Field<int?>("DivisionId"),
                            category = header.Field<int?>("Category"),
                            approvalStatus = header.Field<int?>("ApprovalStatus"),
                            approvalLevel = header.Field<int?>("ApprovalLevel"),
                            approvedDate = header.Field<DateTime?>("ApprovedDate"),
                            approvedBy = header.Field<int?>("ApprovedBy"),
                            companyId = header.Field<int?>("CompanyId"),
                            branchId = header.Field<int?>("BranchId"),
                            financialYearId = header.Field<int?>("FinancialYearId"),
                            billdiscount = header.Field<decimal?>("billdiscount"),
                            billdiscountPer = header.Field<decimal?>("billdiscountPer"),
                            materialTypeId = header.Field<int?>("MaterialTypeId"),
                            siteManagerId = header.Field<int?>("SiteManagerId"),
                            approvalRemarks = header.Field<string>("ApprovalRemarks"),
                            isReject = header.Field<int?>("IsReject"),
                            workNameId = header.Field<int?>("WorkNameId"),
                            isPercentage = header.Field<int?>("IsPercentage"),
                            disableFlag = header.Field<int?>("DisableFlag"),
                            disablePercentageFlag = header.Field<int?>("DisablePercentageFlag"),
                            disableAmountFlag = header.Field<int?>("DisableAmountFlag"),
                            userId = header.Field<int?>("UserId"),
                            subcontractorId = header.Field<int?>("SubcontractorId"),
                            temporaryTransitLocation = header.Field<string>("TemporaryTransitLocation"),
                            creditPeriod = header.Field<int?>("CreditPeriod"),
                            bankId = header.Field<int?>("BankId"),
                            chequeDate = header.Field<DateTime?>("ChequeDate"),
                            withClear = header.Field<int?>("WithClear"),
                            isAsset = header.Field<int?>("IsAsset"),
                            handlingChargePer = header.Field<decimal?>("HandlingChargePer"),
                            handlingCharge = header.Field<decimal?>("HandlingCharge"),
                            gstPercentage = header.Field<decimal?>("GstPercentage"),
                            siteLoan = header.Field<int?>("SiteLoan"),
                            storageChargePer = header.Field<decimal?>("StorageChargePer"),
                            storageCharge = header.Field<decimal?>("StorageCharge"),
                            rejectRemarks = header.Field<string>("RejectRemarks"),
                            isAmount = header.Field<int?>("IsAmount"),
                            isOtherCharge = header.Field<int?>("IsOtherCharge"),
                            isLoadingUnloading = header.Field<int?>("IsLoadingUnloading"),
                            isTransportation = header.Field<int?>("IsTransportation"),
                            isGst = header.Field<int?>("IsGst"),
                            tcsPer = header.Field<decimal?>("TcsPer"),
                            tcsAmount = header.Field<decimal?>("TcsAmount"),
                            projectId = header.Field<int?>("ProjectId"),
                            paymentModeId = header.Field<int?>("PaymentModeId"),
                            loadingUnloadingPer = header.Field<decimal?>("LoadingUnloadingPer"),
                            billAmount = header.Field<decimal?>("BillAmount"),
                            billAmountBalance = header.Field<decimal?>("BillAmountBalance"),
                            currencyId = header.Field<int?>("CurrencyId"),
                            exchangeRate = header.Field<decimal?>("ExchangeRate"),
                            loadingUnloadingChargeGst = header.Field<decimal?>("LoadingUnloadingChargeGst"), 
                            purchaseDate = header.Field<DateTime?>("PurchaseDate"),
                            paymentNo = header.Field<string>("PaymentNo"), 
                            transportationCharge = header.Field<decimal?>("TransportationCharge"), 
                            transportationChargeGST = header.Field<decimal?>("TransportationChargeGst"),
                            transportationPer = header.Field<decimal?>("TransportationPer"), 
                            customDuty = header.Field<decimal?>("customDuty"),
                            customDutyPer = header.Field<decimal?>("CustomDutyPer"), 
                            doCharge = header.Field<decimal?>("DoCharge"), 
                            doChargePer = header.Field<decimal?>("DoChargePer"),
                            documentationCharge = header.Field<decimal?>("DocumentationCharge"), 
                            documentationChargePer = header.Field<decimal?>("DocumentationChargePer"), 
                            freightCharge = header.Field<decimal?>("FreightCharge"), 
                            freightChargePer = header.Field<decimal?>("FreightChargePer"),
                            purchasetype=header.Field<int?>("PurchaseType"),
                            loadingUnloadingCharge = header.Field<decimal?>("LoadingUnloadingCharge"),
                            mofaCharge = header.Field<decimal?>("mofaCharge"),
                            mofaChargePer = header.Field<decimal?>("mofaChargePer"),
                            taxarea = header.Field<string>("Taxarea"),
                            amountPaidAdvance = header.Field<decimal?>("AmountPaidAdvance"),
                            roundoff = header.Field<decimal?>("Roundoff"),
                            netAmount = header.Field<decimal?>("NetAmount"),
                            vehicleNo = header.Field<string>("VehicleNo"),
                            siteLoanAmt = header.Field<decimal?>("SiteLoanAmt"),
                            discountWithoutTax = header.Field<decimal?>("DiscountWithoutTax"),
                            otherChargesGst = header.Field<decimal?>("OtherChargesGst"),
                            kFCAmount = header.Field<decimal?>("KFCAmount"),
                            gSTAmount = header.Field<decimal?>("GSTAmount"),
                            gSTPer = header.Field<decimal?>("GSTPer"),
                            kFCPer = header.Field<decimal?>("KFCPer"),
                            remark = header.Field<string>("Remark"),
                            otherCharges = header.Field<decimal?>("OtherCharges"),
                            otherChargesPer = header.Field<decimal?>("OtherChargesPer"),
                            reqLoadingTax = header.Field<string>("ReqLoadingTax"),
                            reqTransportTax = header.Field<string>("ReqTransportTax"),

                            purchaseDeliveryDetail = p
                            .Where(x => x.Field<int?>("Id") == p.Key)
                            .GroupBy(q => q.Field<int?>("Id"))
                            .Select(pdd => new
                            {
                                purchaseId = pdd.Key,

                                materialId = pdd.First().Field<int?>("MaterialId"),

                                materialBrandId = pdd.First().Field<int?>("MaterialBrandId"),

                                orderId = pdd.First().Field<int?>("OrderId"),

                                deliveryOrderDetailsId = pdd.First().Field<int?>("DeliveryOrderDetailsId"),

                                deliveryOrderId = pdd.First().Field<int?>("DeliveryOrderId"),

                                materialName = pdd.First().Field<string>("DeliveryMaterialName"),

                                unitLongName = pdd.First().Field<string>("UnitLongName"),

                                quantity = pdd.First().Field<decimal?>("pdsQuantity"),

                                balanceQty = pdd.First().Field<decimal?>("BalanceQty"),

                                currentQuantity = pdd.First().Field<decimal?>("CurrentQuantity"),

                                rate = pdd.First().Field<decimal?>("pdsRate"),

                                convertionQuantity = pdd.First().Field<decimal?>("ConversionQuantity"),

                                conversionUnitName = pdd.First().Field<string>("ConversionUnitName"),

                                discount = pdd.First().Field<decimal?>("pdsDiscount"),

                                tax = pdd.First().Field<decimal?>("pdsTax"),

                                KFC_Per = pdd.First().Field<decimal?>("pdsKFC_Per"),

                                total = pdd.First().Field<decimal?>("pdstotal"),

                                orderDate = pdd.First()["DeliveryOrderDate"] == DBNull.Value
                                                                    ? (DateTime?)null
                                                                    : Convert.ToDateTime(pdd.First()["DeliveryOrderDate"]),

                                remarks = pdd.First().Field<string>("Remarks"),

                                select = pdd.First().Field<bool?>("Select"),

                                MaterialRemarks = pdd.First().Field<string>("MaterialRemarks"),

                                coefficientFactorValue = pdd.First().Field<decimal?>("CoefficientFactorValue"),
                            }).ToList(),

                            purchaseReturnBill = p
                                .Where(x => x.Field<int?>("Id") == p.Key)
                                .GroupBy(r => r.Field<int?>("Id"))
                                .Select(prbl => new
                                {
                                    adjustedAmount = prbl.First().Field<decimal?>("AdjustedAmount"),
                                }).ToList(),

                            otherCharge = globalOtherCharges,
                            purchaseDetail = allPurchaseDetails,
                            service = serviceIdsList
                        };

                    })
                    .ToList();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(result);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<PurchaseList>> Getforapproval(int companyId, int branchid, int UserID, int menuid, int FinancialYearId, int IsAsset)
        //{
        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(new { IsAsset });
        //        var Id = new SqlParameter("@Id", FinancialYearId);
        //        var item = new SqlParameter("@item", json);
        //        var CompanyId = new SqlParameter("@CompanyId", companyId);
        //        var BranchId = new SqlParameter("@BranchId", branchid);
        //        var UserId = new SqlParameter("@UserId", UserID);
        //        var MenuId = new SqlParameter("@MenuId", menuid);
        //        var Action = new SqlParameter("@Action", Actions.Selectforapproval);

        //        var _product = await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@MenuId ,@Action", Id, item, CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<PurchaseList>> Getforapproval(int companyId, int branchid, int UserID, int menuid, int FinancialYearId, int IsAsset)
        {
            try
            {
                var purchaseList = new List<PurchaseList>();

                using (var connection = new SqlConnection(_dbContext.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("Stpro_PurchaseMasterForApproval", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@json", JsonConvert.SerializeObject(new { IsAsset }));
                        command.Parameters.AddWithValue("@Id", FinancialYearId);
                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@BranchId", branchid);
                        command.Parameters.AddWithValue("@UserId", UserID);
                        command.Parameters.AddWithValue("@MenuId", menuid);
                        command.Parameters.AddWithValue("@Action", 5);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var purchase = new PurchaseList
                                {
                                    Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    PurchaseInvoiceNo = reader["PurchaseInvoiceNo"]?.ToString() ?? "",
                                    PurchaseOrderNo = reader["PurchaseOrderNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PurchaseOrderNo"]),
                                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["PurchaseDate"]),
                                    SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader["SupplierName"]?.ToString() ?? "",
                                    ProjectId = reader["ProjectId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProjectId"]),
                                    ProjectName = reader["ProjectName"]?.ToString() ?? "",
                                    UnitId = reader["UnitId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UnitId"]),
                                    BlockId = reader["BlockId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BlockId"]),
                                    FloorId = reader["FloorId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FloorId"]),
                                    DivisionId = reader["DivisionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DivisionId"]),
                                    CompanyId = reader["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CompanyId"]),
                                    BranchId = reader["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BranchId"]),
                                    FinancialYearId = reader["FinancialYearId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FinancialYearId"]),
                                    Remark = reader["Remark"]?.ToString() ?? "",
                                    Taxarea = reader["Taxarea"]?.ToString() ?? "",
                                    Category = reader["Category"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Category"]),
                                    ApprovalStatus = reader["ApprovalStatus"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ApprovalStatus"]),
                                    ApprovalLevel = reader["ApprovalLevel"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ApprovalLevel"]),
                                    ApprovedBy = reader["ApprovedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ApprovedBy"]),
                                    BillAmount = reader["BillAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BillAmount"]),
                                    BillAmountBalance = reader["BillAmountBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BillAmountBalance"]),
                                    NetAmount = reader["NetAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["NetAmount"]),
                                    PaymentModeId = reader["PaymentModeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PaymentModeId"]),
                                    MaterialTypeId = reader["MaterialTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaterialTypeId"]),
                                    DisableFlag = reader["DisableFlag"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DisableFlag"]),
                                    IsGst = reader["IsGst"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsGst"]),
                                    IsTransportation = reader["IsTransportation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsTransportation"]),
                                    IsLoadingUnloading = reader["IsLoadingUnloading"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsLoadingUnloading"]),
                                    IsOtherCharge = reader["IsOtherCharge"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsOtherCharge"]),
                                    IsAmount = reader["IsAmount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsAmount"]),
                                    IsPercentage = reader["IsPercentage"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsPercentage"]),
                                    WithClear = reader["WithClear"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WithClear"]),
                                    Currency = reader["Currency"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Currency"]),
                                    ExchangeRate = reader["ExchangeRate"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["ExchangeRate"]),
                                    LAmount = reader["LAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["LAmount"]),

                                    PurchaseDetail = new List<PurchaseDetail>(),
                                    OtherCharge = new List<PurchaseOtherCharge>(),
                                    Service = new List<int>(),
                                    PurchaseReturnBill = new List<BuildExeMaterialServices.Models.PurchaseReturnBill>()
                                };
                                purchaseList.Add(purchase);
                            }

                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                parent.PurchaseDetail.Add(new PurchaseDetail
                                {
                                    PurchaseDetailId = reader.GetInt32(reader.GetOrdinal("PurchaseDetailId")),
                                    PurchaseId = purchaseId,
                                    MaterialId = reader["MaterialId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaterialId"]),
                                    MaterialName = reader["MaterialName"]?.ToString() ?? "",
                                    Quantity = reader["Quantity"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Quantity")),
                                    Rate = reader["Rate"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Rate")),
                                    Discount = reader["Discount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Discount")),
                                    Tax = reader["Tax"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Tax")),
                                    Total = reader["Total"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Total")),
                                    Amount = reader["Amount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    LAmount = reader["LAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("LAmount")),
                                    LandingCost = reader["LandingCost"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("LandingCost")),
                                    IsServiceCharge = reader["isServiceCharge"] == DBNull.Value ? 0 : Convert.ToInt32(reader["isServiceCharge"]),
                                    MaterialRemarks = reader["MaterialRemarks"]?.ToString() ?? ""
                                });
                            }

                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                int isServiceCharge = reader["IsServiceCharge"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsServiceCharge"]);

                                if (isServiceCharge == 1)
                                {
                                    parent.PurchaseDetail.Add(new PurchaseDetail
                                    {
                                        PurchaseDetailId = reader.GetInt32(reader.GetOrdinal("Id")),
                                        PurchaseId = purchaseId,
                                        MaterialId = 0,
                                        MaterialName = reader["ChargeName"]?.ToString() ?? "Service Charge",
                                        Quantity = 1,
                                        Rate = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        Discount = 0,
                                        Tax = reader["ChargePercentage"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargePercentage")),
                                        Total = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        Amount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        LAmount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        LandingCost = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        IsServiceCharge = 1,
                                        MaterialRemarks = "Mapped from Other Charges Table"
                                    });
                                }
                                else
                                {
                                    parent.OtherCharge.Add(new PurchaseOtherCharge
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        PurchaseId = purchaseId,
                                        ChargeName = reader["ChargeName"]?.ToString() ?? "",
                                        ChargePercentage = reader["ChargePercentage"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargePercentage")),
                                        ChargeAmount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        PaymentModeId = reader["PaymentModeId"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("PaymentModeId")),
                                        SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                        WithClear = reader["WithClear"] == DBNull.Value ? 0 : (Convert.ToBoolean(reader["WithClear"]) ? 1 : 0),
                                        IsServiceCharge = 0
                                    });
                                }
                            }

                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                if (reader["ServiceId"] != DBNull.Value)
                                {
                                    parent.Service.Add(reader.GetInt32(reader.GetOrdinal("ServiceId")));
                                }
                            }
                        }
                    }
                }

                return purchaseList;
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

        //public async Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid, int userId, int menuid, int FinancialYearId, int IsAsset)
        //{
        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(new { IsAsset });
        //        var Id = new SqlParameter("@Id", FinancialYearId);
        //        var item = new SqlParameter("@item", json);
        //        var CompanyId = new SqlParameter("@CompanyId", companyId);
        //        var BranchId = new SqlParameter("@BranchId", branchid);
        //        var UserId = new SqlParameter("@UserId", userId);
        //        var MenuId = new SqlParameter("@MenuId", menuid);
        //        var Action = new SqlParameter("@Action", Actions.SelectforEdit);

        //        var _product = await _dbContext.tbl_PurchaseMasterList.FromSqlRaw("Stpro_PurchaseMasterForApproval @Id,@item, @CompanyId, @BranchId,@UserId ,@MenuId,@Action", Id, item, CompanyId, BranchId, UserId, MenuId, Action).ToListAsync();
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<PurchaseList>> GetforEdit(int companyId, int branchid, int userId, int menuid, int FinancialYearId, int IsAsset)
        {
            try
            {
                var purchaseList = new List<PurchaseList>();

                using (var connection = new SqlConnection(_dbContext.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("Stpro_PurchaseMasterForApproval", connection))
                    {
                       command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@json", JsonConvert.SerializeObject(new { IsAsset }));
                        command.Parameters.AddWithValue("@Id", FinancialYearId);
                        command.Parameters.AddWithValue("@CompanyId", companyId);
                        command.Parameters.AddWithValue("@BranchId", branchid);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@MenuId", menuid);
                        command.Parameters.AddWithValue("@Action", 4); 

                             using (var reader = await command.ExecuteReaderAsync())
                          {
                           
                            while (await reader.ReadAsync())
                            {
                                var purchase = new PurchaseList
                                {
                                    Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    PurchaseInvoiceNo = reader["PurchaseInvoiceNo"]?.ToString() ?? "",

                                    PurchaseOrderNo = reader["PurchaseOrderNo"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["PurchaseOrderNo"]),

                                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value
    ? (DateTime?)null
    : Convert.ToDateTime(reader["PurchaseDate"]),

                                    SupplierId = reader["SupplierId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["SupplierId"]),

                                    SupplierName = reader["SupplierName"]?.ToString() ?? "",

                                    ProjectId = reader["ProjectId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["ProjectId"]),

                                    ProjectName = reader["ProjectName"]?.ToString() ?? "",

                                    UnitId = reader["UnitId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["UnitId"]),

                                    BlockId = reader["BlockId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["BlockId"]),

                                    FloorId = reader["FloorId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["FloorId"]),

                                    DivisionId = reader["DivisionId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["DivisionId"]),

                                    CompanyId = reader["CompanyId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["CompanyId"]),

                                    BranchId = reader["BranchId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["BranchId"]),

                                    FinancialYearId = reader["FinancialYearId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["FinancialYearId"]),

                                    Remark = reader["Remark"]?.ToString() ?? "",

                                    Taxarea = reader["Taxarea"]?.ToString() ?? "",

                                    Category = reader["Category"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["Category"]),

                                    ApprovalStatus = reader["ApprovalStatus"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["ApprovalStatus"]),

                                    ApprovalLevel = reader["ApprovalLevel"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["ApprovalLevel"]),

                                    ApprovedBy = reader["ApprovedBy"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["ApprovedBy"]),

                                    BillAmount = reader["BillAmount"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["BillAmount"]),

                                    BillAmountBalance = reader["BillAmountBalance"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["BillAmountBalance"]),

                                    NetAmount = reader["NetAmount"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["NetAmount"]),

                                    PaymentModeId = reader["PaymentModeId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["PaymentModeId"]),

                                    MaterialTypeId = reader["MaterialTypeId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["MaterialTypeId"]),

                                    DisableFlag = reader["DisableFlag"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["DisableFlag"]),

                                    IsGst = reader["IsGst"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsGst"]),

                                    IsTransportation = reader["IsTransportation"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsTransportation"]),

                                    IsLoadingUnloading = reader["IsLoadingUnloading"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsLoadingUnloading"]),

                                    IsOtherCharge = reader["IsOtherCharge"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsOtherCharge"]),

                                    IsAmount = reader["IsAmount"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsAmount"]),

                                    IsPercentage = reader["IsPercentage"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["IsPercentage"]),

                                    WithClear = reader["WithClear"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["WithClear"]),

                                    Currency = reader["Currency"] == DBNull.Value
    ? (int?)null
    : Convert.ToInt32(reader["Currency"]),

                                    ExchangeRate = reader["ExchangeRate"] == DBNull.Value
    ? (decimal?)null
    : Convert.ToDecimal(reader["ExchangeRate"]),

                                    LAmount = reader["LAmount"] == DBNull.Value
    ? (decimal?)null
    : Convert.ToDecimal(reader["LAmount"]),

                                    PurchaseDetail = new List<PurchaseDetail>(),
                                    OtherCharge = new List<PurchaseOtherCharge>(),
                                    Service = new List<int>()
                                };
                                purchaseList.Add(purchase);
                            }

                           
                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                parent.PurchaseDetail.Add(new PurchaseDetail
                                {
                                    PurchaseDetailId = reader.GetInt32(reader.GetOrdinal("PurchaseDetailId")),
                                    PurchaseId = purchaseId,
                                    MaterialId = reader["MaterialId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaterialId"]),
                                    MaterialName = reader["MaterialName"]?.ToString() ?? "",
                                    Quantity = reader["Quantity"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Quantity")),
                                    Rate = reader["Rate"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Rate")),
                                    Discount = reader["Discount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Discount")),
                                    Tax = reader["Tax"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Tax")),
                                    Total = reader["Total"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Total")),
                                    Amount = reader["Amount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    LAmount = reader["LAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("LAmount")),
                                    LandingCost = reader["LandingCost"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("LandingCost")),
                                    IsServiceCharge = reader["isServiceCharge"] == DBNull.Value ? 0 : Convert.ToInt32(reader["isServiceCharge"]),
                                    MaterialRemarks = reader["MaterialRemarks"]?.ToString() ?? ""
                                });
                            }

                            
                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                int isServiceCharge = reader["IsServiceCharge"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsServiceCharge"]);

                                if (isServiceCharge == 1)
                                {
                                    
                                    parent.PurchaseDetail.Add(new PurchaseDetail
                                    {
                                        
                                        PurchaseDetailId = reader.GetInt32(reader.GetOrdinal("Id")),
                                        PurchaseId = purchaseId,                      
                                        MaterialId =  0,
                                        MaterialName = reader["ChargeName"]?.ToString() ?? "Service Charge",
                                        Quantity = 1, 
                                        Rate = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        Discount = 0,
                                        Tax = reader["ChargePercentage"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargePercentage")), 

                                        Total = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        Amount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        LAmount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        LandingCost = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),

                                        IsServiceCharge = 1, 
                                        MaterialRemarks = "Mapped from Other Charges Table"
                                    });
                                }
                                else
                                {
                                   
                                    parent.OtherCharge.Add(new PurchaseOtherCharge
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        PurchaseId = purchaseId,
                                        ChargeName = reader["ChargeName"]?.ToString() ?? "",
                                        ChargePercentage = reader["ChargePercentage"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargePercentage")),
                                        ChargeAmount = reader["ChargeAmount"] == DBNull.Value ? 0 : reader.GetDecimal(reader.GetOrdinal("ChargeAmount")),
                                        PaymentModeId = reader["PaymentModeId"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("PaymentModeId")),
                                        SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                        WithClear = reader["WithClear"] == DBNull.Value ? 0 : (Convert.ToBoolean(reader["WithClear"]) ? 1 : 0),
                                        IsServiceCharge = 0
                                    });
                                }
                            }

                          
                            await reader.NextResultAsync();
                            while (await reader.ReadAsync())
                            {
                                var purchaseId = reader.GetInt32(reader.GetOrdinal("PurchaseId"));
                                var parent = purchaseList.FirstOrDefault(p => p.Id == purchaseId);
                                if (parent == null) continue;

                                if (reader["ServiceId"] != DBNull.Value)
                                {
                                    parent.Service.Add(reader.GetInt32(reader.GetOrdinal("ServiceId")));
                                }
                            }
                        }
                    }
                }

                return purchaseList;
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
                var data = await (from a in _dbContext.tbl_PurchaseMaster
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

        public async Task<IEnumerable<MaterialSchedule>> MaterialSchedule(MaterialSearch materialSearch)
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
                var fianncialyearid = new SqlParameter("@fianncialyearid", "0");
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
