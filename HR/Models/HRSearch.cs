using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    [Keyless]
    public class HRSearch
    {
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? FinancialYearId { get; set; }

        public DateTime Date { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }
        public int? UnitId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime?joiningDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? EmployeeCategoryId { get; set; }
        public int? EmployeeLabourGroupId { get; set; }
        public int? LabourHead { get; set; }
        public int? EmployeeDepartmentId { get; set; }
        public int? EmployeeDesignationId { get; set; }

        public int IsOpening { get; set; }
        public string? MultiEmployeeId { get; set; }
        public int? CategoryId { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? WorkOrderStatus { get; set; }
        public int? WorkOrderId { get; set; }
        public String? PaymentType { get; set; }
        public String? PaymentMode { get; set; }
        public int? MonthId { get; set; }
        public int? ViewType { get; set; }
        public int? WorkTypeId  { get; set; }
        public int? ReportId { get; set; }
        public int? WorkNameId { get; set; }
        public int? UserId { get; set; }
        public int? YearId { get; set; }
        public int? MonthlyVaryingHead { get; set; }
        public int? MonthlyWise { get; set; }
        public int? WorkId { get; set; }

        public int? DurationId { get; set; }
        public int? LabourWorkId { get; set; }
        public int? ProjectWise {  get; set; }
        public int? ApprovalLevel { get; set; }

        public int Id { get; set; }


        public int? ItemId { get; set; }


        public string? ItemCode { get; set; }

        public int ActionButton { get; set; }

        public string? ProjectIds { get; set; }

        public string? Attendance { get; set; }
        public string? CategoryIds { get; set; }

        public DateTime DateAssigned { get; set; }

        public List<ProjectDateRange> JsonData { get; set; } = new List<ProjectDateRange>();
        public int? LeaveId { get; set; }
        public int? CarryForward { get; set; }
        public int? ContractorId { get; set; }

        public string EmployeeStatus { get; set; }
    }

    public class ProjectDateRange
    {
        public int ProjectId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
