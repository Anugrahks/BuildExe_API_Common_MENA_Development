using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class SpecificationDetailsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int SpecificationDetailsId { get; set; }
        public int SpecificationMasterId { get; set; }
        public int SpecItemTypeId { get; set; } 
        public int SpecItemId { get; set; }
        public String  SpecItemName { get; set; }
        public int Unit_id { get; set; }
        public String Unit { get; set; }
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
