using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class Attendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public int LabourHead { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? EstimatedToDate { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 BulkEntry { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }

        public string Remarks { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public Int16 IsDeleted { get; set; }
        public int? WorkOrderId { get; set; }
        public Int16 UserId { get; set; }
        public int AttendanceWise { get; set; }
        public List<AttendanceDetail> AttendanceDetail { get; set; }


    }
    public class AttendanceDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceDetailId { get; set; }
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public DateTime DateWorked { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Work { get; set; }
        public decimal OverTime { get; set; }
        public decimal OverTimeAmount { get; set; }
        public int Category { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? PenaltyAmount { get; set; }
        public decimal? OtherCharges { get; set; }
        public int LabourHead { get; set; }
        public int IsHours { get; set; }
        public string? LoginLocation { get; set; }
        public string? LogoutLocation { get; set; }
        public string? LoginPictures { get; set; }
        public string? LogoutPictures { get; set; }
        public int PunchedIn { get; set; }
        public string? Remarks { get; set; }

        public List<AttendancePunchingDetailsAttendance> AttendancePunchingDetails { get; set; }
    }

    public class AttendancePunchingDetailsAttendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AttendanceDetailId { get; set; }
        public int AttendanceId { get; set; }
        public int AttendancePayrollId { get; set; }
        public decimal Distance { get; set; }
        public int EmployeeId { get; set; }
        public decimal LoginLatitude { get; set; }
        public decimal LoginLongitude { get; set; }
        public string LoginLocation { get; set; }
        public string LoginPictures { get; set; }
        public DateTime LoginTime { get; set; }
        public decimal LogoutLatitude { get; set; }
        public decimal LogoutLongitude { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int ProjectId { get; set; }
        public int PunchedIn { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }

        public string? Remarks { get; set; }

    }
}
