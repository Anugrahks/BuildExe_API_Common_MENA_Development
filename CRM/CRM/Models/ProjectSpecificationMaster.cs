using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeServices.Models
{
    public class ProjectSpecificationMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? RefNo { get; set; }
        public DateTime? SpecDate { get; set; }

        public int DepartmentId { get; set; } = 0;
        public int SpecOrManual { get; set; } = 0;

        public int EstimationId { get; set; } = 0;

        public string SubId { get; set; } 
        public int? ProjectId { get; set; }
        public int? UnitId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? DivisionId { get; set; }
        public int? EnquiryId { get; set; }

        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ApprovedBy { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int UserId { get; set; }
        public DateTime? EnteredOnDate { get; set; }
        public string ImageUrlMaster { get; set; }
        public string SpecIds { get; set; }
        public string projectDescription { get; set; }

        public DateTime? ValidityExpiryDate { get; set; }


        public decimal SpecialDiscountAmountMaster { get; set; }


        public decimal DiscountAmountMaster { get; set; }

        public List<ProjSpecification> ProjSpecification { get; set; }

    }

    public class ProjSpecification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProjSpecificationId { get; set; } = 0;
        public int SpecMasterId { get; set; } = 0;
        public string GroupingLabel { get; set; }
        public int WorkNameId { get; set; }
        public string SpecNumber { get; set; }
        public string SpecName { get; set; }
        public string SacCode { get; set; }
        public string SpecDescription { get; set; }
        public int? WorkTypeId { get; set; }
        public int? UnitId { get; set; }
        public decimal? SpecUnit { get; set; }
        public int? CategoryId { get; set; }

        public decimal QuantityRequired { get; set; }
        public decimal? RatePerUnit { get; set; }

        public decimal? SpecAmount { get; set; }

        public decimal? QuotedRatePerUnit { get; set; }
        public decimal? QuotedAmount { get; set; }
        public decimal? MarktRatePerUnit { get; set; }
        public decimal? NegotiatedRatePerUnit { get; set; }
        public decimal? DeptRatePerUnit { get; set; }
        public decimal? WaterElectricityCharge { get; set; }
        public decimal? LabourAdditionalCharge { get; set; }
        public decimal? SubcontractAdditionalCharge { get; set; }
        public decimal? ContractorProfit { get; set; }
        public decimal? ContractorProfitAmt { get; set; }
        public decimal? OtherExpense { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? MarketWaterElectricityCharge { get; set; }
        public decimal? MarketLabourAdditionalCharge { get; set; }
        public decimal? MarketSubcontractAdditionalCharge { get; set; }
        public decimal? MarketContractorProfit { get; set; }
        public decimal? MarketContractorProfitAmt { get; set; }
        public decimal? MarketTax { get; set; }
        public decimal? MarketTaxAmount { get; set; }
        public decimal? MarketOtherExpense { get; set; }
        public string TaxArea { get; set; }
        public string TaxType { get; set; }
        public decimal LabourAdditionalChargePer { get; set; }
        public decimal SubcontractAdditionalChargePer { get; set; }
        public decimal WaterElectricityChargePer { get; set; }
        public DateTime? EnteredOnDate { get; set; }
        public decimal OtherExpensePer { get; set; }
        public decimal CostIndex { get; set; }
        public decimal SpecSubTotal { get; set; }
        public string ImageUrl { get; set; }

        public int ProjSpecTableId { get; set; }

        public int MultiplyOrDivision { get; set; }

        public decimal BeforeConversionQuantity { get; set; }

        public decimal ConversionValue { get; set; }

        public decimal TotalMaterialProfitPer { get; set; }

        public decimal TotalMaterialProfitAmount { get; set; }

        public decimal TotalSubcontractorProfitPer { get; set; }

        public decimal TotalSubcontractorProfit { get; set; }

        public decimal TotalLabourProfitPer { get; set; }

        public decimal TotalLabourProfit { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SpecialDiscountAmount { get; set; }

        public int IsDeductionWork { get; set; }

        public decimal? SpecSubTotalRoundOff { get; set; }

        public decimal? DeptAmount { get; set; }

        public decimal CustomsDuty { get; set; }

        public decimal DOCharge { get; set; }

        public decimal HandlingCharge { get; set; }
        public decimal MOFAAttestation { get; set; }
        public decimal DocumentationCharge { get; set; }
        public decimal StorageCharge { get; set; }
        public List<ProjSpecificationDetails> ProjSpecificationDetails { get; set; }

        public List<ProjSpecificationDimensionDetails> ProjSpecificationDimensionDetails { get; set; }
        public List<ProjSpecificationSteelDimensionDetails> ProjSpecificationSteelDimensionDetails { get; set; }
    }

    public class ProjSpecificationDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjSpecificationDetailsId { get; set; }
        public int? ProjSpecificationId { get; set; }
        public int? SpecificationMasterId { get; set; }
        public string GroupingLabel { get; set; }
        public int WorkNameId { get; set; }

        public string SpecName { get; set; }
        public int? ProjSpecItemTypeId { get; set; }
        public int? ProjSpecItemId { get; set; }
        public decimal? ProjQtyRequired { get; set; }
        public decimal? OrginalQty { get; set; }
        public decimal? ProjRateOfItem { get; set; }
        public decimal? ProjRateOfConveyance { get; set; }
        public decimal? MarketRate { get; set; }
        public decimal? MarketRateOfConveyance { get; set; }
        public int? TemplateId { get; set; }
        public int BrandId { get; set; } = 0;
        public decimal WastagePer { get; set; }

        public decimal WastageAmount { get; set; }

        public decimal MaterialProfitPer { get; set; }

        public decimal MaterialProfitAmount { get; set; }

        public decimal SubcontractorProfitPer { get; set; }

        public decimal SubcontractorProfit { get; set; }

        public decimal LabourProfitPer { get; set; }

        public decimal LabourProfit { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }

    }

    public class ProjSpecificationDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjSpecificationDimensionId { get; set; }
        public int? ProjSpecificationId { get; set; }
        public int? SpecificationMasterId { get; set; }
        public string GroupingLabel { get; set; }
        public int WorkNameId { get; set; }

        public string SpecName { get; set; }
        public string SubWork { get; set; }
        public decimal Subquantity { get; set; } = 0;
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

        public List<ProjSpecificationDeductionDimensionDetails> ProjSpecificationDeductionDimensionDetails { get; set; }
    }

    public class ProjSpecificationSteelDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjSpecificationDimensionId { get; set; }
        public int? ProjSpecificationId { get; set; }
        public int? SpecificationMasterId { get; set; }
        public string GroupingLabel { get; set; }
        public int WorkNameId { get; set; }

        public string SpecName { get; set; }
        public string SubWork { get; set; }
        public decimal Length_D { get; set; } = 0;
        public decimal Breadth_M { get; set; }
        public string TypeName { get; set; }

        public string WorkType { get; set; }
        public decimal Spacing { get; set; }
        public decimal N_o { get; set; }
        public decimal Quantity { get; set; }
        public int MaterialId { get; set; }

        public string MaterialName { get; set; }
        public decimal TotalLength { get; set; }
    }


    public class ProjSpecificationDeductionDimensionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjSpecificationDeductionDimensionId { get; set; }
        public int? ProjSpecificationId { get; set; }
        public int? SpecificationMasterId { get; set; }
        public string GroupingLabel { get; set; }
        public int WorkNameId { get; set; }

        public string SpecName { get; set; }
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
}
