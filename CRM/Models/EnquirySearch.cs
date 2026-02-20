using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class EnquirySearch
    {
        public string? Phone { get; set; }
        public string? FirstName { get; set; }


        public int UserId { get; set; }

        public string? EnquiryNo { get; set; }

        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int BranchId { get; set; }
       
    }
}
