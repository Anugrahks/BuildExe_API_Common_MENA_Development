using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class Expense
    {
        public string? Type { get; set; }
        public string? Particular { get; set; }
        public decimal? Amount { get; set; }
    }
}
