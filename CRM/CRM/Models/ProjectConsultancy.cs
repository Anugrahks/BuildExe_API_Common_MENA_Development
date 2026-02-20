using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ProjectConsultancy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ProjectId { get; set; }
        public int Workid { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public Int16 UserId { get; set; }
    }
}
