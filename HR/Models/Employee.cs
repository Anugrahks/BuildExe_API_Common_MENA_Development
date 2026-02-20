using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public string? EmployeeDepartmentName { get; set; }
        public int EmployeeDesignationId { get; set; }
        public string? EmployeeDesignationName { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public string? EmployeeLabourGroupName { get; set; }
        public int? LabourHead { get; set; }
        public string? LabourHeadName { get; set; }
      
        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }
        public DateTime? time_in { get; set; }
        public DateTime? time_out { get; set; }
        public decimal? relaxation { get; set; }
        public int IsSelect { get; set; }
    }
}
