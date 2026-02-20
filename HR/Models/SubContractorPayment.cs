using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    public class SubContractorPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }

        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 FinantialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
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
        public List<SubContractorPaymentDetails> SubContractorPaymentDetails { get; set; }

        public List<SubContractorPaymentOtherDeductionDetail> SubContractorPaymentOtherDeductionDetail { get; set; }
    }
    public class SubContractorPaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubContractorPaymentDetailsId { get; set; }
        public int SubContractorPaymentId { get; set; }
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

        public int OtherDeduction { get; set; }

        public string OtherDeductionLabel { get; set; }

        public decimal OtherDeductionAmount { get; set; }

        

    }


    public class SubContractorPaymentOtherDeductionDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int SubContractorPaymentId { get; set; }
        public int BillId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal OtherDeductionPer { get; set; }

        public decimal OtherDeductionAmount { get; set; }
        public string OtherDeductionLabel { get; set; }

    }
}
