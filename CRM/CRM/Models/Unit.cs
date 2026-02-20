using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        public string UnitShortName { get; set; }
        public string UnitLongName { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
    }
}
