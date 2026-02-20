using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeServices.Models
{
    public class WeeklyBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int BillType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal BillAmount { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public Int16 IsReject { get; set; }

        public int UserId { get; set; }
        public List<WeeklyBillDetails> WeeklyBillDetails { get; set; }
    }
    public class WeeklyBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeeklyBillDetailsId { get; set; }
        public int WeeklyBillId { get; set; }
        public int SpecId { get; set; }
        public string SpecNumber { get; set; }
        public string SpecName { get; set; }
        public string SacCode { get; set; }

        public decimal ScheduledQty { get; set; }
        public decimal RatePerUnit { get; set; }
        public decimal PreviousQty { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal RevisedQty { get; set; }
        public decimal ActualQty { get; set; }
        public decimal ActualRate { get; set; }
        public decimal? Tax { get; set; }

        public int MultiplyOrDivision { get; set; }

        public decimal BeforeConversionQuantity { get; set; }

        public decimal ConversionValue { get; set; }
        public List<WeeklyBillDimensionDetails> WeeklyBillDimensionDetails { get; set; }
    }


    public class WeeklyBillDimensionDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int WeeklyBillDimensionId { get; set; }
        public int WeeklyBillId { get; set; }
        public int SpecId { get; set; }
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

        public List<WeeklyBillDeductionDimensionDetails> WeeklyBillDeductionDimensionDetails { get; set; }

    }


    public class WeeklyBillDeductionDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeeklyBillDeductionDimensionId { get; set; }
        public int? WeeklyBillId { get; set; }
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


    public class WeeklyBillByDates
    {
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int BillNoFrom { get; set; }
        public int BillNoTo { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
    }
}
