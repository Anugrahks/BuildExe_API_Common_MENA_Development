using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Template
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplateNo { get; set; }
        public int WorkNameId { get; set; }
        //public int WorkTypeId { get; set;}
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 UserId { get; set; }
        public List<TemplateDetail> TemplateDetail { get; set; }
    }
    public class TemplateDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TemplateDetailId { get; set; }
        public int TemplateId { get; set; }
        public int ItemTypeId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }


    }
}
