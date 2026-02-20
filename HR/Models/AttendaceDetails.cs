using BuildExeHR.Models;
using Microsoft.VisualBasic;
using System;

namespace BuildExeHR.Models
{
    public class AttendaceDetails
    {
        public int Id { get; set; }
        public int AttendanceDetailId { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public int EmployeeDesignationId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int EmployeeSalaryTypeId { get; set; }
        public string FullName { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public int ProjectId { get; set; }
        public decimal OverTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Work { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal OverTimeRate { get; set; }
        public string ProjectName { get; set; }
        public decimal SalaryAmount { get; set; }
        public DateTime? ToDate { get; set; }
        public string WorkShortName { get; set; }
        public string WorkCategoryName { get; set; }

        public DateTime? DateWorked { get; set; }

    }
}

