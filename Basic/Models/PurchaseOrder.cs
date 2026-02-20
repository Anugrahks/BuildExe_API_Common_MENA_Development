using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime DateOrdered { get; set; }
        public string UnitName { get; set; }
        public string BlockName { get; set; }
        public string FloorName { get; set; }
        public string OrderNo { get; set; }
        public int orderId { get; set; }
        public string SupplierName { get; set; }
        public string ProjectName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string WorkNameId { get; set; }
    }
}
