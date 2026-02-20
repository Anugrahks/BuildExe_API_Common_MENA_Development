using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class ContractorWorkOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateOrdered { get; set; }
        public string WorkOrderNo { get; set; }
        public int ContractorId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal NegotiatedAmount { get; set; }
        public string TaxType { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public string Remarks { get; set; }
        public int Category { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }

        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }

        public Int16 UserId { get; set; }

        public decimal TotalWorkRoundOff { get; set; }

        public List<ContractorWorkOrderDetails> ContractorWorkOrderDetails { get; set; }
        public List<ContractorStageSetting> ContractorStageSetting { get; set; }
    }
    public class ContractorWorkOrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractorWorkOrderDetailsId { get; set; }
        public int ContractorWorkOrderId { get; set; }
        public string HsnCode { get; set; }
        public string WorkName { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }

    }
    public class ContractorStageSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContractorWorkOrderId { get; set; }
        public string StageName { get; set; }
        public decimal StagePer { get; set; }
        public decimal StageAmount { get; set; }
        public int? FinancialYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StageStatusId { get; set; }
        public decimal TaxAmount { get; set; }
        public int PercentageWise { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string? Remarks { get; set; }
        public decimal TdsPer { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal RetentionPer { get; set; }
        public decimal RetentionAmount { get; set; }

        public decimal AdditionalAmount { get; set; }

        public decimal AdditionalIGST { get; set; }

        public decimal AdditionalCGST { get; set; }

        public decimal AdditionalSGST { get; set; }

        public string ReferenceNo { get; set; }
        public List<ContractorAdditionalBillDetails> ContractorAdditionalBillDetails { get; set; }
    }
    public class ContractorAdditionalBillDetails
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
        public int? ProjectId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? DivisionId { get; set; }
        public string? WorkOrderNo { get; set; }

    }

}
