using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class AnnualLeaveMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LeaveRequiredFromDate { get; set; }
        public DateTime LeaveRequiredToDate { get; set; }
        public int NoOfLeaves { get; set; }
        public DateTime RelievingDate { get; set; }
        public DateTime LastWorkingDate { get; set; }
        public DateTime RejoiningDate { get; set; }
        public int WorkHandOverEmployeeId { get; set; }
        public string Remarks { get; set; }
        //public int? PaymentType { get; set; }
        //public DateTime? PaymentDate { get; set; }
        //public string PaymentMode { get; set; }
        //public int? PaymentModeId { get; set; }
        //public string PaymentNo { get; set; }
        //public DateTime? ChequeDate { get; set; }
        //public int? WithClear { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? VoucherNumber { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsRejected { get; set; }
        public int UserId { get; set; }
        public int IsDeleted { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal DaySalary { get; set; }

        public List<AnnualLeaveSettlements> AnnualLeaveSettlements { get; set; }
        public List<AnnualLeaveSurrenders> AnnualLeaveSurrenders { get; set; }
    }

    public class AnnualLeaveSettlements
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnnualLeaveSettlementsId { get; set; }
        public int AnnualLeaveMasterId { get; set; }
        public int ParticularId { get; set; }
        public string ParticularName { get; set; }
        public string ParticularType { get; set; }
        public int IsSalaryHead { get; set; }
        public int IsLeaveSurrender { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
    }

    public class AnnualLeaveSurrenders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnnualLeaveSurrendersId { get; set; }
        public int AnnualLeaveMasterId { get; set; }
        public int LeaveId { get; set; }
        public string LeaveName { get; set; }
        public decimal BalanceLeave { get; set; }
        public decimal TakenLeave { get; set; }
    }
}
