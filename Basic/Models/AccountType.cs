using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class account_type_
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int account_type_id { get; set; }
        public string  account_type_name { get; set; }
    }
}
