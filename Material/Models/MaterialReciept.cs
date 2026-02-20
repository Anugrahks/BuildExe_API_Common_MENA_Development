using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialReciept
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReceiveMasterId { get; set; }
        public int TransferId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ProjectIdTo { get; set; }
        public int UnitIdTo { get; set; }
        public int BlockIdTo { get; set; }
        public int FloorIdTo { get; set; }
        public int DivisionIdTo { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
       // public Int16 multicompany { get; set; }
      //  public Int16 ToCompany { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ReceiveApprovedDate { get; set; }
        public Int16 ReceiveApprovedBy { get; set; }
       

        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal MiscellaneousExpense { get; set; }
        public decimal MiscellaneousExpensePer { get; set; }

        public decimal NetAmount { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public int IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public decimal? RoundOff { get; set; }
        public string? taxarea { get; set; }

        public int UserId { get; set; }
        public string? Remarks { get; set; }

        public List<RecieptDetail> RecieptDetail { get; set; }
    }
    public class RecieptDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecieptDetailId { get; set; }
        public int MaterialRecieptId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }
        public decimal? Discount { get; set; }
        public string? taxarea { get; set; }

        public decimal Total { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }
}
