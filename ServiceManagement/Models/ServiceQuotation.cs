using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeServiceManagement.Models
{
    public class ServiceQuotation
    {
        // ---------- MASTER ----------
        public int Id { get; set; }                 // For update
        public int InvoiceNo { get; set; }
        public DateTime INvoiceDate { get; set; }
        public string RefNo { get; set; }
        public string ServiceType { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
        public int ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int ApprovalStatus { get; set; }
        public string RejectedRemarks { get; set; }
        public string CustomerApprovalStatus { get; set; }
        public decimal RoundOff { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime EnteredOnDate { get; set; }
        public int IsDeleted { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int JobNo { get; set; }
    }
    public class ServiceQuotationDetails
    {
        // ---------- Details ----------     // 
        [Key]  
        public int ServiceInvDetailId { get; set; } 
        public int InvoiceId { get; set; }
        public int MaterialId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }

}