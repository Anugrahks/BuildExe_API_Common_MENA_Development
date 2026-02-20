using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class AccountSubGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountSubGroupId { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountGroupId { get; set; }
        public string AccountSubGroupName { get; set; }
        public string Description { get; set; }
        public Int16? CompanyId { get; set; }
        public Int16? BranchId { get; set; }
    }
}
