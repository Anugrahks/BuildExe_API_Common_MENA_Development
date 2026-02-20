using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class SpecSearch
    {
        public int? DepartmentId { get; set; }
        public int? EstimationId { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }

        public int Action {  get; set; }

        public string EstimationIds { get; set; }

        public string? SubId { get; set; }

        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int BranchId { get; set; }

        public List<JsonDataRange> JsonData { get; set; } = new List<JsonDataRange>();


    }

    public class JsonDataRange
    {
        public int ProjSpecTableId { get; set; }
        public int OrderId { get; set; }
    }
}
