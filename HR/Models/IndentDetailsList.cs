using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class IndentDetailsList
    {
        public int IndentDetailsId { get; set; }
        public int IndentId { get; set; }
        
        public int WorkId { get; set; }
        public string labourWorkName { get; set; }
        public int  UnitId { get; set; }
        public string  UnitShortName { get; set; }
        public decimal QuantityRequired { get; set; }
        public decimal WorkRate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public decimal QuantityOrdered { get; set; }
        public int PurchaseFlag { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }
        public string WorkCategoryName { get; set; }
        public string WorkShortName { get; set; }


    }
}
