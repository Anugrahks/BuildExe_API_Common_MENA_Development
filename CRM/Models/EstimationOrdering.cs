using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{

    [Keyless]
    public class EstimationOrdering
    {
        
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

    }

}
