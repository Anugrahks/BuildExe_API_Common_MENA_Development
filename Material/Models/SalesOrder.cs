using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class SalesOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateOrdered { get; set; }
        public int OrderNo { get; set; }
        public int CustomerId { get; set; }
        public string Contactperson { get; set; }
        public string ContactNo { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; } 
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsRejected { get; set; }
        public int UserId { get; set; }
        public decimal? BillDiscountPer { get; set; } 
        public decimal? BillDiscount { get; set; }
        public decimal? RoundOff { get; set; }
        public int MaterialTypeId { get; set; }
        public string Remarks { get; set; }

        public string ReferenceNo { get; set; }

        public List<SalesOrderDetails> SalesOrderDetails { get; set; }

    }



    public class SalesOrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int MaterialId { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal ItemRate { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal? DiscountAmt { get; set; } 
        public decimal? TaxPer { get; set; } 
        public decimal? TaxAmt { get; set; }
        public int StockPoint { get; set; }


    }
}
