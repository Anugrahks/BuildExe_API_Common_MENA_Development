using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class PayrollAttendanceForApproval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }

        public int EmployeeDesignationId { get; set; }
        public string EmployeeDesignationName { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Work { get; set; }
        public decimal OverTime { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal OtherCharges { get; set; }
        public int Category { get; set; }
        public string? WorkCategoryName { get; set; }

        public int? WorkNameId { get; set; }
        public string? WorkShortName { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal DaySalaryAmount { get; set; }
        public decimal OvertimeRate { get; set; }
        public DateTime? time_in { get; set; }
        public DateTime? time_out { get; set; }
        public decimal? relaxation { get; set; }
        public decimal? BreakHours { get; set; }
        public int IsSelect { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }
        public int? UnitId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public string? ProjectName { get; set; }
        public string? UnitName { get; set; }
        public string? BlockName { get; set; }
        public string? FloorName { get; set; }

        public int? StatusCode { get; set; }
        public String? ErrorMessage { get; set; }
        public DateTime? LastDateTo { get; set; }
        public decimal? PenaltyAmount { get; set; }
        public int IsHours { get; set; }
        public string? LoginLocation { get; set; }
        public string? LogoutLocation { get; set; }
        public string? LoginPictures { get; set; }
        public string? LogoutPictures { get; set; }
        public int PunchedIn { get; set; }

    }
}
