using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LeaveApplicationList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? Others { get; set; }
        public string? Reason { get; set; }
        public DateTime RequiredFrom { get; set; }
        public DateTime RequiredTo { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public DateTime? ResumingDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public string? Comments { get; set; }
        public int ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalLevel { get; set; }
        public int IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int Paid { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int Maxlevel { get; set; }
        public int? viewType { get; set; }
        public int? LeaveReimbursementHeadId { get; set; }
        public int? PenaltyDeductionHeadId { get; set; }
        public int UserId { get; set; }
        public int IsWithHolidays { get; set; }
    }
}
