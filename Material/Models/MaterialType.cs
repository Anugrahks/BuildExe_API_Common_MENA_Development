using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialTypeId { get; set; }
        public string MaterialTypeName { get; set; }
        
    }
}
