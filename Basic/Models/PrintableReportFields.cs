using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class PrintableReportFields
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string ParentCollectionName { get; set; }

    }
}
