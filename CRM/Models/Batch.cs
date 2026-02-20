using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    [Table("tbl_Batch")]
    public class Batch
    {
       
        public long Id { get; set; }
     
        public Guid GuId { get; set; } = Guid.NewGuid();
        
        public int SitemanagerId { get; set; }

        public string BatchNo { get; set; }
    }
}
