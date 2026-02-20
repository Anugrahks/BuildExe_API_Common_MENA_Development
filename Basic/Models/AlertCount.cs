using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class AlertCount
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int Counts { get; set; }
    }

    [Keyless]
    public class AlertCountNew
    {
        public int AlertType { get; set; }
        public string? AlertName { get; set; }
        public decimal Counts { get; set; }

    }
}
