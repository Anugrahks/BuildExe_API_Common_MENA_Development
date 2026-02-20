using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class BoqMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int WorkNameId { get; set; }
        public int? CategoryId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
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
        public Decimal  NetAmount { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public List<BoqDetails> BoqDetails { get; set; }
    }

    public class BoqDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BoqDetailsId { get; set; }
        public int BoqMasterId { get; set; }
        public int? TemplateId { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemId { get; set; }
        public int BrandId { get; set; }
        public decimal QtyRequired { get; set; }
        public decimal RateOfItem { get; set; }

        public int MasApproval { get; set; }

    }
}
