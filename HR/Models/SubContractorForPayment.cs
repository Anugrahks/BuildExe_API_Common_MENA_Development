using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class SubContractorForPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Billno { get; set; }
        public string? WorkOrderNo { get; set; }
        public DateTime BillDate { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }


        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Payment { get; set; }
        public decimal BillAmountBalance { get; set; }
       
        public decimal? PaymentAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }

        public decimal? RoundOff { get; set; }

        public int OtherDeduction { get; set; }

        public string OtherDeductionLabel { get; set; }

        public decimal OtherDeductionAmount { get; set; }

        public decimal OtherDeductionPer { get; set; }

        public int IsClicked { get; set; }

        public int IsAdded { get; set; }

        public string BillRemarks { get; set; }

    }
}
