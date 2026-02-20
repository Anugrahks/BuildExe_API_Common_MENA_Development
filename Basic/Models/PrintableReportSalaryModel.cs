using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class PrintableReportSalaryModel
    {
        public PrintableReportSalaryModel()
        {
            ReportFields = new List<KeyValuePair<string, string>>();
            TableTypeReportFields = new List<TableTypeReportFieldsViewModel>();
            TableTypeReportFields2 = new List<TableTypeReportFieldsViewModel>();
            TableTypeReportFields3 = new List<TableTypeReportFieldsViewModel>();
        }
        [JsonProperty("reportFields")]
        public List<KeyValuePair<string, string>> ReportFields { get; set; }

        [JsonProperty("tableTypeReportFields")]
        public List<TableTypeReportFieldsViewModel> TableTypeReportFields { get; set; }

        [JsonProperty("tableTypeReportFields2")]
        public List<TableTypeReportFieldsViewModel> TableTypeReportFields2 { get; set; }

        [JsonProperty("tableTypeReportFields3")]
        public List<TableTypeReportFieldsViewModel> TableTypeReportFields3 { get; set; }
    }
}
