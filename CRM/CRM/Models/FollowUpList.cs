using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class FollowUpList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EnquiryId { get; set; }
        public DateTime EnquiryDate { get; set; }
        
        public int ModeofEnquiryid { get; set; }
        public string Mode { get; set; }
        public int Enquiryfor { get; set; }
        public string? Enquiry_For { get; set; }
        public string? EnquiryNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        
        public string? CustReq { get; set; }
        public DateTime? Followupdate { get; set; }

        public string? Attendedstaff { get; set; }
        public string? AttendedstaffName { get; set; }
        public string? feedback { get; set; }
        public string? Remarks { get; set; }
        public DateTime nextfollowup { get; set; }
        public string? status { get; set; }
        public int SlNo { get; set; }
        public int MarketingteamId { get; set; }
        public int AttendedstaffId { get; set; }
        public int EnquiryStatusId { get; set; }

        public int OrderId { get; set; }

        public string StageName { get; set; }
        public int StageStatus { get; set; }
    }
}
