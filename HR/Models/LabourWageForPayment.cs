using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LabourWageForPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
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
        public decimal DaysWorked { get; set; }
        public decimal DailyWageAmount { get; set; }
        public decimal Salary { get; set; }
        public decimal OverTimeHrs { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal OverTimeRate { get; set; }
        public decimal TotalWage { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal Othercharges { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal NetPayable { get; set; }
        public decimal? AdvanceBalance { get; set; }
        public int? AttendanceId { get; set; }
        public int IsAdvanceByProject { get; set; }
        public int IsHours { get; set; }

    }
}
