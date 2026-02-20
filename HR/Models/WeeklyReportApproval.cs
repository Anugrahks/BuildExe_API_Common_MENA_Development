using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class WeeklyReportApproval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SupplierId { get; set; }

        public int AccountheadId  { get; set; }

        public int ProjectId { get; set; }
        public decimal ApprovedCash { get; set; }

        public decimal ApprovedCheque { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }


        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int? IsReject { get; set; }
        public int UserId { get; set; }


    }
}
