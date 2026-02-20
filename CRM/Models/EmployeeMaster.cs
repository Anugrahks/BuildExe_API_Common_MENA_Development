using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public int EmployeeDesignationId { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public int LabourHead { get; set; }
        public string EmployeeSalaryTypeId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfJoining { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }
        public string PfNo { get; set; }
        public string EsiNo { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
