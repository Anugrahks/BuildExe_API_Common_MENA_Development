using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class FinancialYear
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinancialYearId { get; set; }
        public string Financial_Year { get; set; }
        public int CompanyId { get; set; }
        public string Active { get; set; }
        public string Status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
