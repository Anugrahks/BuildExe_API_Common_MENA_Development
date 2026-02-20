using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using BuildExeHR.Models;
using System.Security.Principal;

namespace BuildExeHR.Models
{
    public class SubContractorQuotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int QuotationNumber { get; set; }
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
        public List<SubContractorQuotationDetail> SubContractorQuotationDetail { get; set; }
        public List<QuotationSubContractor> QuotationSubContractor { get; set; }

    }
    public class SubContractorQuotationDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubContractorQuotationDetailId { get; set; }
        public int QuotationMasterId { get; set; }
        public int WorkId { get; set; }
        public decimal QuantityRequired { get; set; }
        public decimal WorkRate { get; set; }
        public int IndentId { get; set; }
    }
    public class QuotationSubContractor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuotationSubContractorId { get; set; }
        public int QuotationMasterId { get; set; }
        public int SubContractorId { get; set; }
    }
}