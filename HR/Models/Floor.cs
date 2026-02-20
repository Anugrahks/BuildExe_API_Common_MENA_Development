using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class Floor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FloorId { get; set; }
        public string FloorName { get; set; }
    }
}
