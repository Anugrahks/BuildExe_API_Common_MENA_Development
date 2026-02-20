using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class AttendanceMonthlyList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AttendanceType { get; set; }

        public int MonthId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public int IsDeleted { get; set; }

        public string MonthName { get; set; }
        public int Maxlevel { get; set; }

        public int YearId { get; set; }


        public int MDurationId { get; set; }


        public DateTime MFromDate { get; set; }

        public DateTime MToDate { get; set; }

        public int MEmployeeMasterId { get; set; }
    }
}
