using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeDesignation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmployeeDesignationName { get; set; }
        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public int EmployeeCategoryId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public byte  marketing { get; set; }
        public Int16 UserId { get; set; }
    }
}
