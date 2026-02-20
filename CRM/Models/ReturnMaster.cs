using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ReturnMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReturnType { get; set; } // Specification : 1 / Schedule : 2 / Direct : 3
        public DateTime ReturnDate { get; set; }
        public int ClientId { get; set; }
        public string CreditNote { get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public string ReferenceNo { get; set; }
        public string TaxType { get; set; }
        public string TaxArea { get; set; }        
        public int WorkCategoryId { get; set; }
        public string Description { get; set; }

        // Payment
        public int PaymentType { get; set; } // CASH: 1. BANK: 2
        public DateTime PaymentDate { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string? PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int WithClear { get; set; }

        // Voucher
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        // Misc
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsRejected { get; set; }
        public int UserId { get; set; }
        public string Remarks { get; set; }
        public int IsDeleted { get; set; }

        // Total
        public decimal TotalGstPer { get; set; }
        public decimal TotalGst { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal ExpectedReturnAmount { get; set; }

        // Child
        public List<ReturnDetails> ReturnDetails { get; set; }
    }

    public class ReturnDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReturnDetalId { get; set; }
        public int ReturnId { get; set; }

        public int? BillDetailsId { get; set; }
        public int? ScheduleNo { get; set; }
        public int? SpecId { get; set; }
        public decimal? CurrentQty { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? PartRatePerUnit { get; set; }
        public string? Particular { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal? Total { get; set; }
    }
}
