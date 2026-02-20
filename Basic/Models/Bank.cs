using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BankId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IfsCode { get; set; }
        public string Micr_Code { get; set; }
        public decimal CurrentBalance { get; set; }
        public string BalanceType { get; set; }
        public Int16 AccountTypeId { get; set; }
        public string AccountNo { get; set; }
        public string City { get; set; }
        public int FinancialYearId { get; set; }
        public Int16 IsOD { get; set; }
        public decimal MinimumBalance { get; set; }
        public Int16 UserId { get; set; }
        public decimal? ODLimit { get; set; }
        public DateTime? ODDate { get; set; }



    }
}
