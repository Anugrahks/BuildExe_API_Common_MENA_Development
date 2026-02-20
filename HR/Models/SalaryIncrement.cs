using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class SalaryIncrement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeId { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public string? Remarks { get; set; }
        public decimal? PercentageofIncrement { get; set; }
        public decimal? IncrementAmount { get; set; }
        public decimal? HikeAmount { get; set; }
        public decimal? OTIncrement { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int UserId { get; set; }

    }

}
