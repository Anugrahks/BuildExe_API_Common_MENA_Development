using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class PendingClientBills
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PendingClientBillsId { get; set; }
        public int Type { get; set; }



        public DateTime? BillDate { get; set; }
        public String? BillNo { get; set; }
        public String  Description { get; set; }
        public decimal  BillAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Advance { get; set; }


    }
}
