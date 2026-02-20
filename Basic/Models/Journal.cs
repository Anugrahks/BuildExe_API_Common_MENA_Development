using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class Journal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime JournalDate { get; set; }
        public short CompanyId { get; set; }
        public short BranchId { get; set; }
        public int UserId { get; set; }
        public short FinancialYearId { get; set; }

        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public short ApprovalStatus { get; set; }
        public short ApprovedBy { get; set; }
        public short ApprovalLevel { get; set; }

        public string? Description { get; set; }
        public short IsDeleted { get; set; }

        public int IsReject { get; set; }
        public int EnquiryId { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        // ✅ Nested collection (like SiteExpense)
        public List<JournalDetails> JournalDetails { get; set; }
    }

    public class JournalDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int JournalDetailsId { get; set; }

        public int JournalId { get; set; }

        public int DebitHeadId { get; set; }
        public int CreditHeadId { get; set; }
        public decimal Amount { get; set; }

        public int? BillId { get; set; }
        public string? BillName { get; set; }

        public int? Type { get; set; }
        public int? JournalType { get; set; }
        public int? EmployeeCategoryId { get; set; }

        public decimal? GSTPercentage { get; set; }
        public int? TaxAreaId { get; set; }
        public decimal? GSTAmount { get; set; }
        public int? AccountTypeId { get; set; }

        public string? Remarks { get; set; }
        public int WithClear { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string? ChequeNo { get; set; }
        public string? RefNo { get; set; }

        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }

        public string? ChildTaxType { get; set; }
        public string? VATNumber { get; set; }

        public int ProjectIdDet { get; set; }
        public int BlockIdDet { get; set; }
        public int FloorIdDet { get; set; }
        public int UnitIdDet { get; set; }
        public int DivisionIdDet { get; set; }

        public string ProjectNameDet { get; set; }
        public string BlockNameDet { get; set; }
        public string FloorNameDet { get; set; }
        public string UnitNameDet { get; set; }
        public string DivisionNameDet { get; set; }

        public int EnquiryIdDet { get; set; }

        public string EnquiryNameDet { get; set; }

        public List<DocumentDetailsJour> DocumentDetails { get; set; }
    }
    public class DocumentDetailsJour
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

        public List<DocumentFileJour> DocumentFiles { get; set; }
    }

    public class DocumentFileJour
    {
        public string DocumentName { get; set; }
        public string Document { get; set; }
        public int Confidential { get; set; }
        public int UserId { get; set; }
    }



}
