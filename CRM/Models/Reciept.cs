using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class Reciept
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime  RecieptDate { get; set; }
       
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16  FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public Int16 WithClear { get; set; }
        public string Remarks { get; set; }
        public int ChequeClearenceID { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public DateTime? ChequeDate { get; set; }
        public DateTime? NextRecieptDate { get; set; }
        public int RecieptType { get; set; }
        public int CustomerId { get; set; }
        public int? IsCreditPayment { get; set; }
        public string? clientUniqueName { get; set; }
        public List<RecieptDetail> RecieptDetail { get; set; }

    }
    public class RecieptDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecieptDetailId { get; set; }
        public int RecieptId { get; set; }
        public Int16 Type { get; set; }
        public int BillId { get; set; }
        public decimal  Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Advance { get; set; }
        public decimal TdsPer { get; set; }
        public decimal TdsAmt { get; set; }
        public decimal BdsCharge { get; set; }

    }
    }
