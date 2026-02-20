using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class SiteExpense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int EmployeeId { get; set; }
        public string Form { get; set; }
        public int Category { get; set; }
        public int WithClear { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? WorkNameId { get; set; }
        public int? UserId { get; set; }
        public string TaxArea { get; set; }
        public decimal? SiteLoanAmt { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; }

        public int BatchID { get; set; }

        public string BatchNo { get; set; }

        public List<SiteExpenseDetails> SiteExpenseDetails { get; set; }
    }

    public class SiteExpenseDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }
        public int SiteExpenseMasterId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public int DebitHeadId { get; set; }
        public int CreditHeadId { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public decimal? GSTper { get; set; }
        public decimal? SGST { get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? RoundOff { get; set; }
        public int Category { get; set; }
        public int? WorkNameId { get; set; }
        public string VATNumber { get; set; }

        public List<DocumentDetails> DocumentDetails { get; set; }
    }

    public class DocumentDetails
    {
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string DocumentStatus { get; set; }
        public DateTime EnteredDate { get; set; }
        public string FileNo { get; set; }
        public string RackNo { get; set; }
        public int DocumentGroupId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Category { get; set; }
        public string DocumentPath { get; set; }
        public int UserId { get; set; }

        public List<DocumentFile> DocumentFiles { get; set; }
    }

    public class DocumentFile
    {
        public string DocumentName { get; set; }
        public string Document { get; set; }
        public int Confidential { get; set; }
        public int UserId { get; set; }
    }
}
