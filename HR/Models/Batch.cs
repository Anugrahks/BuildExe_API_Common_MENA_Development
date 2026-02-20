using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    [Table("tbl_Batch")]
    public class Batch
    {
        public long Id { get; set; }

        public string BatchNo { get; set; }
    }
}
