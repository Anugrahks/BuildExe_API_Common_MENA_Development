using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class SupplierAdvanceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentBy { get; set; }
        public string PaymentNo { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal AdvanceRecoveryBalance { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public string Narration { get; set; }
        public Int16 WithClear { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }
        public int Maxlevel { get; set; }
        public DateTime? chequeDate { get; set; }
        public int SiteManagerId { get; set; }
        public int? SlNo { get; set; }

        public int VouNumber { get; set; }



        public int VouTypeId { get; set; }

        public int VouMasterId { get; set; }


        public decimal NewAmount { get; set; }
    }
}
