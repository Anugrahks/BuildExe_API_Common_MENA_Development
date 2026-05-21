using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonProperty("purchaseInvoiceNo")]
        public string PurchaseInvoiceNo { get; set; }

        [JsonProperty("purchaseOrderNo")]
        public int PurchaseOrderNo { get; set; }

        [JsonProperty("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("supplierId")]
        public int SupplierId { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [JsonProperty("unitId")]
        public int UnitId { get; set; }

        [JsonProperty("blockId")]
        public int BlockId { get; set; }

        [JsonProperty("floorId")]
        public int FloorId { get; set; }

        [JsonProperty("divisionId")]
        public int DivisionId { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("taxarea")]
        public string Taxarea { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("approvalStatus")]
        public Int16 ApprovalStatus { get; set; }

        [JsonProperty("approvalLevel")]
        public Int16 ApprovalLevel { get; set; }

        [JsonProperty("approvedDate")]
        public DateTime ApprovedDate { get; set; }

        [JsonProperty("approvedBy")]
        public Int16 ApprovedBy { get; set; }

        [JsonProperty("companyId")]
        public Int16 CompanyId { get; set; }

        [JsonProperty("branchId")]
        public Int16 BranchId { get; set; }

        [JsonProperty("financialYearId")]
        public Int16 FinancialYearId { get; set; }

        [JsonProperty("voucherTypeId")]
        public int VoucherTypeId { get; set; }

        [JsonProperty("voucherNumber")]
        public int VoucherNumber { get; set; }

        [JsonProperty("isDeleted")]
        public Int16 IsDeleted { get; set; }

        [JsonProperty("billAmount")]
        public decimal? BillAmount { get; set; }

        [JsonProperty("billAmountBalance")]
        public decimal BillAmountBalance { get; set; }

        [JsonProperty("amountPaidAdvance")]
        public decimal AmountPaidAdvance { get; set; }

        [JsonProperty("billdiscount")]
        public decimal billdiscount { get; set; }

        [JsonProperty("billdiscountPer")]
        public decimal? billdiscountPer { get; set; }

        [JsonProperty("roundoff")]
        public decimal Roundoff { get; set; }

        [JsonProperty("transportationCharge")]
        public decimal TransportationCharge { get; set; }

        [JsonProperty("transportationPer")]
        public decimal TransportationPer { get; set; }

        [JsonProperty("loadingUnloadingCharge")]
        public decimal LoadingUnloadingCharge { get; set; }

        [JsonProperty("loadingUnloadingPer")]
        public decimal LoadingUnloadingPer { get; set; }

        [JsonProperty("otherCharges")]
        public decimal OtherCharges { get; set; }

        [JsonProperty("otherChargesPer")]
        public decimal OtherChargesPer { get; set; }

        [JsonProperty("customDutyPer")]
        public decimal customDutyPer { get; set; }

        [JsonProperty("customDuty")]
        public decimal customDuty { get; set; }

        [JsonProperty("doChargePer")]
        public decimal doChargePer { get; set; }

        [JsonProperty("doCharge")]
        public decimal doCharge { get; set; }

        [JsonProperty("handlingChargePer")]
        public decimal handlingChargePer { get; set; }

        [JsonProperty("handlingCharge")]
        public decimal handlingCharge { get; set; }

        [JsonProperty("documentationChargePer")]
        public decimal documentationChargePer { get; set; }

        [JsonProperty("documentationCharge")]
        public decimal documentationCharge { get; set; }

        [JsonProperty("mofaChargePer")]
        public decimal mofaChargePer { get; set; }

        [JsonProperty("mofaCharge")]
        public decimal mofaCharge { get; set; }

        [JsonProperty("storageChargePer")]
        public decimal storageChargePer { get; set; }

        [JsonProperty("storageCharge")]
        public decimal storageCharge { get; set; }

        [JsonProperty("freightChargePer")]
        public decimal freightChargePer { get; set; }

        [JsonProperty("freightCharge")]
        public decimal freightCharge { get; set; }

        [JsonProperty("reqLoadingTax")]
        public string? ReqLoadingTax { get; set; }

        [JsonProperty("reqTransportTax")]
        public string? ReqTransportTax { get; set; }

        [JsonProperty("reqOtherCharesTax")]
        public string? ReqOtherCharesTax { get; set; }

        [JsonProperty("kFCPer")]
        public decimal KFCPer { get; set; }

        [JsonProperty("gSTPer")]
        public decimal GSTPer { get; set; }

        [JsonProperty("gSTAmount")]
        public decimal GSTAmount { get; set; }

        [JsonProperty("kFCAmount")]
        public decimal KFCAmount { get; set; }

        [JsonProperty("materialTypeId")]
        public Int16 MaterialTypeId { get; set; }

        [JsonProperty("paymentModeId")]
        public Int16 PaymentModeId { get; set; }

        [JsonProperty("siteManagerIdS")]
        public int SiteManagerId { get; set; }

        [JsonProperty("approvalRemarks")]
        public string? ApprovalRemarks { get; set; }

        [JsonProperty("rejectRemarks")]
        public string? RejectRemarks { get; set; }

        [JsonProperty("isReject")]
        public int IsReject { get; set; }

        [JsonProperty("workNameId")]
        public int? WorkNameId { get; set; }

        [JsonProperty("netAmount")]
        public decimal? NetAmount { get; set; }

        [JsonProperty("tcsPer")]
        public decimal? TcsPer { get; set; }

        [JsonProperty("tcsAmount")]
        public decimal? TcsAmount { get; set; }

        [JsonProperty("gstPercentage")]
        public decimal? GstPercentage { get; set; }

        [JsonProperty("isGst")]
        public int IsGst { get; set; }

        [JsonProperty("isTransportation")]
        public int IsTransportation { get; set; }

        [JsonProperty("isLoadingUnloading")]
        public int IsLoadingUnloading { get; set; }

        [JsonProperty("isOtherCharge")]
        public int IsOtherCharge { get; set; }

        [JsonProperty("isPercentage")]
        public int? IsPercentage { get; set; }

        [JsonProperty("isAmount")]
        public int? IsAmount { get; set; }

        [JsonProperty("disableFlag")]
        public int DisableFlag { get; set; }

        [JsonProperty("disablePercentageFlag")]
        public int DisablePercentageFlag { get; set; }

        [JsonProperty("disableAmountFlag")]
        public int DisableAmountFlag { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("subcontractorId")]
        public int SubcontractorId { get; set; }

        [JsonProperty("temporaryTransitLocation")]
        public string? TemporaryTransitLocation { get; set; }

        [JsonProperty("creditPeriod")]
        public int CreditPeriod { get; set; }

        [JsonProperty("vehicleNo")]
        public string VehicleNo { get; set; }

        [JsonProperty("siteLoan")]
        public int SiteLoan { get; set; }

        [JsonProperty("siteLoanAmt")]
        public decimal SiteLoanAmt { get; set; }

        [JsonProperty("discountWithoutTax")]
        public decimal DiscountWithoutTax { get; set; }

        [JsonProperty("service")]
        public List<int> Service { get; set; }

        [JsonProperty("otherCharge")]
        public List<PurchaseOtherCharge> OtherCharge { get; set; }

        [JsonProperty("purchaseDetail")]
        public List<PurchaseDetail> PurchaseDetail { get; set; }

        [JsonProperty("purchaseDeliveryDetail")]
        public List<PurchaseDeliveryDetail> PurchaseDeliveryDetail { get; set; }

        [JsonProperty("purchaseReturnBill")]
        public List<PurchaseReturnBill> PurchaseReturnBill { get; set; }

        [JsonProperty("bankId")]
        public int? BankId { get; set; }

        [JsonProperty("paymentNo")]
        public string? PaymentNo { get; set; }

        [JsonProperty("chequeDate")]
        public DateTime? ChequeDate { get; set; }

        [JsonProperty("withClear")]
        public int? WithClear { get; set; }

        [JsonProperty("isAsset")]
        public int IsAsset { get; set; }

        [JsonProperty("transportationChargeGst")]
        public decimal TransportationChargeGst { get; set; }

        [JsonProperty("loadingUnloadingChargeGst")]
        public decimal LoadingUnloadingChargeGst { get; set; }

        [JsonProperty("otherChargesGst")]
        public decimal OtherChargesGst { get; set; }

        [JsonProperty("fromDeliveryOrder")]
        public int FromDeliveryOrder { get; set; }

        [JsonProperty("currencyId")]
        public int? CurrencyId { get; set; }

        [JsonProperty("currency")]
        public int? Currency { get; set; }

        [JsonProperty("exchangeRate")]
        public decimal? ExchangeRate { get; set; }

        [JsonProperty("fCBillAmount")]
        public decimal? FCBillAmount { get; set; }

        [JsonProperty("fCBillAmountBalance")]
        public decimal? FCBillAmountBalance { get; set; }

        [JsonProperty("fCNetAmount")]
        public decimal? FCNetAmount { get; set; }

        [JsonProperty("lAmount")]
        public decimal LAmount { get; set; }
    }

    public class PurchaseOtherCharge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PurchaseId { get; set; }

        [JsonProperty("chargeName")]
        public string ChargeName { get; set; }

        [JsonProperty("chargePercentage")]
        public decimal ChargePercentage { get; set; }

        [JsonProperty("chargeAmount")]
        public decimal ChargeAmount { get; set; }

        [JsonProperty("paymentModeId")]
        public int PaymentModeId { get; set; }

        [JsonProperty("supplierId")]
        public int SupplierId { get; set; }

        [JsonProperty("withClear")]
        public int WithClear { get; set; }

        [JsonProperty("isServiceCharge")]
        public int IsServiceCharge { get; set; }
    }
    public class PurchaseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PurchaseDetailId { get; set; }

        [JsonProperty("purchaseId")]
        public int? PurchaseId { get; set; }

        [JsonProperty("materialId")]
        public int? MaterialId { get; set; }

        [JsonProperty("materialBrandId")]
        public int? MaterialBrandId { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("tax")]
        public decimal Tax { get; set; }

        [JsonProperty("purchaseOrderDetailsId")]
        public int? PurchaseOrderDetailsId { get; set; }

        [JsonProperty("KFC_Per")]
        public decimal KFC_Per { get; set; }

        [JsonProperty("sgst")]
        public decimal? Sgst { get; set; }

        [JsonProperty("cgst")]
        public decimal? Cgst { get; set; }

        [JsonProperty("igst")]
        public decimal? Igst { get; set; }

        [JsonProperty("purchaseAmount")]
        public decimal? PurchaseAmount { get; set; }

        [JsonProperty("total")]
        public decimal? Total { get; set; }

        [JsonProperty("ChildDescription")]
        public string ChildDescription { get; set; }

        [JsonProperty("materialRemarks")]
        public string MaterialRemarks { get; set; }

        [JsonProperty("materialCategoryId")]
        public int? MaterialCategoryId { get; set; }

        [JsonProperty("coefficientFactorValue")]
        public decimal CoefficientFactorValue { get; set; }

        [JsonProperty("conversionQuantity")]
        public decimal ConversionQuantity { get; set; }

        [JsonProperty("conversionUnitName")]
        public string ConversionUnitName { get; set; }

        [JsonProperty("FCTotal")]
        public decimal? FCTotal { get; set; }

        [JsonProperty("CurrencyId")]
        public int? CurrencyId { get; set; }

        [JsonProperty("ExchangeRate")]
        public decimal? ExchangeRate { get; set; }

        [JsonProperty("lamount")]
        public decimal LAmount { get; set; }

        [JsonProperty("materialName")]
        public string MaterialName { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("isServiceCharge")]
        public int? IsServiceCharge { get; set; }

        [JsonProperty("LandingCost")]
        public decimal? LandingCost { get; set; }

        [JsonProperty("FCBillAmount")]
        public decimal? FCBillAmount { get; set; }

        [JsonProperty("FCNetAmount")]
        public decimal? FCNetAmount { get; set; }

        [JsonProperty("WarrantyDetails")]
        public List<MaterialWarrantyDetails> WarrantyDetails { get; set; }
    }


    public class PurchaseDeliveryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseDeliveryDetailId { get; set; }
        public int PurchaseId { get; set; }
        public int MaterialId { get; set; }

        public int MaterialBrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public int DeliveryOrderDetailsId { get; set; }

        public int DeliveryOrderId { get; set; }
        public decimal KFC_Per { get; set; }

        public decimal? Total { get; set; }

    }

    public class PurchaseReturnBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseReturnBillDetailId { get; set; }
        public int PurchaseReturnId { get; set; }

        public int PurchaseId { get; set; }

        public int SupplierId { get; set; }
        public decimal AdjustedAmount { get; set; }
        public int IsOpening { get; set; }
    }

    public class MaterialWarrantyDetails
    {
        [Key]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("VoucherNumber")]
        public int VoucherNumber { get; set; }

        [JsonProperty("VoucherTypeId")]
        public int VoucherTypeId { get; set; }

        [JsonProperty("ProjectId")]
        public int ProjectId { get; set; }

        [JsonProperty("MaterialId")]
        public int MaterialId { get; set; }

        [JsonProperty("serialNo")]       
        public string SerialNo { get; set; }

        [JsonProperty("warrantyDate")]   
        public DateTime WarrantyDate { get; set; }

        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("BranchId")]
        public int BranchId { get; set; }

        [JsonProperty("CompanyId")]
        public int CompanyId { get; set; }

        [JsonProperty("EnteredOn")]
        public DateTime EnteredOn { get; set; }
    }


    public class PurchaseDetailDto
    {
        public int? purchaseDetailId { get; set; }
        public int? purchaseId { get; set; }
        public int? materialId { get; set; }  
        public string materialName { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public decimal? total { get; set; }
        public decimal? discount { get; set; }
        public decimal? tax { get; set; }
        public decimal? kFC_Per { get; set; }
        public decimal? coefficientFactorValue { get; set; }
        public decimal? conversionQuantity { get; set; }
        public string conversionUnitName { get; set; }
        public string materialRemarks { get; set; }
        public int? materialCategoryId { get; set; }
        public string childDescription { get; set; }
        public decimal? fCNetAmount { get; set; }
        public decimal? lAmount { get; set; }
        public decimal? landingCost { get; set; }
        public decimal? amount { get; set; }
        public decimal? purchaseAmount { get; set; }
        public decimal? sgst { get; set; }
        public decimal? cgst { get; set; }
        public decimal? igst { get; set; }
        public int? currencyId { get; set; }
        public decimal? exchangeRate { get; set; }
        public decimal? fCBillAmount { get; set; }
        public int isServiceCharge { get; set; }
        public object warrantyDetails { get; set; }
    }


}
