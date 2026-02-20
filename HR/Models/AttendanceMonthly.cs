using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class AttendanceMonthly
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AttendanceType { get; set; }
        public int YearId { get; set; }
       
        public int MonthId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
      
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16  IsReject { get; set; }
        public int IsDeleted { get; set; }

        public int MDurationId { get; set; }


        public DateTime MFromDate { get; set; }

        public DateTime MToDate { get; set; }

        public int MEmployeeMasterId { get; set; }
        public List<AttendanceMonthlyEmployeeDetails> AttendanceMonthlyEmployeeDetails { get; set; }

    }
    public class AttendanceMonthlyEmployeeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceMonthlyEmployeeId { get; set; }
        public int AttendanceMonthlyId { get; set; }
        public int EmployeeId { get; set; }
        public int TotalNoofDays { get; set; }
        public int NoofHolidays { get; set; }
        public int NoofWorkingDays { get; set; }
        public decimal NoofDaysWorked { get; set; }

        public decimal NoofHolidaysWorked { get; set; }
        public decimal NoofHolidaysLeave { get; set; }
        public decimal TotalNoofLeave { get; set; }
        public decimal Amount { get; set; }
        public decimal OverTime { get; set; }
        public decimal OverTimeAmount { get; set; }

        public int LeaveId { get; set; }

        public List<AttendanceMonthlyDetails> AttendanceMonthlyDetails { get; set; }
        public List<AttendanceLeaveDetails> AttendanceLeaveDetails { get; set; }

        public List<AttendanceSpecialOTDetails> AttendanceSpecialOTDetails { get; set; }

        
    }

    public class AttendanceMonthlyDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceMonthlyDetailsId { get; set; }
        public int AttendanceMonthlyId { get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }

        public int? CategoryId { get; set; }
        public decimal? NoofWorkedDays { get; set; }
        public decimal? NoofWorkedHolidays { get; set; }
        public decimal Othrs { get; set; }
    }

    public class AttendanceLeaveDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceMonthlyLeavesId { get; set; }
        public int AttendanceMonthlyId { get; set; }

        public decimal? NoofLeaves { get; set; }
        public int? LeaveId { get; set; }
    }

    public class AttendanceSpecialOTDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceSpecialOTDetailsId { get; set; }
        public int AttendanceMonthlyId { get; set; }

        public int? SpecialOTId { get; set; }
        public decimal? SpecialOTRate { get; set; }

        public decimal? SpecialOTHours { get; set; }

        public decimal? SpecialOTAmount { get; set; }
    }

}
