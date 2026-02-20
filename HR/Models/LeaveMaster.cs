using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class LeaveMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public int LeaveType { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public decimal YearlyLeave { get; set; }
        public decimal MonthlyLeave { get; set; }
        public int DesignationId { get; set; }
        public int UserId { get; set; }
        public int SalaryDeductionBasedOn { get; set; }
        public int DeductionType { get; set; }
        public decimal MultiplicationIndex { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int IsCarryForward { get; set; }       
        public int Ismonthlycarryforward { get; set; }
        public List<LeaveMasterBenefitDetails> LeaveMasterBenefitDetails { get; set; }

    }

    public class LeaveSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveId { get; set; }
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }

    public class LeaveSettingsDet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveId { get; set; }
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int? NoOfLeaves { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }


    public class LeaveMasterBenefitDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int LeaveMasterId { get; set; }
        public int BenefitId { get; set; }
        public decimal MultiplicationIndex { get; set; }
        public string BenefitName { get; set; }


    }
}
