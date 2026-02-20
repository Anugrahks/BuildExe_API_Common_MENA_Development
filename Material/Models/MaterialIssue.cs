using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    //public class MaterialIssue
    //{

    //    public int Id { get; set; }
    //    public DateTime? IssueDate { get; set; }
    //    public int DivisionId { get; set; }
    //    public int? ProjectId { get; set; }
    //    public int? BlockId { get; set; }
    //    public int? FloorId { get; set; }
    //    public int? UnitId { get; set; }
    //    public int? TypeId { get; set; }
    //    public int? EmpployeeCategoryId { get; set; }
    //    public int? EmployeeId { get; set; }
    //    public int? StageId { get; set; }
    //    public int? WorkCategoryId { get; set; }
    //    public int? WorkNameId { get; set; }
    //    public int? ApprovalStatus { get; set; }
    //    public int? ApprovalLevel { get; set; }
    //    public short? ApprovedBy { get; set; }
    //    public DateTime? ApprovedDate { get; set; }
    //    public string ApprovalRemarks { get; set; }
    //    public string RejectRemarks { get; set; }
    //    public DateTime? EnteredOnDate { get; set; }

    //    public List<MaterialIssueDetail> MaterialIssueDetail { get; set; }
    //}

    //public class MaterialIssueDetail
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int IssueDetailId { get; set; }
    //    public int IssueMasterId { get; set; }
    //    public int MaterialTypeId { get; set; }
    //    public int MaterialId { get; set; }
    //    public decimal IssueQuantity { get; set; }

    //}





    public class MaterialIssue
    {
        public int Id { get; set; } 
        public DateTime IssueDate { get; set; }
        public int DivisionId { get; set; }
        public int IssueId { get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int UnitId { get; set; }
        public int TypeId { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }
        public string StageName { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public short ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int? IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; } 
        public int UserId { get; set; }
        public int IsReject { get; set; }
        public virtual List<MaterialIssueDetail> MaterialIssueDetail { get; set; }
    }



    public class MaterialIssueDetail
    {
        public int IssueDetailId { get; set; } 
        public int IssueMasterId { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialId { get; set; }
        public decimal IssueQuantity { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }


}
