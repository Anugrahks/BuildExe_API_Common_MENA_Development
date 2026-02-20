using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeMaterialServices.Models
{
    public class SupplierPaymentList
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 FinantialYearId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        
        public DateTime PaymentDate { get; set; }
        public DateTime? chequeDate { get; set; }
        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentNo { get; set; }
        public Int16 WithClear { get; set; }
        public Int16 BillWise { get; set; }
        public Int16 OnlinePayment { get; set; }
        public string Paymentdetails { get; set; }
        public Int16 IsDeleted { get; set; }
        public int ChequeClearenceID { get; set; }
        public Int16 SupplierReturn { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovedBy { get; set; }
        public int Maxlevel { get; set; }
        public int? SitemanagerId { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public string RejectRemarks { get; set; }
    }
}
