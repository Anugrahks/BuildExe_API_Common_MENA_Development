using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class ReportFieldsValuesViewModel
    {
        [JsonProperty("reportFields")]
        public List<KeyValuePair<string, string>> ReportFields { get; set; }
    }
}
