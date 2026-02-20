using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class SubContractorRateRevision
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int SubContractorWorkOrderId { get; set; }
        public int WorkId { get; set; }
        public decimal WorkRate { get; set; }
        public DateTime DateRevised { get; set; }
    }
}

