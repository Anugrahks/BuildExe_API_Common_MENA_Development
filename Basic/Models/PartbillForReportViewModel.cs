using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class PartBillForReportViewModel
    {
        public PartBillForReportViewModel()
        {
            ReportFields = new List<KeyValuePair<string, string>>();
            TableTypeReportFields = new List<TableTypeReportFieldsViewModel>();
        }
        public List<KeyValuePair<string, string>> ReportFields { get; set; }
        public List<TableTypeReportFieldsViewModel> TableTypeReportFields { get; set; }
    }
}
