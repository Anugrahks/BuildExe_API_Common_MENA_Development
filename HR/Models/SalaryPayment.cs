using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    public class SalaryPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string? PaymentNo { get; set; }
        public int WithClear { get; set; }
        public string? Remarks { get; set; }
        public int ChequeClearenceID { get; set; }
        public int ApprovalStatus { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalLevel { get; set; }
        public int IsReject { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int UserId { get; set; }
        public int IsBulk{ get; set; }

        public string MultiEmployeeId { get; set; }

        public decimal? TransactionCharge { get; set; }
        public List<SalaryPaymentDetails> SalaryPaymentDetails { get; set; }

    }




    public class SalaryPaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaryPaymentDetailsId { get; set; }
        public int? SalaryPaymentId { get; set; }
        public int EmployeeId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public decimal NetSalary { get; set; }

        public decimal? PayingAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? RoundOff { get; set; }
        public int IsOpening { get; set; }
    }
}
