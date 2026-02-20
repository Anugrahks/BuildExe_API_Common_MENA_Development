
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BuildExeMaterialServices.Models
{
    public class ItemReturnTransfer
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime RTDate { get; set; }
        public int TypeId { get; set; }
        public int ProjectIdFrom { get; set; }
        public int BlockIdFrom { get; set; }
        public int FloorIdFrom { get; set; }
        public int DivisionIdFrom { get; set; }
        public int UnitIdFrom { get; set; }
        public int SupplierId { get; set; }
        public int ProjectIdTo { get; set; }
        public int BlockIdTo { get; set; }
        public int FloorIdTo { get; set; }
        public int DivisionIdTo { get; set; }
        public int UnitIdTo { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }
        public int BillNo { get; set; }
        public string RefNo { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalStatus { get; set; }
        public string ApprovalRemarks { get; set; }

        public string RejectRemarks { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public int IsRejected { get; set; }
        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal GSTPer { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal TcsPer { get; set; }
        public decimal TcsAmount { get; set; }
        public decimal BillDiscountPer { get; set; }
        public decimal BillDiscount { get; set; }
        public int IsPercentage { get; set; }
        public int IsGST { get; set; }
        public int IsTransportation { get; set; }
        public int IsLoadingUnloading { get; set; }
        public int IsOtherCharge { get; set; }
        public int IsAmount { get; set; }
        public string Remarks { get; set; }

        public string OtherTaxArea { get; set; }
        public decimal TotalDisc { get; set; }
        public decimal TotalGST { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal GrossAmt { get; set; }
        public List<ReturnTransferDetails> ReturnTransferDetails { get; set; }

        public List<ItemTransferExtraCharges> ItemTransferExtraCharges { get; set; }
        


    }
    public class ReturnTransferDetails

    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReturnTransferDetailId { get; set; }
        public int ReturnTransferMasterId { get; set; }
        public int ItemId { get; set; }
        public int ItemIntakeId { get; set; }
        public decimal UnitRent { get; set; }
        public int ItemSupplier { get; set; }
        public DateTime IntakeDate { get; set; }
        public decimal IntakeQuantity { get; set; }
        public decimal BalanceQuantity { get; set; }
        public decimal InputtedQuantity { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalDays { get; set; }
        public decimal TotalMonth { get; set; }
        public decimal Total { get; set; }
        public decimal GstPercent { get; set; }
        public decimal GstAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string TaxArea { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal DiscountPer { get; set; }
        public DateTime ReturnTranferDate { get; set; }
        public int IsOpening { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }


    }

    public class ItemTransferExtraCharges

    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ItemReturnTransferId { get; set; }
        public int SupplierId { get; set; }
        public decimal Transportationchild { get; set; }
        public decimal LoadingUnloadingchild { get; set; }
        public decimal OtherChargeschild { get; set; }
        public decimal Discountchild { get; set; }
        public decimal TCSchild { get; set; }
        public decimal gSTchild { get; set; }

        public decimal sgst { get; set; }
        public decimal cgst { get; set; }
        public decimal igst { get; set; }
        public decimal NetAmt { get; set; }

    }

}
