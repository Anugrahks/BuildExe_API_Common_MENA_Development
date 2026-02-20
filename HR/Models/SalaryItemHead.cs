using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    public class SalaryItemHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountHeadid { get; set; }
        public Int16 SalaryHeadTypeId { get; set; }
        public string HeadName { get; set; }
        public string CalculateOn { get; set; }
        public int Active { get; set; }
        public string? Remarks { get; set; }
        public string CalculationMode { get; set; }
        public Int16 VaryingHead { get; set; }
        public decimal? UpperLimit { get; set; }
        public Int16 DeductLeave { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }

        public decimal? EmployerContribution { get; set; }

        public string EmployerContributionType { get; set; }
        public Int16 UserId { get; set; }


        public int DailyHead { get; set; }

        public int IsFacility { get; set; }


        public int IsAccount { get; set; }

        public List<SalaryItemBenefitDetails> SalaryItemBenefitDetails { get; set; }

    }


    public class SalaryItemBenefitDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int SalaryItemHeadId { get; set; }
        public int BenefitId { get; set; }
        public decimal MultiplicationIndex { get; set; }
        public string BenefitName { get; set; }


    }
}
