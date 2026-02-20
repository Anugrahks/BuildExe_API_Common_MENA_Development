using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class TendorAnalysis
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int TendorSubmittedId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractorName { get; set; }
        public decimal? MarksGiven { get; set; }
        public string? Narration { get; set; }
        public int? Position { get; set; }
        public decimal? TenderAmount { get; set; }
        public int? UserId { get; set; }
    }
}
