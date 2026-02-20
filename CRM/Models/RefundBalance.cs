using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class RefundBalance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public DateTime? VoucherDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RefundAmount { get; set; }
    }
}
