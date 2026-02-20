using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class TendorSubmitted
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public DateTime?  DateEntered { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public decimal  TenderAmount { get; set; }
    
     
        public decimal? TenderFee { get; set; }
        public string? FeeType { get; set; }
        public int? FeeId { get; set; }
        public string? FeeChequeNo  { get; set; }
       public DateTime? FeeChequeDate { get; set; }


        public decimal? Othercharge { get; set; }
        public string? OtherchargeType { get; set; }
        public string? OtherchargeChequeNo { get; set; }
        public int? OtherchargeId { get; set; }
        public DateTime? OtherchargeChequeDate { get; set; }


        public decimal? EmdAmount { get; set; }
        public string? EmdType { get; set; }
        public int? EmdTypeId { get; set; }
        public string? EmdChequeNo { get; set; }
        public DateTime? EmdChequeDate { get; set; }

        public string? TransactionNO { get; set; }
        public string? Narration { get; set; }
        public string? EmdStatus { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public string? StatusDescription { get; set; }

        public int? FinancialYearId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? EmdVouNo { get; set; }
        public int? EmdVouTypeId { get; set; }
        public int? TenderFeeVouNo { get; set; }
        public int? TenderFeeVouTypeId { get; set; }
        public int? OtherVouNo { get; set; }
        public int? OtherVouTypeId { get; set; }

    }
}
