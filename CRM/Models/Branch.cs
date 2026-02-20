using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Branch
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
       
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
    }
}
