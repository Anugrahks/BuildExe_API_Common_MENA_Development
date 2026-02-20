using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeMaterialServices.Models
{
    public class PurchaseReturn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string DebitNoteNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string RejectRemarks { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public string Taxarea { get; set; }
        public Int16 PurchaseReturnTypeId { get; set; }
        public int StockTypeId { get; set; }
        public int IncludeOtherCharge { get; set; }
        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPer { get; set; }

        public decimal DiscountRecievedPer { get; set; }
        public decimal DiscountRecievedAmt { get; set; }

        public decimal DiscountRecievedSGST { get; set; }

        public decimal DiscountRecievedCGST { get; set; }

        public decimal DiscountRecievedIGST { get; set; }

        public decimal DiscountReceivedGSTper { get; set; }
        public decimal NetAmount { get; set; }
        public int? Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16  IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? RoundOff { get; set; }
        public int UserId { get; set; }
        public string? Remarks { get; set; }
        public int? PaymentType { get; set; }
        public int? PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public int WithClear { get; set; }
        public int IsAsset { get; set; }
        public string ReferenceNo {  get; set; }
        public List<PurchaseReturnDetail> PurchaseReturnDetail { get; set; }

        public List<PurchaseReturnPurchaseBill> PurchaseReturnPurchaseBill { get; set; }
    }
    public class PurchaseReturnDetail 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseReturnDetailId { get; set; }
        public int PurchaseReturnId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Disount { get; set; }
        public decimal Tax { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Cgst { get; set; }

        public decimal? Igst { get; set; }
        public decimal Total {  get; set; }
        public decimal? PurchaseReturnAmount { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }


    public class PurchaseReturnPurchaseBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseReturnBillDetailId { get; set; }
        public int PurchaseReturnId { get; set; }

        public int PurchaseId { get; set; }

        public int SupplierId { get; set; }
        public decimal AdjustedAmount { get; set; }
        public int NotTagged { get; set; }

        public int IsOpening { get; set; }
    }
}
