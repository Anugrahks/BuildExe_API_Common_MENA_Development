using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Level
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int Formlevel { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
