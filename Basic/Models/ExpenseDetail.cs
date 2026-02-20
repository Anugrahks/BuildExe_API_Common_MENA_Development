using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class ExpenseDetail
    {
        public DateTime ? BillDate { get; set; }
        public string? BillNo { get; set; }
        public string? Type { get; set; }
        public int? headId { get; set; }
        public string? HeadName { get; set; }
    
        public string? Particular { get; set; }
        public decimal? Amount { get; set; }

        public string? WorkCategoryName { get; set; }


        public string? WorkName { get; set; }
        public string? Remarks { get; set; }

    }
}
