using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class ItemQuotationBaseRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int SupplierId { get; set; }
        public decimal  BaseRate { get; set; }
        public int Preference { get; set; }
        public string  Remarks { get; set; }
        public decimal  MinimumOrderQuantity { get; set; }
        public int IndentId { get; set; }
        public int PurchaseOrderId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
    }
}
