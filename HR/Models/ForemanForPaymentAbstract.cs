using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class ForemanForPaymentAbstract
    {
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string? ProjectName { get; set; }
        public string? DivisionName { get; set; }

        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public decimal TotalWage { get; set; }
        public decimal TotOtAmount { get; set; }
        public decimal AdvancePaid { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Payment { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? AdvanceBalance { get; set; }
        public int IsAdvanceByProject { get; set; }

        public string BillRemarks { get; set; }
    }
}
