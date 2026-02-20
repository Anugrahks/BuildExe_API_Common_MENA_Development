using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class UserLogs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Int16 UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int MasterId { get; set; }
        public string FormName { get; set; }
        public int Action { get; set; }

    }
}
