using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeServices.Models
{
    public class PartBillMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int BillType { get; set; }
        public int TaxTypeId { get; set; }
        public string? BillNo { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }

        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 FinalBill { get; set; }
       
        public string Taxarea { get; set; }
        public string TaxType { get; set; }

        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal RetentionPercent { get; set; }
        public decimal RetentionAmount { get; set; }
        public decimal LDPercent { get; set; }
        public decimal LDAmount { get; set; }
        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmount { get; set; }

        public decimal SGSTPer { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal KFCPercent { get; set; }
        public decimal KFCAmount { get; set; }
        public string? RejectRemarks { get; set; }
        public string ShippingDetails { get; set; }
        public string Remarks { get; set; }
        public decimal? RoundOff { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public decimal? AdvanceInterestPer { get; set; }
        public decimal? AdvanceInterest { get; set; }
        public decimal? AdvanceTds { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TdsOnGst { get; set; }
        public decimal? TdsOnGstPer { get; set; }
        public int? DivisionId { get; set; }
        public string EWayBillNumber { get; set; }
        public string BuyersOrderNumber { get; set; }

        public int WorkCategoryId { get; set; }

        public string? clientUniqueName { get; set; }
        public List<PartBillDetails> PartBillDetails { get; set; }
        public List<PartBillRetentiondetail> PartBillRetentiondetail { get; set; }


    }
    public class PartBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartBillDetailsId { get; set; }
        public int PartBillMasterId { get; set; }
        public int ScheduleNo { get; set; }
        public int SpecId { get; set; }
        public decimal PartRatePerUnit { get; set; }
        public decimal ScheduledQty { get; set; }
        public decimal PreviousQty { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Description { get; set; }
        public int? WeeklyBillId { get; set; }

        public int ScheduleUniqueId { get; set; }


        public int MultiplyOrDivision { get; set; }

        public decimal BeforeConversionQuantity { get; set; }

        public decimal ConversionValue { get; set; }
        public int DeliveryOrderId { get; set; }

        public string DeliveryOrderWorkName { get; set; }

        public string DeliveryOrderReferenceNo { get; set; }

        public int DeliveryOrderNumber { get; set; }

        public List<string> DeliveryOrderNo { get; set; }  // Must be strings

        public DateTime? DeliveryDate { get; set; }


        public List<PartBillDimensionDetails> PartBillDimensionDetails { get; set; }

    }

    public class PartBillDimensionDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PartBillDimensionId { get; set; }
        public int PartBillId { get; set; }
        public int ScheduleNo { get; set; }
        public int SpecId { get; set; }

        public int? WeeklyBillId { get; set; }
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

        public List<PartBillDeductionDimensionDetails> PartBillDeductionDimensionDetails { get; set; }

    }


    public class PartBillDeductionDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartBillDeductionDimensionId { get; set; }
        public int? PartBillId { get; set; }
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

    public class PartBillRetentiondetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MasterId { get; set; }
        public decimal? RetentionPer { get; set; }
        public decimal? RetentionAmt { get; set; }
        public string? Label { get; set; }

    }
}
