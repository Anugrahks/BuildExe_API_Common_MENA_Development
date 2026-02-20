using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class ReportConfiguration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string? TypeName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int TextCaseType { get; set; }
        public int ViewWise { get; set; }

        public int Allignment { get; set; }
        public List<ConfigurationFieldDetails> ConfigurationFieldDetails { get; set; }
        public List<ConfigurationFilterDetails> ConfigurationFilterDetails { get; set; }
        //public String?  MenuName { get; set; }
        //public String? FieldName { get; set; }
        //public int ReportFilterId { get; set; }
        //public String? FilterFields { get; set; }
        //public String? FilterType { get; set; }

    }
    public class ConfigurationFieldDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationFieldDetailsId { get; set; }
        public int ReportConfigurationId { get; set; }
        public int ReportFieldId { get; set; }
        public int Order { get; set; }

    }
    public class ConfigurationFilterDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationFilterDetailsId { get; set; }
        public int ReportConfigurationId { get; set; }
        public int ReportFilterId { get; set; }
    }

    public class PrintableTemplate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string? TemplateCode { get; set; }
        public string? Content { get; set; }
        public int? UserId { get; set; }

    }
}
