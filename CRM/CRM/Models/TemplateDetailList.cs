using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class TemplateDetailList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TemplateDetailListId { get; set; }
        public int TemplateId { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Unit_Id { get; set; }
        public string  UnitName { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }


        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }
}
