using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeLabourGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeLabourGroupId { get; set; }
        public string EmployeeLabourGroupName { get; set; }

    }
}
