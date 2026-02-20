using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class MaterialProduction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public DateTime?ProductionDate { get; set; }
            public int? ProjectId { get; set; }
            public int? BlockId { get; set; }
            public int? FloorId { get; set; }
            public int? UnitId { get; set; }
            public int? DivisionId { get; set; }
            public string ReferenceNo { get; set; }
            public decimal? OtherExpensePer { get; set; }
            public decimal? OtherExpense { get; set; }
            public int UserId { get; set; } = 0;
            public DateTime? UpdatedOn { get; set; }
            public DateTime? ApprovedOn { get; set; }
            public int? ApprovedBy { get; set; }
            public int? ProductionMaterialId { get; set; }
            public int? CompanyId { get; set; }
            public int? BranchId { get; set; }
            public int? FinancialYearId { get; set; }
            public int ApprovalStatus { get; set; } 
            public int ApprovalLevel { get; set; } 
            public int IsRejected { get; set; } 
            public decimal? NetAmount { get; set; }
            public decimal? TotalQuantity { get; set; }
            public decimal? TotalRate { get; set; }
            public string ApprovalRemarks { get; set; }
            public string RejectRemarks { get; set; }
            public string Remarks { get; set; }
        
            public List<ProductionDetail> ProductionDetail { get; set; }


    }



    public class ProductionDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductionDetailId { get; set; }
        public int ProductionMasterId { get; set; }
        public int ProductionMaterialId { get; set; }
        public decimal? OtheExpensePer { get; set; }
        public decimal? OtherExpense { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalRate { get; set; }

        public decimal NetCost { get; set; }
        public List<ProductionMaterialDetail> ProductionMaterialDetail { get; set; }
        public List<ProductionLabourDetail> ProductionLabourDetail { get; set; }
    }
    public class ProductionMaterialDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialDetailId { get; set; }
        public int ProductionId { get; set; }
        public int ProductionMaterialId { get; set; }
        public int RawMaterialId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitRate { get; set; }
        public int? UnitId { get; set; }

    }

    public class ProductionLabourDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LabourDetailId { get; set; }
        public int ProductionId { get; set; }
        public int ProductionMaterialId { get; set; }
        public int LabourWorkId { get; set; }
        public decimal? NoOfLabours { get; set; }
        public decimal? Rate { get; set; }


    }
}
