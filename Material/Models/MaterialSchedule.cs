using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class MaterialSchedule
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public decimal? ScheduledQty_Project { get; set; }
        public decimal? ScheduledQty_Category { get; set; }
        public decimal? PurchasedQty_Project { get; set; }
        public decimal? PurchasedQty_Category { get; set; }
        public decimal? Balance_Project { get; set; }
        public decimal? Balance_Category { get; set; }
     
        public decimal? PurchaseApprovalPendingQty { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? Stock { get; set; }
        public decimal? OtherProjectStock { get; set; }


    }
}
