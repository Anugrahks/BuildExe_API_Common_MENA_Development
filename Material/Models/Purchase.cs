using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeMaterialServices.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PurchaseInvoiceNo { get; set; }
        public int PurchaseOrderNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public string Remark { get; set; }
        public string Taxarea { get; set; }
        public int Category { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }

        public decimal BillAmount { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal AmountPaidAdvance { get; set; }
        public decimal billdiscount { get; set; }
        public decimal? billdiscountPer { get; set; }
        public decimal Roundoff { get; set; }
        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }

        public string? ReqLoadingTax { get; set; }
        public string? ReqTransportTax { get; set; }
        public string? ReqOtherCharesTax { get; set; }

        public decimal KFCPer { get; set; }
        public decimal GSTPer { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal KFCAmount { get; set; }
        public Int16   MaterialTypeId { get; set; }


        public Int16 PaymentModeId { get; set; }
        public int SiteManagerId { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TcsPer { get; set; }
        public decimal? TcsAmount { get; set; }
        public decimal? GstPercentage { get; set; }
        public int IsGst { get; set; }
        public int IsTransportation { get; set; }
        public int IsLoadingUnloading { get; set; }
        public int IsOtherCharge { get; set; }
        public int? IsPercentage { get; set; }
        public int? IsAmount { get; set; }
        //public decimal? Sgst { get; set; }
        public int DisableFlag { get; set; }
        public int DisablePercentageFlag { get; set; }
        public int DisableAmountFlag { get; set; }

        public int UserId { get; set; }

        public int SubcontractorId { get; set; }
        public string? TemporaryTransitLocation { get; set; }
        public int CreditPeriod { get; set; }
        public string VehicleNo { get; set; }
        public int SiteLoan { get; set; }
        public decimal SiteLoanAmt { get; set; }


        public decimal DiscountWithoutTax { get; set; }
        public List<PurchaseDetail> PurchaseDetail { get; set; }

        public List<PurchaseDeliveryDetail> PurchaseDeliveryDetail { get; set; }

        
        public List<PurchaseReturnBill> PurchaseReturnBill { get; set; }

        public int? BankId { get; set; }
        public string? PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int? WithClear { get; set; }

        public int IsAsset { get; set; }


        public decimal TransportationChargeGst { get; set; }
        public decimal LoadingUnloadingChargeGst { get; set; }
        public decimal OtherChargesGst { get; set; }


        public int FromDeliveryOrder { get; set; }

        public int? CurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? FCBillAmount { get; set; }
        public decimal? FCBillAmountBalance { get; set; }
        public decimal? FCNetAmount { get; set; }
    }
    public class PurchaseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseDetailId { get; set; }// naming convension----classname+ id
        public int PurchaseId { get; set; }// naming convension----parentclassname+ id
        public int MaterialId { get; set; }

        public int MaterialBrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public int PurchaseOrderDetailsId { get; set; }
        public decimal KFC_Per { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Cgst { get; set; }

        public decimal? Igst { get; set; }
        public decimal? PurchaseAmount { get; set; }

        public decimal? Total { get; set; }


        public string ChildDescription { get; set; }

        public string MaterialRemarks { get; set; }

        public int? MaterialCategoryId { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }

        public decimal? FCTotal { get; set; }
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


}
