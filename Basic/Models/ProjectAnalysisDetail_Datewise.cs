using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class ProjectAnalysisDetail_Datewise
    {
        public DateTime? EntryDate { get; set; }
        public string? EntryNo { get; set; }
        public string? UniqueId { get; set; }
        public string? Particular { get; set; }

        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public int? ParticularId { get; set; }
        public int Id { get; set; }
        public int DocExist {get; set; }
        public string TypeName {  get; set; }
        public string Description { get; set; }
    }
}
