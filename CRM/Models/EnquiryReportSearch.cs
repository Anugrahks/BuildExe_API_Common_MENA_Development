using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeServices.Models
{
    [Keyless]
    public class EnquiryReportSearch
    {
        public DateTime ? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ModeOfEnquiryId { get; set; }
        public int? EnquiryId { get; set; }
        public int? EnquiryStatusId { get; set; }
        public int? EnquiryForId { get; set; }
        public int? AssignStaffId { get; set; }
        public int? MarketingteamId { get; set; }
        public DateTime ? ReminderDate { get; set; }

        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? ReportId { get; set; }

        public int? FinancialYearId { get; set; }

        public int? MonthId { get; set; }


        public int? LeadType { get; set; }
        public int? UserId { get; set; }
    }
}
