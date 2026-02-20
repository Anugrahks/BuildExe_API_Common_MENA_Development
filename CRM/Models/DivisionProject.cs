using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class DivisionProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public decimal TotalArea { get; set; }
        public decimal RatePerArea { get; set; }
        public decimal TotalAmount { get; set; }

        public int PaymentModeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
        
        public int IsExtended { get; set; }

        public int Status { get; set; }
        public int? IsEdit { get; set; }
    }
}
