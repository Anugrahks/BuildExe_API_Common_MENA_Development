using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeServices.Models
{
  
    public class Enquiry
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryId { get; set; }
        public DateTime EnquiryDate { get; set; }
        public int? AssignStaffid { get; set; }
        public int? ModeofEnquiryid { get; set; }
        public int? ProfessionId { get; set; }
        public int? Enquiryfor { get; set; }
        public string? EnquiryNo { get; set; }


        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProjectIdEnquiry { get; set; }
        public string? ProjectNameEnquiry { get; set; }
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

        public int Status { get; set; }
        public int? NextStaff { get; set; }
        public int? Marketingteam { get; set; }
        public DateTime? ReminderDate { get; set; }

        public string? IsWorkOrder { get; set; }
        public int ApprovalLevel { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public string? Enquiry_For { get; set; }
        public string? Location { get; set; }

        public string UnqProspectName { get; set; }

        public string? ReferenceNumber { get; set; }
        public List<ToUserEnquiryDetails> ToUserEnquiryDetails { get; set; }
    }

    public class ToUserEnquiryDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToUserEnquiryDetailsId { get; set; }
        public int EnquiryId { get; set; }
        public Int16 UserId { get; set; }
    }
    public class EnquiryValidation
    {
        public int Id { get; set; }
        public string EnquiryId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
    }

}
