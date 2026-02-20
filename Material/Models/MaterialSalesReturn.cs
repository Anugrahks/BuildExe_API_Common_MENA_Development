using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class MaterialSalesReturn
    {
        // Sales Return Details
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReturnType { get; set; } // Raw Materials: 1, Finished Goods: 2, Scrap: 3
        public DateTime ReturnDate { get; set; }
        public int CustomerId { get; set; }
        public string CreditNote { get; set; }
        public int StockPoint { get; set; }
        public int DivisionId { get; set; }
        public string ReferenceNo { get; set; }
        public string TaxArea { get; set; }

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

        // Discount
        public decimal? BillDiscountPer { get; set; }
        public decimal? BillDiscount { get; set; }
        public decimal? RoundOff { get; set; }
        
        // Total
        
        public decimal TotalGst { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }

        // Child
        public List<MaterialSalesReturnDetails> MaterialSalesReturnDetails { get; set; }
    }

    public class MaterialSalesReturnDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialSalesReturnDetailId { get; set; }
        public int MaterialSalesReturnId { get; set; }

        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? TaxAmt { get; set; }
        public int? StockPoint { get; set; }
        public int? DivisionId { get; set; }
    }
}
