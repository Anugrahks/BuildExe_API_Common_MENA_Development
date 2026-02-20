using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class ProjectSearch
    {
        public int? ProjectId { get; set; }

        public int? DivisionId { get; set; }
        public String ? ProjectTypeId { get; set; }
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? ReportId { get; set; }
    }
}
