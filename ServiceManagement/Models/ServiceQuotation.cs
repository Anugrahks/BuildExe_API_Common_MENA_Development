using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeServiceManagement.Models
{
    public class ServiceQuotation
    {
        // ---------- MASTER ----------
        public int Id { get; set; }                 // For update
        public int EntryType { get; set; }
        public int? StockPointId { get; set; }
        public int SiteServiceType { get; set; }
        public string StockPoint { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int ClientId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string StationLocation { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
        public int ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public string RejectRemarks { get; set; }
        public int ApprovalStatus { get; set; }
        public string SummaryAndRecommendation { get; set; }
    }

}