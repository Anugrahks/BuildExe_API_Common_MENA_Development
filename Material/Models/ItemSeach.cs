using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class ItemSearch
    {
        [Required]
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitRent {  get; set; }
        public DateTime ReturnTranferDate { get; set; }
        public int TypeId   { get; set; }
        public int SupplierId   { get; set; }
        public int ItemSupplier {  get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public decimal GstPercent { get; set; }
        public decimal DiscountPer { get; set; }
        public int FinancialYearId { get; set; }
        public int BranchId { get; set; }

    }
}
