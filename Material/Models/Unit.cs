using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeMaterialServices.Models
{
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitShortName { get; set; }
        public string UnitLongName { get; set; }

        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public int Isdefault { get; set; }

        public int IsCoefficient { get; set; }


        public int CoefficientStatic { get; set; }
        }
}
