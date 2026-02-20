
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class BalanceSheet
    {
        public int? AccountHeadId { get; set; }
        public int? AccountTypeId { get; set; }
        public int? AccountGroupId { get; set; }
        public int? AccountSubGroupId { get; set; }
        public string? AccountHeadName { get; set; }
        public string? AccountTypeName { get; set; }
        public string? AccountGroupName { get; set; }
        public string? AccountSubGroupName { get; set; }
        public decimal Amount { get; set; }

    }
}

