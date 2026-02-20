using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
 
    public class ProjectBookingCancellation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public DateTime CancellationDate { get; set; }
        public decimal BookingAmount { get; set; }
        public decimal Deduction { get; set; }
        public decimal RefundAmount { get; set; }

        public DateTime? PaymentDate { get; set; }
        public String? PaymentMode { get; set; }
        public int? BankId { get; set; }
        public string? RefNO { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

    }
}
