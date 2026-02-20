using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class GeneralLedger
    {
        public DateTime Date { get; set; }
        public int? VoucherMasterId { get; set; }
        public int? VoucherTypeId { get; set; }
        public string? VoucherTypeName { get; set; }
        public int? VoucherNumber { get; set; }
        public string? Particular { get; set; }
        public decimal AccountOpening { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public Int16 Isopening { get; set; }
        public string? description { get; set; }
        public string? FormName { get; set; }
        public string? ProjectName { get; set; }
        public int? SlNo { get; set; }
        public string? ChequeNo { get; set; }

        public int IsVirtual { get; set; }
    }
}
