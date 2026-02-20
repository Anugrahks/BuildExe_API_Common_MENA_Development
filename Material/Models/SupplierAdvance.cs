using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class SupplierAdvance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public string PaymentMode { get; set; }
        public int PaymentBy { get; set; }
        public string PaymentNo { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal AdvanceRecoveryBalance { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public string Narration { get; set; }
        public Int16 WithClear { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }
        public DateTime? chequeDate { get; set; }
        public int SitemanagerId { get; set; }
        public int IsReject { get; set; }
        public int SiteLoan { get; set; }

        public decimal SiteLoanAmt { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        public int VouNumber { get; set; }



        public int VouTypeId { get; set; }

        public int VouMasterId { get; set; }


        public decimal NewAmount { get; set; }
    }
}
