using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class MaterialProjectSearchList
    {
        public int? Id { get; set; }
        public string? MaterialID { get; set; }
        public int? MaterialBrandId { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? ProjectId { get; set; }
        public int? UnitId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public Int16? CompanyId { get; set; }
        public Int16? BranchId { get; set; }
        public Int16? FinancialYearId { get; set; }
        public int? withStock { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int IsEdit { get; set; }
        public int? DivisionId { get; set; } 
    }
}
