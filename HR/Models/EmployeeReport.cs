using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string? EmailId { get; set; }
     //   public int EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
       // public int EmployeeDepartmentId { get; set; }
        public string? EmployeeDepartmentName { get; set; }
       // public int EmployeeDesignationId { get; set; }
        public string EmployeeDesignationName { get; set; }

        public string Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string? PhoneNo { get; set; }

        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }
        public int? SlNo { get; set; }
    }
}
