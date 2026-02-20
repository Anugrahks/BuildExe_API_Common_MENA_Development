using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class SpecificationNegotiation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SpecId { get; set; }
        public decimal NegotiatedRate { get; set; }
        public DateTime? NegotiatedDate { get; set; }

        public int SpecMasterId { get; set; }

        public int EstimationId { get; set; }

        public string SubId { get; set; }

        public string SpecName { get; set; }

        public int WorkNameId { get; set; }

        public int ProjSpecTableId { get; set; }

    }
}
