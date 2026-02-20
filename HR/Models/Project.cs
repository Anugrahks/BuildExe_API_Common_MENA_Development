using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectTypeId { get; set; }

        public int DepartmentId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
