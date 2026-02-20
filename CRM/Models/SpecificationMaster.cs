using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class SpecificationMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Spec_Id{ get; set; }
        public string SpecNumber { get; set; }
        public string SpecName { get; set; }
    
        public string SacCode { get; set; }
        public string SpecDescription { get; set; }
        public int DepartmentId { get; set; }
        public int WorkTypeId { get; set; }
        public int UnitId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        public Decimal SpecUnit { get; set; }
        public Decimal RatePerUnit { get; set; }
        public Decimal DeptRatePerUnit { get; set; }
        public Decimal LabourAdditionalChargePer { get; set; }
        public Decimal SubcontractAdditionalChargePer { get; set; }
        public Decimal WaterElectricityChargePer { get; set; }
        public Decimal WaterElectricityCharge { get; set; }
        public Decimal LabourAdditionalCharge { get; set; }
        public Decimal SubcontractAdditionalCharge { get; set; }
        public Decimal ContractorProfit { get; set; }
        public Decimal ContractorProfitAmt { get; set; }
        public Decimal other_expense { get; set; }
        public Decimal OtherExpensePer { get; set; }
        public Decimal CostIndex { get; set; }
        public Decimal Tax { get; set; }
        public Decimal TaxAmount { get; set; }
        public Decimal SpecSubTotal { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }

        public decimal CustomsDuty { get; set; }

        public decimal DOCharge { get; set; }

        public decimal HandlingCharge { get; set; }
        public decimal MOFAAttestation { get; set; }
        public decimal DocumentationCharge { get; set; }
        public decimal StorageCharge { get; set; }

        public List<SpecificationDetails > SpecificationDetails  { get; set; }
    }
    public class SpecificationDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int SpecificationDetailsId { get; set; }
        public int SpecificationMasterId { get; set; }
        public int SpecItemTypeId { get; set; }
        public int SpecItemId { get; set; }
        public decimal QtyRequired { get; set; }
        public decimal RateOfItem { get; set; }
        public decimal RateOfConveyance { get; set; }
        public decimal WastagePer { get; set; }

        public decimal WastageAmount { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }

    }
}
