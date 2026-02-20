using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuildExeBasic.Models
{
    public class TableTypeReportFieldsViewModel
    {
        public TableTypeReportFieldsViewModel()
        {
            TableTypeReportFieldValues = new List<ReportFieldsValuesViewModel>();
        }
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        [JsonProperty("tableTypeReportFieldValues")]
        public List<ReportFieldsValuesViewModel> TableTypeReportFieldValues { get; set; }
    }
}
