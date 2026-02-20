using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string Color { get; set; }
        public Boolean Bold { get; set; }
        public int FontSize { get; set; }
        public int IsSelect { get; set; }
    }
}
