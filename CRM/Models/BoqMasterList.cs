using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class BoqMasterList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
       // public int? WorkTypeNameId { get; set; }
       // public string? WorkTypeName { get; set; }
       
        public int? WorkNameId { get; set; }
        public string? WorkShortName { get; set; }

        public int? CategoryId { get; set; }
        public string? WorkCategoryName { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Decimal Qty { get; set; }
        public Decimal WaterElectricityChargePer { get; set; }
        public Decimal WaterElectricityCharge { get; set; }
        public Decimal LabourAdditionalChargeper { get; set; }
        public Decimal LabourAdditionalCharge { get; set; }
        public Decimal SubcontractAdditionalChargePer { get; set; }
        public Decimal SubcontractAdditionalCharge { get; set; }
        public Decimal ContractorProfitPer { get; set; }
        public Decimal ContractorProfitAmt { get; set; }
        public Decimal Other_expense { get; set; }
        public Decimal TaxPer { get; set; }
        public Decimal TaxAmount { get; set; }
        public Decimal NetAmount { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int Maxlevel { get; set; }
    }
}
