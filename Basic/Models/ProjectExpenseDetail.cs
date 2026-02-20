using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace BuildExeBasic.Models
{
    [Keyless]
    public class ProjectExpenseDetail
    {

        public int? SlNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string? BillNo { get; set; }
        public string? Type { get; set; }
        public int? HeadId { get; set; }
        public string? HeadName { get; set; }
        public int? WorkId { get; set; }
        public string? WorkName { get; set; }     
        public int? SupplierId { get; set; }
        public int? MaterialId { get; set; }
        public string? SupplierName { get; set; }
        public string? MaterialName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Expense { get; set; }
        public string? Narration { get; set; }
        public int? EmployeeId { get; set; }
        public string? Designation { get; set; }
        public string? Particular { get; set; }
        public int? TypeId { get; set; }
        public int? ProjectId { get; set; }
        public decimal? Wage { get; set; }
        public decimal? OverTimeHrs { get; set; }
        public decimal? OTAmount { get; set; }
        public decimal? OtherCharge { get; set; }
        public int? NoOfLabour { get; set; }

    }
}
