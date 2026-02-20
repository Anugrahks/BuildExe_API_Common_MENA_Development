using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    
    public class ProjectBlockFloorAssignList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public Int16 IsActive { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        
        public int BlockId { get; set; }
        public string BlockName { get; set; }
    }
}
