using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class RefundList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string ProjectName { get; set; }
        public string DivisionShortName { get; set; }
        public int? UnitId { get; set; }
        public string? UnitName { get; set; }
        
        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
        public int? FloorId { get; set; }
        public string? FloorName { get; set; }
        public Int16 RefundType { get; set; }
        public DateTime Refunddate { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal? performanceguarantee { get; set; }
        public string? Narration { get; set; }

        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int? WithClear { get; set; }

        public int MasterId { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public int IsDeleted { get; set; }

        public int? ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemark { get; set; }
        public int? IsReject { get; set; }
        public int? RejectedBy { get; set; }
        public string RejectRemark { get; set; }

        public string DepartmentShortName { get; set; }

        public string FirstName { get; set; }

    }
}
