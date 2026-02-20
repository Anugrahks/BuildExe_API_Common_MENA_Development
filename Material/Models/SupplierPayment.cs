using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeMaterialServices.Models
{
    public class SupplierPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 FinantialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? chequeDate { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public Int16 WithClear { get; set; }
        public Int16 BillWise { get; set; }
        public Int16 OnlinePayment { get; set; }
        public string Paymentdetails { get; set; }
        public Int16 IsDeleted { get; set; }
        public int ChequeClearenceID { get; set; }
        public Int16 SupplierReturn { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public int? SitemanagerId { get; set; }
        public int IsReject { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int SiteLoan { get; set; }

        public decimal SiteLoanAmt { get; set; }
        public List<SupplierPaymentDetails> SupplierPaymentDetails { get; set; }
        public List<DebitNote> DebitNote { get; set; }



    }
    public class SupplierPaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierPaymentDetailsId { get; set; }
        public int SupplierPaymentId { get; set; }
        public int PurchaseId { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int IsOpening { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal DebitNoteAdjustmentAmount { get; set; }
        public int Rental { get; set; }

        public decimal RoundOff {  get; set; }


    }


    public class DebitNote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Int32 CompanyId { get; set; }
        public Int32 BranchId { get; set; }
        public int? SupplierId { get; set; }
        public int? FinancialYearId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RecoveryAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RecoveredAmount { get; set; }
        public int DebitNoteNo { get; set; }
        public int IsSelect { get; set; }
        public int PurchaseReturnId { get; set; }
        public DateTime? TransactionTime { get; set; }
        public int? SupplierPaymentId { get; set; }
    }

}