using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class AdditionalBillReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
       
        public string BillNumber { get; set; }
       
        public string ProjectName { get; set; }
      
        public string? UnitName { get; set; }
     
        public string? BlockName { get; set; }
       
        public string? FloorName { get; set; }
      
        public decimal Amount { get; set; }
       
       

        public string WorkDescription { get; set; }
        public string Taxarea { get; set; }
        public string TaxType { get; set; }

        public Int16 ApprovalStatus { get; set; }
        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }

        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public int? SlNo { get; set; }
        //public decimal RetentionPercent { get; set; }
        


    }
}
