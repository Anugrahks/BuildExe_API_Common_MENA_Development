using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class Voucher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime  VoucherDate { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public string  FormName { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int SupplierId { get; set; }
        public int UserId { get; set; }
        public int IsDeleted { get; set; }

        public int VoucherDetailsId { get; set; }
        public int AccountHeadId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public string Narration { get; set; }
    }
}
