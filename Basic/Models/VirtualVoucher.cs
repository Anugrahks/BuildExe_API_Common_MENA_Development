using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{

    public class VirtualVoucher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime VoucherDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public int ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalLevel { get; set; }

        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public string Description { get; set; }
        public Int16 IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int IsReject { get; set; }

        public List<VirtualVoucherDetails> VirtualVoucherDetails { get; set; }
    }
    public class VirtualVoucherDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VirtualVoucherDetailsId { get; set; }
        public int VirtualVoucherId { get; set; }
        public int DebitHeadId { get; set; }
        public int CreditHeadId { get; set; }
        public decimal Amount { get; set; }
        public int? AccountTypeId { get; set; }
        public string Remarks { get; set; }
   

    }
}
