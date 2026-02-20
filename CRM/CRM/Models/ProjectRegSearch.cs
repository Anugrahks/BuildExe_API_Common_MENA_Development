using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class ProjectRegSearch
    {
        public int Id { get; set; }
        public string? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
    }
}
