using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class ContractorForPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string WorkOrderNo { get; set; }

        public string StageName { get; set; }
        public DateTime DateOrdered { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string? ProjectName { get; set; }
        public string? DivisionShortName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Payment { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }

        public decimal? AdvanceBalance { get; set; }
        public decimal? RoundOff { get; set; }
        public int? IsAdditionalBill { get; set; }
    }
}
