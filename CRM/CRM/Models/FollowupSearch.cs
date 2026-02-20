using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeServices.Models
{
    [Keyless]
    public class FollowupSearch
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ModeOfEnquiryId { get; set; }
        public int? Attendedstaff { get; set; }
        public int? StatusId { get; set; }
        public DateTime? NextFolloupDate { get; set; }
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? ReportId { get; set; }
        public int? EnquiryId { get; set; }
        public int? EnquiryFor { get; set; }
    }
}
