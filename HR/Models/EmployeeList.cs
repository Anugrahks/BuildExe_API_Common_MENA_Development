using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    public class EmployeeList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }

        public int ClientId { get; set; }

        public string FullName { get; set; }
    }

    public class EmployeeListPersonalLedger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string EmployeeCode { get; set; }

        public int ClientId { get; set; }

        public string FullName { get; set; }

        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
    }

}
