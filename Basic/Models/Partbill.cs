using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    public class Partbill
    {
        public int Id { get; set; }
        public DateTime BillDate { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Billtype { get; set; }
        public string Taxtype { get; set; }
        public string ProjectName { get; set; }
        public string UnitName { get; set; }

        public string BillNo { get; set; }
        public string BlockName { get; set; }
        public string FloorName { get; set; }
        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal RetentionPercent { get; set; }
        public decimal RetentionAmount { get; set; }
        public decimal LDPercent { get; set; }
        public decimal LDAmount { get; set; }
        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal KFCPercent { get; set; }
        public decimal KFCAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal AdvanceInterestPer { get; set; }
        public decimal AdvanceInterest { get; set; }
        public decimal AdvanceTds { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal GrossAmount { get; set; }

        public string Taxarea { get; set; }
        public string Remarks { get; set; }
        public string ShippingDetails { get; set; }
    }
}