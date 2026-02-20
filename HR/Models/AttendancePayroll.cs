using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class AttendancePayroll
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public DateTime? Dateworked { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }

        public string Remarks { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 UserId { get; set; }
        public int Isholiday { get; set; }


        public int DepartmentId { get; set; }


        public string DepartmentName { get; set; }
        public List<AttendanceEmployeeDetail> AttendanceEmployeeDetail { get; set; }


    }

    public class AttendanceEmployeeDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeDetailId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int? LeaveId { get; set; }

        public decimal? LeaveCount { get; set; }
        public decimal TotalWork {get; set;}
        public decimal TotalOvertime { get; set; }
        public decimal? PenaltyAmount { get; set; }
        public int? IsEmployeeHoliday { get; set; }
        public int PunchedIn { get; set; }
        public string Remarks { get; set; }
        public decimal TotalOverTimeAmount { get; set; }

        public decimal LateHours { get; set; }

        public int IsLeaveTagged { get; set; }
        public decimal TAAmount { get; set; }

        public string DocumentName { get; set; }

        public decimal TotalBreakHours { get; set; }

        public string Document { get; set; }

        public List<AttendancePayrollDetail> AttendancePayrollDetail { get; set; }

        public List<AttendancePunchingDetails> AttendancePunchingDetails { get; set; }

        public List<AttendancePayrollSpecialOTDetails> AttendancePayrollSpecialOTDetails { get; set; }
    }

    public class AttendancePayrollSpecialOTDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceSpecialOTDetailsId { get; set; }
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public int SpecialOTId { get; set; }
        public decimal SpecialOTRate { get; set; }

        public decimal SpecialOTHours { get; set; }

        public decimal SpecialOTAmount { get; set; }
    }
    public class AttendancePayrollDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceDetailId { get; set; }
        public int AttendanceId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public DateTime? Dateworked { get; set; }
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
        public string? LoginLocation { get; set; }
        public string? LogoutLocation { get; set; }
        public string? LoginPictures { get; set; }
        public string? LogoutPictures { get; set; }
        public int PunchedIn { get; set; }
    }


    public class AttendancePunchingDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string? Remarks { get; set; }

        public decimal TAAmountPunching { get; set; }
    }

}
