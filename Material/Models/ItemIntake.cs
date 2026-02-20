using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeMaterialServices.Models
{
    public class ItemIntake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public string ReferenceNo { get; set; }
        public int? BillNo { get; set; }
        public int? ProjectId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? DivisionId { get; set; }
        public int? UnitId { get; set; }
        public int? SupplierId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ApprovalStatus { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? IsDeleted { get; set; }
        public int? IsRejected { get; set; }
        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int? ApprovalLevel { get; set; }
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
        public string TaxArea { get; set; }
        public string Remarks { get; set; }

        public DateTime? ExpectedReturnDate { get; set; }

        public List<ItemIntakeDetail> ItemIntakeDetail { get; set; }


    }

    public class ItemIntakeDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemDetailId { get; set; }
        public int ItemIntakeId { get; set; }
        public int ItemId { get; set; }
        public int MaterialUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitRent { get; set; }
        public DateTime IntakeDate { get; set; }
        public int IndentId  { get; set; }

    }
}