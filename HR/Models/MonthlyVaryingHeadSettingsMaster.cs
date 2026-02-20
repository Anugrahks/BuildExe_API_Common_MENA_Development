using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    public class MonthlyVaryingHeadSettingsMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int UserId { get; set; }
        public string? BenefitsHeadIds { get; set; }
        public string? DeductionHeadIds { get; set; }
        public string? DutiesAndTaxesHeadIds { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int? IsDeleted { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? IsReject { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public DateTime? MFromDate { get; set; }

        public DateTime? MToDate { get; set; }

        public int MDurationId { get; set; }
        public int MEmployeeMasterId { get; set; }
        public List<MonthlyVaryingHeadSettingsDetails> MonthlyVaryingHeadSettingsDetails { get; set; }


    }
    [Keyless]
    [NotMapped]
    public class MonthlyVaryingHeadSettingsDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailsId { get; set; }
        public int? MasterId { get; set; }
        public int? EmployeeId { get; set; }
        public string? Benefits { get; set; }
        public string? Deductions { get; set; }
        public string? DutiesAndTaxes { get; set; }

        public decimal TAAmountMonthly { get; set; }
    }
}
