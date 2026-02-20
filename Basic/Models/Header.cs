using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class Header
    {
        public int Id { get; set; }
        public int MenuId { get; set; }

        public int Slno { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
