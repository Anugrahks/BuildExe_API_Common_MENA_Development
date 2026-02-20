using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ProjectBlockFloorAssign
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Int16  IsActive { get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int UnitId { get; set; }
        public int UserId { get; set; }


    }
}
