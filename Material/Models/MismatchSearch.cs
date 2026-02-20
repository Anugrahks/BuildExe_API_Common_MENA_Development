using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class MismatchSearch
    {
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ProjectIdFrom { get; set; }
        public int? BlockIdFrom { get; set; }
        public int? FloorIdFrom { get; set; }
        public int? UnitIdFrom { get; set; }
        public int? ProjectIdTo { get; set; }
        public int? BlockIdTo { get; set; }
        public int? FloorIdTo { get; set; }
        public int? UnitIdTo { get; set; }
        public DateTime? ReceiveFromDate { get; set; }
        public DateTime? ReceiveToDate { get; set; }
        public DateTime? TransferFromDate { get; set; }
        public DateTime? TransferToDate { get; set; }
        public int? ReportId { get; set; }

    }
}
