using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class TAPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }


        public DateTime ToDate { get; set; }

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
        public int IsReject { get; set; }
        public int IsDeleted { get; set; }

        public DateTime PaymentDate { get; set; }
        public string? PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string? PaymentNo { get; set; }
        public int WithClear { get; set; }

        public string? Remarks { get; set; }
        public int ChequeClearenceId { get; set; }

        public DateTime? ChequeDate { get; set; }

        public decimal PaidAmount { get; set; }


        public decimal PreviousBalance { get; set; }
        public decimal TACar { get; set; }
        public decimal TABike { get; set; }
        public decimal TABusFare { get; set; }
        public decimal Total { get; set; }

        public decimal BalanceAmount { get; set; }

        public List<TAPaymentDetails> TAPaymentDetails { get; set; }

    }
    public class TAPaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TAPaymentDetailsId { get; set; }
        public int MasterId { get; set; }

        public int BillId { get; set; }

    }

}
