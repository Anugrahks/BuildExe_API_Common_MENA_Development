using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class StockSearch
    {
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ProjectId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? UnitId { get; set; }

        public int? DivisionId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? MaterialBrandId { get; set; }
        public int? MaterialCategoryId { get; set; }
        public int? MaterialId { get; set; }

        public int? ReportId { get; set; }
    }
}
