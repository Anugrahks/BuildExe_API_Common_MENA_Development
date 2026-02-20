using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialUsage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ConsumptionMasterId { get; set; }
        public DateTime UsageDate { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int? EmployeeCategoryId { get; set; }
        public int? IssuedTo { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int? WorkCategory { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }

        public int UserId { get; set; }
        public int? BulkEntry { get; set; }
        public List<MaterialUsageDetails> MaterialUsageDetails { get; set; }
    }
    public class MaterialUsageDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  MaterialUsageDetailsId { get; set; }
        public int MaterialUsageId { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }

}
