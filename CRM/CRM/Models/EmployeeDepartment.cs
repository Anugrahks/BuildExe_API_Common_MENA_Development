using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class EmployeeDepartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeDepartmentId { get; set; }
        public string EmployeeDepartmentName { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
