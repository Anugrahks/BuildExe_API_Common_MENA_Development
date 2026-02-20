using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class PurchaseForPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PurchaseInvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
      //  public int SupplierId { get; set; }
        public int Project_Id { get; set; }
        public string  ProjectName { get; set; }
        public int Unit_Id { get; set; }
        public string UnitName { get; set; }
        public int Block_Id { get; set; }
        public string BlockName { get; set; }
        public int Floor_Id { get; set; }
        public string FloorName { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Payment { get; set; }
        public decimal BillAmountBalance { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Advance { get; set; }
        public decimal? DebitNoteAdjustmentAmount { get; set; }

    }
}
