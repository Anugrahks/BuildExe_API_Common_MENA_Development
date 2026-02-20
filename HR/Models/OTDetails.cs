using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace BuildExeHR.Models
{
    public class OTDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string OTName { get; set; }
        public decimal MultiplyIndex { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

        public int UserId { get; set; }

    }


}
