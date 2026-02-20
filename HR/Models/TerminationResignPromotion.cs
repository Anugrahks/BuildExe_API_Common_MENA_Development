using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class TerminationResignPromotion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int TypeId { get; set; } = 0;
        public string? Reason { get; set; }
        public DateTime? EntryDate { get; set; }

        public DateTime? InitiativeDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public int? EmployeeId { get; set; }
        public int DesignationId { get; set; } = 0;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public decimal SalaryAmt { get; set; }
        public decimal OvertimeAmt { get; set; }
        public decimal LoanAmt { get; set; } = 0;
        public decimal AdvanceAmt { get; set; } = 0;
        public decimal EmiAmt { get; set; } = 0;
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int IsDeleted { get; set; } = 0;
        public int CompanyId { get; set; } = 0;
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }

        public string? Remarks { get; set; }
        public int UserId { get; set; }
        public List<TerminationResignPromotionDocumentDetails> TerminationResignPromotionDocumentDetails { get; set; }


    }
    public class TerminationResignPromotionDocumentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DetailId { get; set; }
        public int MasterId { get; set; } = 0;
        public string? DocumentName { get; set; }
        public string? Document { get; set; }

    }


}
