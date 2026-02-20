using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class EmployeeMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime?  DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }

        public string PanNumber { get; set; }
        public string AdharNumber { get; set; }
        public string PfNo { get; set; }
        public string EsiNo { get; set; }
        public string Bank { get; set; }
        public string branch { get; set; }
        public string Accno { get; set; }
        public string Ifsc { get; set; }

        public decimal openigamt { get; set; }
        public string? SalaryScale { get; set; }
        public string JobStatus { get; set; }
        
        public string Status { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }

        public string Nationality { get; set; }
        public string? PassportNo { get; set; }
        public DateTime? PassDateOfIssue { get; set; }
        public DateTime? PassDateofExpiry { get; set; }
        public string? VisaNo { get; set; }
        public string? VisaDesignation { get; set; }
        public decimal? VisaAmount { get; set; }
        public DateTime? VisaIssueDate { get; set; }
        public DateTime? VisaExpiryDate { get; set; }
        public decimal? TicketCharge { get; set; }
        public decimal? Penaltyforleave { get; set; }
        public string GratuityStatus { get; set; }
        public int? LeaveSalary { get; set; }
        public decimal Allowance { get; set; }
        public  Int16  UserId { get; set; }

        public string? LabourCardNo { get; set; }
        public string? OccupationCode { get; set; }
        public string? WhatsappNo { get; set; }
        public string? AlternateMobileNo { get; set; }
        public string? EmergencyContactNo { get; set; }
        public int EmployeeId { get; set; }
        public decimal? LoanOpening { get; set; }
        public decimal? EmiAmount { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }

        public int UserType { get; set; }
        public string? GstNo { get; set; }

        public string? BloodGroup { get; set; }


    }
}
