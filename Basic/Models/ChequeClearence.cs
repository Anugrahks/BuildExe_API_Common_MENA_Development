using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class ChequeClearence
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ChequeDate { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public string FormName { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 ClearenceStatus { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string Party { get; set; }
        public decimal Amount { get; set; }
        public string ChequeType { get; set; }
        public Int16 UserId { get; set; }
        public string Narration { get; set; }
        public decimal BouncingCharge { get; set; }

    }
}
