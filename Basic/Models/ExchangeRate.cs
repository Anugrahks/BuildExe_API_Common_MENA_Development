using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class ExchangeRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BaseCurrencyId { get; set; }
        public int TargetCurrencyId { get; set; }
        public decimal Exchange_Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Source { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int IsDeleted { get; set; }
        public List<ExchangeRateDetail> ExchangeRateDetail { get; set; }
    }
    public class ExchangeRateDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TargetCurrencyId { get; set; }
        public decimal Exchange_Rate { get; set; }

    }
}
