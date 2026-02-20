using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class AdvanceBalance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public DateTime? VoucherDate { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? AdvanceRecovery { get; set; }
    }
}
