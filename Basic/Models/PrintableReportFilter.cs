using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class PrintableReportFilter
    {
        public int PrintableReportFilterId { get; set; }
        public int MenuId { get; set; }
        public string FilterFields { get; set; }
        public string FilterType { get; set; }
        public string SearchField { get; set; }
    }
}
