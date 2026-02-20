using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ClientAdvanceReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string ProjectName { get; set; }
        public string? UnitName { get; set; }

        public string? BlockName { get; set; }

        public string? FloorName { get; set; }


        public decimal AdvanceAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public string Remarks { get; set; }
        public string PaymentMode { get; set; }
        public string? PaymentModeName { get; set; }
        public string PaymentModeNo { get; set; }
        public int? SlNo { get; set; }
    }
}
