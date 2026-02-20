using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class LoanRepayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeId { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public string? Remarks { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? PayingAmount { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int withclear { get; set; }

        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }

        public int? ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int IsDeleted { get; set; }
        public int UserId { get; set; }

    }

}
