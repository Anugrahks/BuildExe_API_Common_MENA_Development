using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class MaterialusageList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ConsumptionMasterId { get; set; }
        public DateTime UsageDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int? EmployeeCategoryId { get; set; }
        public string? EmployeeCategoryName { get; set; }
        public int? IssuedTo { get; set; }
        public string?  FullName { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int? WorkCategory { get; set; }
        public string? WorkCategoryName { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int ViewType { get; set; }
        public int? WorkNameId { get; set; }
        public int? BulkEntry { get; set; }
    }
}
