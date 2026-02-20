using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class ProjectAnalysisDetail
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Type { get; set; }
        public string Particular { get; set; }
        public decimal Amount { get; set; }
        public int IsHeader { get; set; }

        public int? ParticularId { get; set; }
    }
}
