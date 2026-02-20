using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    [Keyless]
    public class SalaryVaryingHeadDetails
    {
        public int HeadType { get; set; }
       public int SalaryItemHeadId { get; set; }  
        public string HeadName { get; set; }
        public decimal? Percentage { get; set; }
        public decimal Amount { get; set; }
    }
}
