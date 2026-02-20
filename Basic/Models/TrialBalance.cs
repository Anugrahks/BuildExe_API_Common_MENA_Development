using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class TrialBalance
    {
        public int? AccountHeadId { get; set; }
        public string? AccountHeadName { get; set; }
        public int? AccountTypeId { get; set; }
        public string? AccountTypeName { get; set; }
        public int? AccountGroupId { get; set; }
        public string? AccountGroupName { get; set; }
        public int? AccountSubGroupId { get; set; }
        public string? AccountSubGroupName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public decimal OpeningAmount { get; set; }

    }
}
