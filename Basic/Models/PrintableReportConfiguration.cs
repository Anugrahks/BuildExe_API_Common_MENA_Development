using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class PrintableReportConfiguration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public string TemplateName { get; set; }
        public int UserId { get; set; }
        public string TemplateStructure { get; set; }
        public string WatermarkText { get; set; }
        public string PageSize { get; set; }

    }

    public class PrintableReportCSS {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int MenuId { get; set; }
        public int ColumnNumber { get; set; }
        public string CssText { get; set; }
        public string ColumnHeading { get; set; }
        public string ColumnHeadingcss { get; set; }

        public string? TableIdentifier { get; set; }

    }
}
