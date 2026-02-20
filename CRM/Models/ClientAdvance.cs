using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ClientAdvance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public string Remarks { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentModeNo { get; set; }
        public Int16 withclear { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        
    }
}
