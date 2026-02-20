using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BuildExeHR.Models
{
    public class Division
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DivisionId { get; set; }
        public string DivisionShortName { get; set; }
        public string DivisionLongName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
        public string Remarks { get; set; }
    }
}
