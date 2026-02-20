using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ConsultancyWork
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string WorkName { get; set; }
        public int Unit { get; set; }
        public decimal  UnitRate { get; set; }
        public string Remarks { get; set; }
        public string? Sac_Code { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16  UserId { get; set; } 

    }
}
