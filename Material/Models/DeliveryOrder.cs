using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


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

        public int CurrencyId { get; set; }
        public decimal ExchangeRate { get; set; }

        public int? CustomerId { get; set; }//lakshmi//
        public string? ClientUniqueName { get; set; }


        public List<int> selectedPurchaseOrders { get; set; }

        public bool IsWareHouse { get; set; } = false;

        // Optional navigation properties if needed
        [JsonProperty("deliveryOrderDetails")]
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

        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        [JsonProperty("materialBrandId")]
        public int MaterialBrandId { get; set; } = 0;
        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }
        [JsonProperty("warrantyDetails")]
        public List<WarrantyDetails> WarrantyDetails { get; set; } = new();

        [JsonProperty("combineDetails")]
        public List<combineDetails> CombineDetails { get; set; } = new();

        [JsonProperty("iscombined")]
        public int IsCombined { get; set; }


        [JsonProperty("isnewone")]
        public int IsNewone { get; set; }

        [JsonProperty("isselect")]
        public int IsSelect { get; set; }

        public int purchaseOrderId { get; set; }


        public int purchaseOrderIdDetails { get; set; }









    }

    public class combineDetails

    {
        public int? itemId { get; set;  }
        public string? itemName { get; set; }

        public decimal? requiredQuantity { get; set; }

        public decimal? rate { get; set; }

        public string? purchaseOrderId { get; set; }
    }



    public class WarrantyDetails
    {
        [JsonProperty("id")]
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonProperty("slNo")]
        [System.Text.Json.Serialization.JsonPropertyName("slNo")]
        public int SlNo { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }


        [JsonProperty("warrantyDate")]
        [System.Text.Json.Serialization.JsonPropertyName("warrantyDate")]  
        public string WarrantyDate { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public int ProjectId { get; set; }
        public int MaterialId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime EnteredOn { get; set; }
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

        [JsonProperty("iscombined")]
        public int? IsCombined { get; set; }

        [JsonProperty("isnewone")]
        public int? IsNewone { get; set; }

        [JsonProperty("isselect")]
        public int IsSelect { get; set; }

        [JsonProperty("combineDetails")]
        public List<combineDetails> CombineDetails { get; set; } = new();
        public decimal ReceivedQuantity { get; set; }

    }

}
