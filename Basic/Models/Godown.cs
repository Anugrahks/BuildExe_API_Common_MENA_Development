using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class Godown
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GodownId { get; set; }
        public string GodownName { get; set; }
        public string GodownAddess { get; set; }
        public DateTime  GodownDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
