using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeMaterialServices.Models
{
    public class MaterialTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TransferDate { get; set; }
        public int TransferMasterId { get; set;  }
        public int ProjectIdFrom { get; set; }
        public int UnitIdFrom { get; set; }
        public int BlockIdFrom { get; set; }
        public int FloorIdFrom { get; set; }
        public int DivisionIdFrom { get; set; }
        public int ProjectIdTo { get; set; }
        public int UnitIdTo { get; set; }
        public int BlockIdTo { get; set; }
        public int FloorIdTo { get; set; }
        public int DivisionIdTo { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 multicompany { get; set; }
        public Int16 ToCompany { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 TransferStatusId { get; set; }

        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal MiscellaneousExpense { get; set; }
        public decimal MiscellaneousExpensePer { get; set; }

        public decimal NetAmount { get; set; }
        public int? Category { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16  IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public decimal? RoundOff { get; set; }
        public string? taxarea { get; set; }
        public string? Remarks { get; set; }

        public int UserId { get; set; }

        public int SubcontractorId { get; set; }
        public int IsAsset { get; set; }

        public List<TransferDetail> TransferDetail { get; set; }
    }
    public class TransferDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferDetailId { get; set; }
        public int MaterialTransferId { get; set; }
        public int MaterialId { get; set; }
        public int? IndentId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }
        public decimal? Discount { get; set; }
        public decimal Total { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }
        public string ConversionUnitName { get; set; }

        public List<TransferItemDetails> TransferItemDetails { get; set; }
    }


    public class TransferItemDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TransferItemDetailsId { get; set; }
        public int TransferDetailId { get; set; }

        public int MaterialTransferId { get; set; }

        public int AssetMasterId { get; set; }
        public string? ItemCode { get; set; }
       
    }

}
