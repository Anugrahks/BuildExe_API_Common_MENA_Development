using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeMaterialServices.Models
{
    public class DeliveryOrderMaster
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? FloorId { get; set; }
        public string FloorName { get; set; }
        public DateTime? DateOrdered { get; set; }
        public int? OrderId { get; set; }
        public string ReferenceNo { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Contactperson { get; set; }
        public string ContactNo { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int ApprovalLevel { get; set; } = 0;
        public int? ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int? WorkCategoryId { get; set; }
        public string WorkCategoryName { get; set; }
        public int? WorkNameId { get; set; }
        public string WorkName { get; set; }
        public int UserId { get; set; } = 0;
        public string VehicleNo { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string RejectRemarks { get; set; }
        public int IsCompleted { get; set; } = 0;
        public int MaterialTypeId { get; set; } = 0;

        public int IsAsset { get; set; }


        // Optional navigation properties if needed
        public List<DeliveryOrderDetails> DeliveryOrderDetails { get; set; }
        public List<DeliveryOrderSubDetails> DeliveryOrderSubDetails { get; set; }

    }


    public class DeliveryOrderDetails
    {
        public int DeliveryOrderDetailId { get; set; }
        public int? DeliveryOrderId { get; set; }
        public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public decimal? QuantityPurchased { get; set; }
        public decimal? UnitRate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public string Remarks { get; set; }
        public int MaterialBrandId { get; set; }
        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
    }

    public class DeliveryOrderSubDetails
    {
        public int DeliveryOrderSubDetailId { get; set; }
        public int? DeliveryOrderId { get; set; }
        public int? ItemId { get; set; }
        public string ItemName { get; set; }

        public int? PurchaseOrderId { get; set; }
        public int? PurchaseOrderIdDetails { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitRate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public string Remarks { get; set; }
        public string PartNumber { get; set; }
        public DateTime WarrantyDate { get; set; }

    }

}