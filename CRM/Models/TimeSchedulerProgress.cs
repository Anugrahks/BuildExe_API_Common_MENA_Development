using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class TimeSchedulerProgress
    {
        public int? ProjectId { get; set; }

        public int? DivisionId { get; set; }
        public int? ScheduleNumber { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string SubWorkName {  get; set; } 
    }
}
