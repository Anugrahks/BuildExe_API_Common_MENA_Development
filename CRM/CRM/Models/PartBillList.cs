using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class PartBillList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int BillType { get; set; }
        public int TaxTypeId { get; set; }
        public string? BillNo { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 FinalBill { get; set; }

        public string Taxarea { get; set; }
        public string TaxType { get; set; }

        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal RetentionPercent { get; set; }
        public decimal RetentionAmount { get; set; }
        public decimal LDPercent { get; set; }
        public decimal LDAmount { get; set; }
        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmount { get; set; }

        public decimal SGSTPer { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal KFCPercent { get; set; }
        public decimal KFCAmount { get; set; }

        public string ShippingDetails { get; set; }
        public string Remarks { get; set; }
        public decimal? RoundOff { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int Maxlevel { get; set; }
        public decimal? AdvanceInterestPer { get; set; }
        public decimal? AdvanceInterest { get; set; }
        public decimal? AdvanceTds { get; set; }
        public int? ViewType { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TdsOnGst { get; set; }
        public decimal? TdsOnGstPer { get; set; }
        public int? DivisionId { get; set; }
        public string EWayBillNumber { get; set; }
        public string BuyersOrderNumber { get; set; }

        public int WorkCategoryId { get; set; }

        public string? clientUniqueName { get; set; }

    }
}
