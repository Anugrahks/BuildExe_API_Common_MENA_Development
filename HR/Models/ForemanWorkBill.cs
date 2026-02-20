using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class ForemanWorkBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WorkOrderMasterId { get; set; }
        public DateTime   BillDate { get; set; }
        public string BillNumber { get; set; }
        public int ForemanId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
       
        public decimal  Amount { get; set; }
        public decimal AmountPaidAdvance { get; set; }
        public decimal BalanceAmount { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public string? Remarks { get; set; }

        public Int16 UserId { get; set; }

        public List<ForemanWorkBillDetails> ForemanWorkBillDetails { get; set; }
        public int FinalBill { get; set; }
        
    }
    public class ForemanWorkBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ForemanWorkBillDetailsId { get; set; }
        public int ForemanWorkBillId { get; set; }
        public int LabourWorkId { get; set; }
        public decimal NoOfLabours { get; set; }
        public decimal Wage { get; set; }
        public decimal OTRate { get; set; }
        public decimal OTHours { get; set; }
    }
}
