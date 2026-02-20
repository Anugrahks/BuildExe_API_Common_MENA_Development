using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ReportHeaderSettings
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public string HeaderName { get; set; }       
        public string? HeaderImage { get; set; }       
        public string isSelect { get; set; } 
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
    }
}
