using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        public string DepartmentShortName { get; set; }
        public string DepartmentLongName { get; set; }
        public string DepartmentCategory { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }

        public int EmployeeDept { get; set; }

        public int IsMD { get; set; }

    }
}
