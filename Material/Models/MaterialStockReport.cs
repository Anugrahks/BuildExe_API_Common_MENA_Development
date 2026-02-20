using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialStockReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
        public decimal MaterialUnitRate { get; set; }
      
        public string MaterialUnit { get; set; }
        public string? MaterialBrandName { get; set; }
        public decimal? Stock { get; set; }
        public decimal? DamageStock { get; set; }

        public string ProjectName { get; set; }
        public string? UnitName { get; set; }
        public string? BlockName { get; set; }
        public string? FloorName { get; set; }
    }
}
