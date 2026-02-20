using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class EnquiryStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryStatusId { get; set; }
        public string Status { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }

    }
}
