using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class DayBookSummary
    {
        public DateTime Date { get; set; }
        public Int64? VoucherMasterId { get; set; }
        public string? VoucherTypeName { get; set; }
        public int? VoucherNumber { get; set; }
        public string? DebitAccountName { get; set; }
        public string CredtiAccountName { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
