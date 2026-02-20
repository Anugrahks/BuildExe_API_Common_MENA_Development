using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class UserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
