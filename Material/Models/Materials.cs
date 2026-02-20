using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class Materials
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
        public decimal MaterialUnitRate { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? DiscountPer { get; set; }
        public int UnitId { get; set; }
        public string? MaterialUnit { get; set; }
        public int MaterialBrandId { get; set; }
        public string? MaterialBrandName { get; set; }
    }
}
