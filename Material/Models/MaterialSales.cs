using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class MaterialSales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string SalesOrderId { get; set; }
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
        public int PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }

        public string? PaymentMode { get; set; }

        public int PaymentModeId { get; set; }

        public string? PaymentNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int WithClear { get; set; }


        public decimal NetAmount { get; set; }

        public decimal TotalGst { get; set; }
        public decimal GrossAmount { get; set; }

        public string Taxarea { get; set; }
        public string ReferenceNo { get; set; }

        public int InvoiceNo { get; set; }

        public List<MaterialSalesDetails> MaterialSalesDetails { get; set; }

    }



    public class MaterialSalesDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialSalesDetailId { get; set; }
        public int MaterialSalesId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? TaxAmt { get; set; }

        public int? StockPoint { get; set; }
        public int SalesOrderDetailId { get; set; }
        
    }
}
