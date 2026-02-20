using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class PurchaseReport
    {
       
        public int Id { get; set; }
        public string PurchaseInvoiceNo { get; set; }
        public int PurchaseOrderNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }

        public string? FloorName { get; set; }


        public string Remark { get; set; }
        public string Taxarea { get; set; }
        public int Category { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
     
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }

        public decimal BillAmount { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal AmountPaidAdvance { get; set; }
        public decimal billdiscount { get; set; }
        public decimal Roundoff { get; set; }
        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }

        public string ReqLoadingTax { get; set; }
        public string ReqTransportTax { get; set; }
        public string ReqOtherCharesTax { get; set; }

        public decimal KFCPer { get; set; }
        public decimal GSTPer { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal KFCAmount { get; set; }
        public Int16 MaterialTypeId { get; set; }
        public Int16 PaymentModeId { get; set; }
        public int SiteManagerId { get; set; }

        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }

        public int PurchaseDetailId { get; set; }
        public int PurchaseId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public int PurchaseOrderDetailsId { get; set; }
        public decimal KFC_Per { get; set; }
        public int? SlNo { get; set; }
    }
}
