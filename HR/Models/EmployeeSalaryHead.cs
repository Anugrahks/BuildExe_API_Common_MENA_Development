using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeSalaryHead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryItemHeadId { get; set; }
        public DateTime  EffectiveFrom { get; set; }
        public string  Active { get; set; }
        public decimal  Rate { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }

        public decimal EmployerContributionPer { get; set; }
        public decimal EmployerContributionAmount { get; set; }
    }
}
