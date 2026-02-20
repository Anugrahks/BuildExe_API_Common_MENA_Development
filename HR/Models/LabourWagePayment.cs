using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LabourWagePayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public int BillVoucherNumber { get; set; }
        public int BillVoucherTypeId { get; set; }


        public DateTime PaymentDate { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public Int16 WithClear { get; set; }
        public string Remarks { get; set; }
        public int ChequeClearenceID { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public DateTime? ChequeDate { get; set; }
        public Int16 UserId { get; set; }
        public int? BulkEntry { get; set; }
        public decimal? Tdsper { get; set; }
        public decimal? Tdsamt { get; set; }

        public int PendingBillsClear { get; set; }

        public int SiteLoan { get; set; }

        public decimal SiteLoanAmt { get; set; }

        public string EmployeeIdList    { get; set; }

        public List<LabourWagePaymentDetails> LabourWagePaymentDetails { get; set; }

    }
    public class LabourWagePaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabourWagePaymentDetailsId { get; set; }
        public int LabourWagePaymentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public int DivisionId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal DaysWorked { get; set; }
        public decimal OverTimeHrs { get; set; }
        public decimal DailyWageAmount { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal TotalWage { get; set; }
        public decimal Othercharges { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal PayingAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal Total { get; set; }
        public decimal PenaltyAmount { get; set; }

    }
}
