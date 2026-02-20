using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    public class SalaryBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string? BenefitHeadIds { get; set; }
        public string? MonthlyBenefitHeadIds { get; set; }
        public string? DeductionHeadIds { get; set; }
        public string? MonthlyDeductionHeadIds { get; set; }
        public string? DutiesandTaxesIds { get; set; }
        public string? MonthlyDutiesandTaxesIds { get; set; }
        public string? LatePenaltyHeadIds { get; set; }
        public string? LeaveDeductionHeadIds { get; set; }
        public string? LeavePenaltyHeadIds { get; set; }
        public string? OverTimeHeadId { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public int ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy  {get;set;}
        public int ApprovalLevel { get; set; }
        public int IsDeleted { get; set; }
        public int IsReject { get; set;}
        public string? Remarks { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        public int DurationId { get; set; }
        public int EmployeeMasterId { get; set; }


        public string MultiEmployeeId { get; set; }

        public List<SalaryBillDetails> SalaryBillDetails { get; set; }

    }




    public class SalaryBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaryBillDetailsId { get; set; }
        public int? SalaryBillId { get; set; }
        public int? EmployeeId { get; set; }
        public string? Benefits { get; set; }
        public string? MonthlyBenefits { get; set; }
        public string? Deductions { get; set; }
        public string? MonthlyDeductions { get; set; }
        public string? DutiesAndTaxes { get; set; }
        public string? MonthlyDutiesAndTaxes { get; set; }
        public string? LatePenalty { get; set; }
        public string? LeaveDeduction { get; set; }
        public string? LeavePenalty { get; set; }
        public string? OverTime { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal NetSalary { get; set; }

    }
}
