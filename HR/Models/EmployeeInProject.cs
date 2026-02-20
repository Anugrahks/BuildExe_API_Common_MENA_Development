using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    [Keyless]
    public class EmployeeInProject
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateAssignedEmployee { get; set; }
        public string FullName { get; set; }
        public int EmployeeDesignationId { get; set; }
        public string EmployeeDesignationName { get; set; }
        public int? LabourHead { get; set; }
        public string? LabourHeadName { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }
        public int isselect { get; set; }

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
