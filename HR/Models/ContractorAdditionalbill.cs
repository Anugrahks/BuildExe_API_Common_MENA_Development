
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    public class ContractorAdditionalbill
    {
        public int Id { get; set; }
        public DateTime? BillDate { get; set; }
        public int? ProjectId { get; set; }
        public int? ContractorId { get; set; }
        public int? WorkOrderId { get; set; }
        public int? BlockId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? IsDeleted { get; set; }
        public int? FloorId { get; set; }
        public int? UnitId { get; set; }
        public int? DivisionId { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public short? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int UserId { get; set; }
        public DateTime? EnteredOnDate { get; set; }
        public decimal AdditionalAmount { get; set; }
        public decimal AdditionalIGST { get; set; }
        public decimal AdditionalCGST { get; set; }
        public decimal AdditionalSGST { get; set; }

        public List<ContractorAdditionalBillDetailss> ContractorAdditionalBillDetailss { get; set; }
    }

    public class ContractorAdditionalBillDetailss
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ContractorAdditionalBillDetailsId { get; set; }
        public int ContractorBillId { get; set; }
        public string WorkName { get; set; }
        public string HsnCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal GstPercentage { get; set; }
        public decimal Igst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Total { get; set; }

    }
}
