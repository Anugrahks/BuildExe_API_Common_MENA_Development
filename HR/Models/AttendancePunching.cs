using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class AttendancePunching
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string LogTime { get; set; }
        public int LogType { get; set; }
        public string DateWorked { get; set; }
        public string Pictures { get; set; }
        public string Location { get; set; }

        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Remarks { get; set; }

        public decimal TAAmount { get; set; }

    }
}
