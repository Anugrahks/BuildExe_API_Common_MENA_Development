using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class EmployeeJoiningModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string PhotoUpload { get; set; }
        public int JoiningType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string VisaDesignation { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal Overtime { get; set; }
        public decimal Loan { get; set; }
        public decimal Advance { get; set; }
        public decimal Emi { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int IsDeleted { get; set; }
        public DateTime? RejoiningDate { get; set; }
        public int SalaryBillType { get; set; }
        public string DurationName { get; set; }
        public int DurationFrom { get; set; }
        public int DurationTo { get; set; }
        public int DurationId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime? NextAnnualLeave { get; set; }
        public DateTime? MaturityDate { get; set; }
        public int NumberOfAnnualLeaves { get; set; }
        public string BankAccNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public decimal OpeningSalary { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public int MaxLevel { get; set; }
        public int ViewType { get; set; }
        public string JoiningTypeName { get; set; }

    }

    
}


