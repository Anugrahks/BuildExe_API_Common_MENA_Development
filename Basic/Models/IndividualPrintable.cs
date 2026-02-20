using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class IndividualPrintable
    {
        [Required]
        public int MenuId { get; set; }
        public DateTime JournalDate { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int EnquiryId { get; set; }
        public int DebitHeadId { get; set; }
        public int CreditHeadId { get; set; }
        public decimal Amount { get; set; }

        public int? BillId { get; set; }

        public string? BillName { get; set; }

        public int? Type { get; set; }
        public int? JournalType { get; set; }
        public int? EmployeeCategoryId { get; set; }

        public decimal? GSTPercentage { get; set; }
        public int? TaxAreaId { get; set; }
        // [NotMapped]
        public decimal? GSTAmount { get; set; }
        // [NotMapped]
        public int? AccountTypeId { get; set; }
        public string Remarks { get; set; }
        public int WithClear { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string? ChequeNo { get; set; }
    }
}
