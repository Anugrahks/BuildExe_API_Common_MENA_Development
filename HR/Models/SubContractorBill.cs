using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class SubContractorBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string  Billno { get; set; }
        public int WorkOrderId { get; set; }

        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int SubContractorId { get; set; }
        public Int16  FinancialYearId { get; set; }
        public DateTime BillDateFrom { get; set; }
        public DateTime BillDateTo { get; set; }
        public decimal AmountAsPerAttendance { get; set; }
        public decimal AmountAsPerBill { get; set; }
        public decimal AmountAsPerBillBalance { get; set; }
        public decimal AmountPaidAdvance { get; set; }
        public decimal AdditionalAmount { get; set; }
        public decimal AmountRecomendable { get; set; }
        public decimal AmountRetensionPercent { get; set; }
        public decimal AmountRetensionAmount { get; set; }
        public decimal AmountTdsPercent { get; set; }
        public decimal AmountTdsAmount { get; set; }
        public decimal? AdvanceTds { get; set; }
        public Int16 TdsStatus { get; set; }
        public Int16 RetentionStatus { get; set; }
        public decimal RetentionBalanceAmount { get; set; }
        public decimal tax { get; set; }
        public decimal taxamount { get; set; }
        public string  taxtype { get; set; }
        public decimal Othercharge { get; set; }
        public decimal RoundOff { get; set; }
        public string  Remarks { get; set; }
        public int Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime  DateApproved { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        public decimal RefundableDeductions { get; set; }
        public decimal DiscountReceived { get; set; }
        public int IsReject { get; set; }
        public int finalBill { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? PurchaseAdjusted { get; set; }
        public decimal? AdditionalIGST { get; set; }
        public decimal? AdditionalCGST { get; set; }
        public decimal? AdditionalSGST { get; set; }
        public Int16 UserId { get; set; }

        public decimal PreviousBalance { get; set; }
        public decimal TotalBalance { get; set; }
        public List<SubContractorBillDetails> SubContractorBillDetails { get; set; }
        public List<SubContractorAdditionalBillDetails> SubContractorAdditionalBillDetails { get; set; }
        public List<PurchaseAdjustmentEntry> PurchaseAdjustmentEntry { get; set; }
        public List<SubcontractorOtherDeductionDetail> SubcontractorOtherDeductionDetail { get; set; }


    }
    public class SubContractorBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SubContractorBillDetailsId { get; set; }
        public int SubContractorBillId { get; set; }
        public int WorkOrderDetailsId { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal NegotiatedAmount { get; set; }
        public decimal WorkRate { get; set; }
        public decimal n { get; set; }
        public decimal l { get; set; }
        public decimal b { get; set; }
        public decimal h { get; set; }

        public int MultiplyOrDivision { get; set; }


        public decimal ConversionValue { get; set; }


        public decimal BeforeConversionQuantity { get; set; }
        public List<SubContractorBillDimensionDetails> SubContractorBillDimensionDetails { get; set; }



    }


    public class SubContractorBillDimensionDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SubContractorBillDimensionId { get; set; }
        public int SubContractorBillId { get; set; }
        public int WorkOrderDetailsId { get; set; }
        public string SubWork { get; set; }

        public decimal Subquantity { get; set; }
        public decimal n { get; set; }
        public decimal l { get; set; }
        public decimal b { get; set; }
        public decimal h { get; set; }
        public decimal l1 { get; set; }
        public decimal l2 { get; set; }
        public decimal b1 { get; set; }

        public decimal b2 { get; set; }
        public decimal GrossQuantity { get; set; }


        public decimal DeductionValue { get; set; }

        public decimal? CoefficientFactor { get; set; }



        public List<SubContractorBillDeductionDimensionDetails> SubContractorBillDeductionDimensionDetails { get; set; }

    }

    public class SubContractorBillDeductionDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubContractorBillDeductionDimensionId { get; set; }
        public int? SubContractorBill { get; set; }
        public int? SpecId { get; set; }
        public string DeductionSubWork { get; set; }
        public decimal DeductionSubquantity { get; set; } = 0;

        public decimal n { get; set; }
        public decimal l { get; set; }
        public decimal b { get; set; }
        public decimal h { get; set; }

        public decimal GrossQuantity { get; set; }

        public decimal DeductionValueChild { get; set; }

        public decimal? CoefficientFactor { get; set; }
    }





    public class SubContractorAdditionalBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SubContractorAdditionalBillDetailsId { get; set; }
        public int SubContractorBillId { get; set; }
        public int WorkId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal GstPercentage { get; set; }
        public decimal Igst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }

    }

    public class PurchaseAdjustmentEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AdjustmentId { get; set; }
        public int SubContractorBillId { get; set; }
        public int SubcontractorId { get; set; }
        public int PurchaseId { get; set; }

        public int TransferId { get; set; }

        
        public decimal Amount { get; set; }


    }

    public class SubcontractorOtherDeductionDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int SubContractorBillId { get; set; }
        public decimal OtherDeductionPer { get; set; }
     
        public decimal OtherDeductionAmt { get; set; }
        public string Label { get; set; }
        public int AccountheadId { get; set; }

    }
}
