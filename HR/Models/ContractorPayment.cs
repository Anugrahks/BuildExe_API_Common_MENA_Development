using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class ContractorPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ChequeDate { get; set; }
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

        public Int16 UserId { get; set; }
        public decimal? Tdsper { get; set; }

        public decimal? Tdsamt { get; set; }

        public int SiteLoan { get; set; }
        public decimal SiteLoanAmt { get; set; }
        public List<ContractorPaymentDetails> ContractorPaymentDetails { get; set; }
    }
    public class ContractorPaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractorPaymentDetailsId { get; set; }
        public int ContractorPaymentId { get; set; }
        public int BillId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal RoundOff { get; set; }
        public int IsAdditionalbill { get; set; }



    }
}
