using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class IndentDetailsList
    {
        public int IndentDetailsId { get; set; }
        public int IndentId { get; set; }

        public int ItemId { get; set; }
        public string MaterialName { get; set; }
        public int? MaterialBrandId { get; set; }
        public string? MaterialBrandName { get; set; }
        public int UnitId { get; set; }
        public string UnitShortName { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal ItemRate { get; set; }
        public decimal TaxPer { get; set; }
        public DateTime? RequiredDate { get; set; }
        public decimal PrevQuantityOrdered { get; set; }
        public int PurchaseFlag { get; set; }
        public int MaterialTypeId { get; set; }
        public string? Remarks { get; set; }
        public string? WorkCategoryName { get; set; }
        public string? WorkShortName { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }
        public string MaterialID { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }
        public string ConversionUnitName { get; set; }

    }
}
