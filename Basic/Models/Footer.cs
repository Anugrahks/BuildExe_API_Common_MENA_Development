using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class Footer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public string Post { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
