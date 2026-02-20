using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class AccountHead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountHeadId { get; set; }
        public string? AccountHeadName { get; set; }
        public int? AccountTypeId { get; set; }
        public int? AccountGroupId { get; set; }
        public int? AccountSubGroupId { get; set; }
        public string? AccountSubGroupName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public decimal? OpeningAmount { get; set; }
        public string? OpeningType { get; set; }
        public string? Description { get; set; }
        public string? Editable { get; set; }
        public Int16? financialyearId { get; set; }
        public int? UserId { get; set; }
        public int InJournal { get; set; }
        public int? IsChangeAmt { get; set; }
        public int? IsDefault { get; set; }
    }
}
