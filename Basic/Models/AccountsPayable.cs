using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class AccountsPayable
    {
        public int AccountHeadId { get; set; }
        public string? Particular { get; set; }
        public decimal? OpeningBalance { get; set; }

        public string? ProjectName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal? Balance { get; set; }
       
    }
}
