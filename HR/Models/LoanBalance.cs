using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LoanBalance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public DateTime VoucherDate { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal LoanRecovery { get; set; }
    }
}
