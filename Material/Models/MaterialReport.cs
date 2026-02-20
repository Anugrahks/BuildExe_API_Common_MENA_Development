using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
       // public int MaterialTypeId { get; set; }
        public string MaterialTypeName { get; set; }
       // public int MaterialBrandId { get; set; }
        public string MaterialBrandName { get; set; }
      //  public int MaterialCategoryId { get; set; }
        public string MaterialCategoryName { get; set; }
      //  public int UnitId { get; set; }
        public string UnitLongName { get; set; }
        public decimal MaterialUnitRate { get; set; }
        public decimal OpenigStock { get; set; }
        public decimal TaxPer { get; set; }
        public decimal KFCPer { get; set; }
        public decimal LandingCost { get; set; }
        public string HsnCode { get; set; }
        public string Remarks { get; set; }
        public int SlNo { get; set; }

    }
}
