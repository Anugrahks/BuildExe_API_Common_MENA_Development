using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
    }
}
