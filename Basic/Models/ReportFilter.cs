using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class ReportFilter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string FilterFields { get; set; }
        public string FilterType { get; set; }
        public string SearchField { get; set; }
    }
}
