using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    [Table("tbl_EmployeeMaster")]
    public class EmployeeMaster
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
