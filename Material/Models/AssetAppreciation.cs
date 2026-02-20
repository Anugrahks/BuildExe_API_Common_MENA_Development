
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class AssetAppreciation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public string Remarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int UserId { get; set; } = 0;

        public List<AssetAppreciationDetails> AssetAppreciationDetails { get; set; }

    }

    public class AssetAppreciationDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int? MaterialId { get; set; }
        public int? AccountHeadId { get; set; }
        public string ItemCode { get; set; }
        public string AssetName { get; set; }
        public decimal CurrentValue { get; set; } = 0;
        public string Type { get; set; }
        public decimal PercentOfAllocation { get; set; } = 0;
        public decimal AmountOfAllocation { get; set; } = 0;
        public decimal CostOfAsset { get; set; } = 0;

    }

 
}
