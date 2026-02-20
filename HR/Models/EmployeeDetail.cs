using System;

namespace BuildExeHR.Models
{
    public class EmployeeDetail
    {
		public int Id { get; set; }
		public string EmployeeCode { get; set; }
		public int EmployeeCategoryId { get; set; }
		public int EmployeeDepartmentId { get; set; }
		public int EmployeeDesignationId { get; set; }
		public int EmployeeLabourGroupId { get; set; }
		public int LabourHead { get; set; }
		public int EmployeeSalaryTypeId { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string Sex { get; set; }
		public DateTime? DateOfJoining { get; set; }
		public DateTime? RetirementDate { get; set; }
		public string PhoneNo { get; set; }
		public string EmailId { get; set; }
		public decimal SalaryAmount { get; set; }
		public string JobStatus { get; set; }
		public Int16 FinancialYearId { get; set; }
		public int? LeaveSalary { get; set; }
		public int EmployeeId { get; set; }
		public DateTime? Timein { get; set; }
		public DateTime? Timeout { get; set; }
		public DateTime? LoginTime { get; set; }
		public DateTime? LogoutTime { get; set; }
		public decimal OverTime { get; set; }
		public decimal Amount { get; set; }
		public decimal OverTimeAmount { get; set; }
		public int UnitId { get; set; }
		public string UnitName { get; set; }
		public decimal Work { get; set; }
		public string WorkShortName { get; set; }
		public string ProjectName { get; set; }
		public DateTime? ToDate { get; set; }
		public decimal OvertimeRate { get; set; }	
		public DateTime? WageToDate { get; set; }
	}
}
