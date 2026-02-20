using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ClientMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Sex { get; set; }
        public string? GST_No { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Post { get; set; }
        public string? Pin { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }

        public int? PaymentModeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int? EnquiryId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }

        //public string? OpeningType { get; set; }
        //public decimal? OpeningBalance { get; set; }
    }
}
