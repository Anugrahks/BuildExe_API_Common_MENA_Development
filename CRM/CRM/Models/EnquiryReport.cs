using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeServices.Models
{
    
    public class EnquiryReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EnquiryId { get; set; }
        public DateTime EnquiryDate { get; set; }
        public int? AssignStaffid { get; set; }
        public string? AssignStaff { get; set; }
        public int? ModeofEnquiryid { get; set; }
        public string? ModeofEnquiry { get; set; }
        public string? Mode { get; set; }
        public int? EnquiryforId { get; set; }
        public string? Enquiryfor { get; set; }
        public string? EnquiryNo { get; set; }


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Mobile2 { get; set; }
        public string? Sex { get; set; }
        public string? EmailId { get; set; }
        public string? gstno { get; set; }

        public string? OfficeAddress { get; set; }
        public string? OfficePhone { get; set; }


        public string? Remarks { get; set; }
        public string? CustReq { get; set; }
        public string? ProjectDesc { get; set; }

        public string? Status { get; set; }
        public int? NextStaff { get; set; }
        public int? MarketingteamId { get; set; }
        public string? Marketingteam { get; set; }
        public DateTime? ReminderDate { get; set; }

        public string? IsWorkOrder { get; set; }
        public int ApprovalLevel { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public int? SlNo { get; set; }


    }
}
