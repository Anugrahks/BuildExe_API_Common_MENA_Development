using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ProjectDivision
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int BlockId { get; set; }
        
        public string BlockName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
