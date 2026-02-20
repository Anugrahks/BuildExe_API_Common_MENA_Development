using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class PurchaseOrderList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public DateTime DateOrdered { get; set; }
        public int orderId { get; set; }
        public string  OrderNo { get; set; }
        public Int16 OrderTypeId { get; set; }
        public int OrderCategoryId { get; set; }
        public Int16 PurchaseFlag { get; set; }
        public int SupplierPreffered { get; set; }
        public string? SupplierName { get; set; }
        public string? Contactperson { get; set; }
        public string? ContactNo { get; set; }
        public string? BillingAddress { get; set; }
        public string? DeliveryAddress { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public Int16 ApprovedStatus { get; set; }
        public Int16 ApprovedBy { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public int? ViewType { get; set; }
        public string? RejectRemarks { get; set; }
        public string VehicleNo { get; set; }
        public int DivisionId { get; set; }
        public string? DivisionShortName { get; set; }

        public string? SupplierQuoteRef { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal ExtraDiscount { get; set; }

        public decimal ExtraGSTPer { get; set; }

        public decimal ExtraGSTAmount { get; set; }
        public decimal NetAmount { get; set; }

        public string? DeliveryLocation { get; set; }

        public decimal ExtraDiscountPer { get; set; }

        public string? DiscountType { get; set; }


    }
}
