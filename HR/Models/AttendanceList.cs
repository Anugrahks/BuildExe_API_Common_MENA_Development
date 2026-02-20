using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
namespace BuildExeHR.Models
{
    [Keyless]
    public class AttendanceList
    {
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public string? EmployeeLabourGroupName { get; set; }
        public int LabourHead { get; set; }
        public string? LabourHeadName { get; set; }
        public string Remarks { get; set; }

        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string ProjectName { get; set; }
        public string DivisionShortName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public DateTime DateWorked { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public int? WorkOrderId { get; set; }
        public int  Maxlevel { get; set; }
        public int? WorkNameId { get; set; }
        public int? WorkCategoryId { get; set; }


    }


    [Keyless]
    public class AttendancePayrollList
    {
        public int Id { get; set; }
        public DateTime DateWorked { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        public int LabourHead {  get; set; } 

    }
}
