using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class SitemanagerList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }

        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int DebitHeadId { get; set; }
        public int CreditHeadId { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }

        public int TransferedEmployeeId { get; set; }
        public string Narration { get; set; }
        public string Form { get; set; }
        public int Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 WithClear { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? paymentDate { get; set; }
        public string? ProjectName { get; set; }
        public string? DivisionShortName { get; set; }

        public string? UnitName { get; set; }
        public string? BlockName { get; set; }
        public string? FloorName { get; set; }
        public string FullName { get; set; }

        public string TransferedFullName { get; set; }
        public string? DebitHeadName { get; set; }
        public string? CreditHeadName { get; set; }
        public int Maxlevel { get; set; }
        public int? WorkNameId { get; set; }

        public string? TaxArea { get; set; }

        public decimal? GSTper { get; set; }
        public decimal? SGST { get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? RoundOff { get; set; }
        public int? BatchID { get; set; }
        public string? BatchName { get; set; }


    }
}
