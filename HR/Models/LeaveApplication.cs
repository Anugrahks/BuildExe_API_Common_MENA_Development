using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    public class LeaveApplication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int EmployeeId { get; set; }
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
        public int UserId { get; set; }
        public int IsWithHolidays { get; set; }
        public decimal TotalDeduction { get; set; }
        public List<LeaveApplicationDetail> LeaveApplicationDetail { get; set; }
        public List<LeaveApplicationDocument> LeaveApplicationDocument { get; set; }
        public List<VaryingHeads> VaryingHeads { get; set; }
    }

    public class LeaveApplicationDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LeaveApplicationDocumentId { get; set; }
        public int LeaveApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }

    }


    public class LeaveApplicationDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LeaveApplicationDetailId { get; set; }
        public int LeaveApplicationId { get; set; }
        public int LeaveType { get; set; }
        public decimal NoOfLeaves { get; set; }
        public decimal BalanceLeaves { get; set; }
        public decimal TakenLeaves { get; set; }
        public decimal Penaltydet_Total { get; set; }

        public decimal? Penaltydet_penaltyPerDay { get; set; }

        public decimal Leave_imp_salaryperDay { get; set; }
        public decimal Leave_imp_det_total { get; set; }

    }



    [Keyless]
    [NotMapped]
    public class VaryingHeads
    {
        public int EmpId { get; set; }
        public string HeadName { get; set; }
        public int HeadId { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public decimal Amount { get; set; }
        public int LeaveApplicationId { get; set; }






    }
}
