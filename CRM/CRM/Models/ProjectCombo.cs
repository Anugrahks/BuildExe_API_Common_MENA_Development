using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ProjectCombo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectTypeId { get; set; }
        public string ProjectName { get; set; }
        public int Status { get; set; }
    }
}
