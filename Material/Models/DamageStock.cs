using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class DamageStock
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime  Entrydate { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public int DivisionId { get; set; }
        public int   MaterialId { get; set; }
        public decimal Stock { get; set; }
        public decimal Rate { get; set; }
        public Int16  FinancialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int UserId { get; set; }
        public string? RejectRemarks { get; set; }
    }
}
