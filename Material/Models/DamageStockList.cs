using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class DamageStockList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Entrydate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }

        public string? FloorName { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialUnitId { get; set; }
        public string UnitShortName { get; set; }
        public decimal Stock { get; set; }
        public decimal Rate { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int ViewType { get; set; }
        public int MaterialTypeId { get; set; }
        public string? RejectRemarks { get; set; }
    }
}
