using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class BoqDetailsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BoqDetailsListId { get; set; }
        public int BoqMasterId { get; set; }
        public int? TemplateId { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public int BrandId { get; set; }
        public string? MaterialBrandName { get; set; }
        public decimal QtyRequired { get; set; }
        public decimal RateOfItem { get; set; }

        public int MasApproval { get; set; }
    }
}
