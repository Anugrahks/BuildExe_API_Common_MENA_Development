using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Followup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowupId { get; set; }
        public int EnquiryId { get; set; }
        public int UserId { get; set; }

        public DateTime Followupdate { get; set; }

        public int Attendedstaff { get; set; }
        public string feedback { get; set; }
        public string Remarks { get; set; }
        public DateTime nextfollowup { get; set; }
        public int status { get; set; }
        public int Marketingteam { get; set; }
        public int OrderId { get; set; }
        public string StageName { get; set; }

        public int ForwardCentralized { get; set; }

        public int ForwardAssigned { get; set; }

        public int StageStatus { get; set; }

        public string referenceNumber { get; set; }


        public string UnqProspectName { get; set; }


        public string? Mode { get; set; }


        public string? Enquiry_For { get; set; }


        public DateTime EnquiryDate { get; set; }






    }
}
