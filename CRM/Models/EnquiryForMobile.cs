using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeServices.Models
{
    public class EnquiryForMobile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryId { get; set; }
        public DateTime EnquiryDate { get; set; }
        public int? Enquiryfor { get; set; }
        public int? AssignStaffid { get; set; }
        public int? ModeofEnquiryid { get; set; }
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
        public int? Status { get; set; }
        public int? NextStaff { get; set; }
        public int? Marketingteam { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string? IsWorkOrder { get; set; }
        public int? ApprovalLevel { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
    }
}
