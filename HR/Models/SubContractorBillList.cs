using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class SubContractorBillList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string Billno { get; set; }
        public int WorkOrderId { get; set; }
        public string WorkName { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string ProjectName { get; set; }
        public string DivisionShortName { get; set; }

        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int SubContractorId { get; set; }
        public string? FullName { get; set; }
        public Int16 FinancialYearId { get; set; }
        public DateTime BillDateFrom { get; set; }
        public DateTime BillDateTo { get; set; }
        public decimal AmountAsPerAttendance { get; set; }
        public decimal AmountAsPerBill { get; set; }
        public decimal AmountAsPerBillBalance { get; set; }
        public decimal AmountPaidAdvance { get; set; }
        public decimal AdditionalAmount { get; set; }
        public decimal AmountRecomendable { get; set; }
        public decimal AmountRetensionPercent { get; set; }
        public decimal AmountRetensionAmount { get; set; }
        public decimal AmountTdsPercent { get; set; }
        public decimal AmountTdsAmount { get; set; }
        public decimal? AdvanceTds { get; set; }
        public Int16 TdsStatus { get; set; }
        public Int16 RetentionStatus { get; set; }
        public decimal RetentionBalanceAmount { get; set; }
        public decimal tax { get; set; }
        public decimal taxamount { get; set; }
        public string taxtype { get; set; }
        public decimal Othercharge { get; set; }
        public decimal RoundOff { get; set; }
        public string Remarks { get; set; }
        public int Category { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime DateApproved { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int finalBill { get; set; }
        public int Maxlevel { get; set; }
        public int? ViewType { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? PurchaseAdjusted { get; set; }
        public decimal? AdditionalIGST { get; set; }
        public decimal? AdditionalCGST { get; set; }
        public decimal? AdditionalSGST { get; set; }

        public decimal? PreviousBalance { get; set; }
        public decimal? TotalBalance { get; set; }
        public decimal RefundableDeductions { get; set; }
        public decimal DiscountReceived { get; set; }

    }
}
