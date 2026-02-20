using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class TableAttendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string Dateworked { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public decimal Work { get; set; }
        public decimal OThrs { get; set; }
        public decimal Wage { get; set; }
        public decimal OTAmount { get; set; }
        public decimal? OtherCharges { get; set; }
        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int? LabourHead { get; set; }
        public int IsGroup { get; set; }
    }
}
