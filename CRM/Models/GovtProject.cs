using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class GovtProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime TenderDate { get; set; }
        public string TenderType { get; set; }
        public string TenderNumber { get; set; }
    }
}
