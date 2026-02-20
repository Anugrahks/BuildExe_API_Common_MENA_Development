using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;


namespace BuildExeHR.Models
{
    public class LeaveSurrenderMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? SurrenderDate { get; set; }
        public string? EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int MonthId { get; set; }
        public string? MonthName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public string Remarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int UserId { get; set; } = 0;

        public List<LeaveSurrenderDetails> LeaveSurrenderDetails { get; set; }

    }

    public class LeaveSurrenderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int? LeaveId { get; set; }
        public string LeaveName { get; set; }
        public decimal NoofLeaves { get; set; }
        public decimal BasicSalaryPerDay { get; set; }
        public decimal Amount { get; set; } = 0;

    }

 
}
