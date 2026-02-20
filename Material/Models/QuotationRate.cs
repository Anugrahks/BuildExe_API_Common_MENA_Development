using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class QuotationRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuotationRateId { get; set; }
        public int QuotationId { get; set; }
        public int SupplierId { get; set; }

        public int DivisionId { get; set; }
        public int MaterialId { get; set; }
        public int BrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int Preference { get; set; }

        public int UserId { get; set; }

    }
}
