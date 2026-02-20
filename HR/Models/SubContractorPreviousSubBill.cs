using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class SubContractorPreviousSubBill
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  WorkOrderDetailsId { get; set; }
        public int WorkId { get; set; }
        public string LabourWorkName { get; set; }
        public int UnitId { get; set; }
        public string UnitShortName { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal WorkRate { get; set; }
        public decimal PreviousBillQty { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal? NegotiatedAmount { get; set; }

        public decimal? n { get; set; }

        public decimal? l { get; set; }

        public decimal? b { get; set; }
        public decimal? h { get; set; }
    }
}
