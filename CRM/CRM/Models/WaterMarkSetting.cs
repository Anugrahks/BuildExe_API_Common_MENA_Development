using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class WaterMarkSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? WatermarkImage { get; set; }
        public string isSelect { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
    }
}
