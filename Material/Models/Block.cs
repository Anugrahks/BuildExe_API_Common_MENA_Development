using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class Block
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
    }
}
