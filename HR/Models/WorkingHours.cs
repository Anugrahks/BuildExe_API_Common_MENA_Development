using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace BuildExeHR.Models
{
    public class WorkingHours
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int designationid { get; set; }
        public DateTime  time_in { get; set; }
        public DateTime time_out { get; set; }
        public decimal  relaxation { get; set; }
        public Int16 UserId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public decimal break_hours { get; set; }
        public int isdefault { get; set; }

        public decimal otrelaxation   { get; set; }

        public int LatePenaltyHalfDay { get; set; }


        public decimal LatePenaltyHours { get; set; }

        public List<LatePenaltyCustomization> LatePenaltyCustomization { get; set; }

    }
    public class LatePenaltyCustomization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WorkingHoursId { get; set; }
        public int DesignationId { get; set; }
        public decimal PenaltyAfterMinutes { get; set; }
        public decimal HourSalary { get; set; }
       

    }
}
