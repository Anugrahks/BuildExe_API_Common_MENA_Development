using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    
    [Keyless]
    public class PurchaseOrderReprintModelView
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string SupplierName { get; set; }
        public string? OrderNo { get; set; }
        public DateTime? DateOrdered { get; set; }
    }
}
