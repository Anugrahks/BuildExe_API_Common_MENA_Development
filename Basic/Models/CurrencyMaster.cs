using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class CurrencyMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyShortName { get; set; }
        public string CurrencyUnitName { get; set; }
        public int CurrencyPrecision { get; set; }
        public string Symbol { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int IsDeleted { get; set; }
        public int IsCompanyCurrency { get; set; }
    }
}
