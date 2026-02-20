using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class AdditionalBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? BillNumber { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int DivisionId { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }

        public string  WorkDescription { get; set; }
        public string Taxarea { get; set; }
        public string TaxType { get; set; }

        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal? RetentionPercent { get; set; }
        public decimal? RetentionAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }

        public decimal KFCPer { get; set; }
        public decimal KFCAmount { get; set; }

        public decimal DiscPer { get; set; }
        public decimal DiscAmount { get; set; }

        // public decimal IGSTPercent { get; set; }
        // public decimal SGSTPer { get; set; }

        // public decimal CGSTPer { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public Int16 IsDeleted { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? AdvanceTds { get; set; }
        public decimal? AdvanceInterestPer { get; set; }
        public decimal? AdvanceInterest { get; set; }
        public decimal? RoundOff { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TdsOnGst { get; set; }
        public decimal? TdsOnGstPer { get; set; }
        public string EWayBillNumber { get; set; }
        public string BuyersOrderNumber { get; set; }

        public int WorkCategoryId { get; set; }


        public string? DiscountType { get; set; }

        public string? clientUniqueName { get; set; }

        public List<AdditionalBillDetails> AdditionalBillDetails { get; set; }
        public List<AdditionalBillRetentiondetail> AdditionalBillRetentiondetail { get; set; }
    }
    public class AdditionalBillDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdditionalBillDetailsId { get; set; }
        public int AdditionalBillId { get; set; }
         public string  ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxPer { get; set; }
        public string Saccode { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? Amount { get; set; }
        public int ItemUnit { get; set; }

        public string ItemName { get; set; }

        public decimal IndividualDis  { get; set; }

        public decimal IndividualDisPer { get; set; }



    }

    public class AdditionalBillRetentiondetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AdditionalBillId { get; set; }
        public decimal? RetentionPer { get; set; }
        public decimal? RetentionAmt { get; set; }
        public string? Label { get; set; }
        
    }
}
