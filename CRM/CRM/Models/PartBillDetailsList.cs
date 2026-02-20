using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class PartBillDetailsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PartBillDetailsId { get; set; }
        public int PartBillMasterId { get; set; }
        public int ScheduleNo { get; set; }
        public int SpecId { get; set; }
        public string SpecNumber { get; set; }
        public string SpecName { get; set; }
        public string SacCode { get; set; }
        public int Unit { get; set; }
        public string UnitShortName { get; set; }
        public decimal PartRatePerUnit { get; set; }
        public decimal ScheduledQty { get; set; }
        public decimal PreviousQty { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Description { get; set; }
        public int? WeeklyBillId { get; set; }
        public int MultiplyOrDivision { get; set; }

        public decimal BeforeConversionQuantity { get; set; }

        public decimal ConversionValue { get; set; }

    }
}
