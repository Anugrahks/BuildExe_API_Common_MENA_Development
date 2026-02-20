using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class TendorNegotiation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime? TenderNegotiationDate { get; set; }
        public decimal? TenderNegotiationAmount { get; set; }
        public decimal? TenderNegotiationPercent { get; set; }
        public string? TenderNegotiationNarration { get; set; }
        public decimal? TenderRevisedAmount { get; set; }
       
    }
   
}
