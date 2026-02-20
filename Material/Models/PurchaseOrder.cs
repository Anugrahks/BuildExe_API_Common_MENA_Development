using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class PurchaseOrder
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public DateTime DateOrdered { get; set; }
        public int orderId { get; set; }
        public string OrderNo { get; set; }
        public Int16 OrderTypeId { get; set; }
        public int OrderCategoryId { get; set; }
        public Int16 PurchaseFlag { get; set; }
        public int SupplierPreffered { get; set; }
        public string? Contactperson { get; set; }
        public string? ContactNo { get; set; }
        public string? BillingAddress { get; set; }
        public string? DeliveryAddress { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedStatus { get; set; }
        public Int16 ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public string? RejectRemarks { get; set; }
        public int UserId { get; set; }

        public string VehicleNo   { get; set; }
        public int DivisionId { get; set; }

        public int IsAsset { get; set; }

        public string? SupplierQuoteRef { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal ExtraDiscount { get; set; }

        public decimal ExtraGSTPer { get; set; }

        public decimal ExtraGSTAmount { get; set; }
        public decimal NetAmount { get; set; }


        public string? DeliveryLocation { get; set; }

        public decimal ExtraDiscountPer { get; set; }





        public List<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        


    }

    public class PurchaseOrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int IndentId { get; set; }

        public int MaterialBrandId { get; set; }
        public int ItemId { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal QuantityPurchased { get; set; }
        public decimal ItemRate { get; set; }

        public decimal Disount { get; set; }
        public decimal Tax { get; set; }
        public string Remarks { get; set; }


        public string MaterialRemarks { get; set; }
        public int? MaterialCategoryId { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }

        public string ConversionUnitName { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
