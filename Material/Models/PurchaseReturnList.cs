using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class PurchaseReturnList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DebitNoteNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public string Taxarea { get; set; }
        public Int16 PurchaseReturnTypeId { get; set; }
        public int StockTypeId { get; set; }
        public int IncludeOtherCharge { get; set; }
        public string ReturnType { get; set; }
        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal NetAmount { get; set; }
        public int? Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public Int16 IsDeleted { get; set; }
        public int ViewType { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? RoundOff { get; set; }
        public string? Remarks { get; set; }
        public int? PaymentType { get; set; }
        public int? PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public int WithClear { get; set; }

        public string ReferenceNo { get; set; }

        public decimal DiscountRecievedPer { get; set; }
        public decimal DiscountRecievedAmt { get; set; }

        public decimal DiscountRecievedSGST { get; set; }

        public decimal DiscountRecievedCGST { get; set; }

        public decimal DiscountRecievedIGST { get; set; }
        public decimal DiscountReceivedGSTper { get; set; }
    }
}
