using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class Quotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuotationMasterId { get; set; }
        public string QuotationNumber { get; set; }
        public DateTime QuotationDate { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public String? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }

        public int UserId { get; set; }
        public List<QuotationDetail> QuotationDetail { get; set; }
        public List<QuotationSupplier> QuotationSupplier { get; set; }

        public List<QuotationRate> QuotationRate { get; set; }
    }

    public class QuotationDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuotationDetailId { get; set; }// naming convension----classname+ id
        public int QuotationId { get; set; }// naming convension----parentclassname+ id
        public int MaterialId { get; set; }
        public int BrandId { get; set; }
        public decimal Quantity { get; set; }
        public int IndentId { get; set; }
    }
    public class QuotationSupplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuotationSupplierId { get; set; }
        public int QuotationId { get; set; }
        public int SupplierId { get; set; }
    }
}
