using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class TAMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TAType { get; set; }
        public decimal TARate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
    }

}
