using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LeaveAccountClearenceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int LeaveId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public decimal LeaveSalary { get; set; }
        public decimal Gratuity { get; set; }
        public decimal AirTicket { get; set; }
        public decimal Others { get; set; }
        public decimal Advance { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal NetPay { get; set; }

        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int? VoucherNumber { get; set; }
        public int? VoucherTypeId { get; set; }

        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int WithClear { get; set; }

        public int? ChequeClearenceID { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }

        public int IsDeleted { get; set; }
        public int IsReject { get; set; }
        public int Maxlevel { get; set; }
        public int? viewType { get; set; }
    }
}
