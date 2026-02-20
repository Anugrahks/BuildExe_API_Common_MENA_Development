using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class DayBook
    {
        public DateTime Date { get; set; }
        public int? VoucherMasterId { get; set; }
        public string? VoucherTypeName { get; set; }
        public int? VoucherNumber { get; set; }
        public string?  Particular { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string? Description { get; set; }
    }
}
