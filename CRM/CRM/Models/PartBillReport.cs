using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class PartBillReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
        public string BillNo { get; set; }
        public string ProjectName { get; set; }
        public string? UnitName { get; set; }
        public string? BlockName { get; set; }
        public string? FloorName { get; set; }

        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string Taxarea { get; set; }
        public string TaxType { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal RetentionAmount { get; set; }
        public decimal LDAmount { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public string ShippingDetails { get; set; }
        public string Remarks { get; set; }
        public int? SlNo { get; set; }

    }
}
