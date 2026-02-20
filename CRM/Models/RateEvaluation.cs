using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class RateEvaluation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int  ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public Decimal SubTotal { get; set; }

        public Decimal WaterElectricityPer { get; set; }
        public Decimal WaterElectricityCharge { get; set; }
        public Decimal LabourAdditionalPer { get; set; }
        public Decimal LabourAdditionalCharge { get; set; }

        public Decimal SubcontractAdditionalPer { get; set; }
        public Decimal SubcontractAdditionalCharge { get; set; }

        public Decimal ContractorProfit { get; set; }
        public Decimal ContractorProfitAmt { get; set; }
        public Decimal other_expense { get; set; }
        public Decimal other_expensePer { get; set; }
        public Decimal Tax { get; set; }
        public Decimal TaxAmount { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public ICollection <RateEvaluationDetails> RateEvaluationDetails { get; set; }

    }

    public class RateEvaluationDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateEvaluationDetailsId { get; set; }
        public int RateEvaluationId { get; set; }
        public int SpecItemTypeId { get; set; }
        public int SpecItemId { get; set; }
        public String SpecItemName { get; set; }
        public decimal QtyRequired { get; set; }
        public decimal SpecQty { get; set; }
        public decimal RateOfItem { get; set; }
        public decimal MarketRate { get; set; }
        public decimal RateOfConveyance { get; set; }
        public decimal MarketRateOfConveyance { get; set; }

    }
}
