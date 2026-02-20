using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class TableAttendanceGet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int Employeeid { get; set; }
        public string FullName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DateWorked { get; set; }
        public decimal SalaryAmount { get; set; }
        public DateTime? Timein { get; set; }
        public DateTime? Timeout { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public decimal OverTime { get; set; }
        public decimal Amount { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal OtherCharges { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Work { get; set; }

        public int WorkNameId { get; set; }

        public string WorkShortName { get; set; }
        public decimal OvertimeRate { get; set; }
        public int LabourHead { get; set; }
        public int IsGroup { get; set; }
    }
}
