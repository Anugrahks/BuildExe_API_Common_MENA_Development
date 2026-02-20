using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class EmployeeDesignation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeDesignationId { get; set; }
        public string EmployeeDesignationName { get; set; }
        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int marketing { get; set; }
        
    }
}
