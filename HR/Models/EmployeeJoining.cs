using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeJoining
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PhotoUpload { get; set; }
        public int? JoiningType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string VisaDesignation { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public decimal SalaryAmount { get; set; }

        public decimal OpeningSalary { get; set; }
        public decimal Overtime { get; set; }
        public decimal Loan { get; set; }
        public decimal Advance { get; set; }
        public decimal Emi { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int IsDeleted { get; set; }
        public DateTime? RejoiningDate { get; set; }
        public int SalaryBillType { get; set; }
        public int DurationId { get; set; }
        public string DurationName { get; set; }
        public int? DurationFrom { get; set; }
        public int? DurationTo { get; set; }
        public int? BranchId { get; set; }

        public int CompanyId { get; set; }

        public int? FinancialYearId { get; set; }
        public DateTime? NextAnnualLeave { get; set; }
        public DateTime? MaturityDate { get; set; }
        public int NumberOfAnnualLeaves { get; set; }
        public string BankAccNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public int UserType { get; set; }

        public int UserId { get; set; }

        public string? WorkPermitNo { get; set; }
        public string? PersonalNo { get; set; }
        public string? FabCardNo { get; set; }
        public string? IBAN { get; set; }
        public string? VisaSponsor { get; set; }

        public int DepartmentId { get; set; }


        public string DepartmentName { get; set; }

        public int AnnualLeaveCarryForward { get; set; }


        public string AnnualLeaveType { get; set; }

        public DateTime OldJoiningDate { get; set; }

        public decimal PenaltyAmount { get; set; }


        public List<EmployeeJoiningDocumentAndAlert> EmployeeJoiningDocumentAndAlert { get; set; }
        public List<EmployeeJoiningBankDetail> EmployeeJoiningBankDetail { get; set; }

        public List<EmployeeJoiningFacilityDetail> EmployeeJoiningFacilityDetail { get; set; }
        public List<EmployeeJoiningExpensePerDay> EmployeeJoiningExpensePerDay { get; set; }

        public List<EmployeeJoiningIssueDetail> EmployeeJoiningIssueDetail { get; set; }

        public List<DurationDetail> DurationDetail { get; set; }


    }


    public class EmployeeJoiningDocumentAndAlert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int EmployeeJoiningId { get; set; }
        public string DocParticular { get; set; }
        public string DocNumber { get; set; }
        public DateTime? DocDateFrom { get; set; }
        public DateTime? DocDateTo { get; set; }
        public DateTime? DocExpiryDate { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }
    }

    public class EmployeeJoiningBankDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int EmployeeJoiningId { get; set; }
        public string AccNumber { get; set; }
        public string Branch { get; set; }
        public string BankName { get; set; }

        public string IFSCCode { get; set; }

        public string Code2  { get; set; }

        public string Code3 { get; set; }
    }

    public class EmployeeJoiningFacilityDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int EmployeeJoiningId { get; set; }
        public string FacParticular { get; set; }
        public decimal? PercentageCalculated { get; set; }
        public string FacCalculationOn { get; set; }
        public decimal FacNumberOfDays { get; set; }
        public decimal FacAmount { get; set; }
        public DateTime? FacDateFrom { get; set; }
        public DateTime? FacDateTo { get; set; }
        public DateTime? FacMaturity { get; set; }
        public int FacAccountheadId { get; set; }
        
    }

    public class EmployeeJoiningExpensePerDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int EmployeeJoiningId { get; set; }
        public string ExpParticular { get; set; }
        public decimal ExpAmount { get; set; }
        public string ExpDescription { get; set; }
    }

    public class EmployeeJoiningIssueDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int EmployeeJoiningId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string QrCode { get; set; }
        public string BarCode { get; set; }
        public int ItemId { get; set; }
        public int AssetMasterId { get; set; }
        public int AssetQuantityNumber { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

    }


    public class DurationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int DetailId { get; set; }
        public int DurationId { get; set; }
        public DateTime? DurationFromDate { get; set; }
        public DateTime? DurationToDate { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }

    }



}
