using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuildExeBasic.Models
{
    public class TermsAndConditonDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TermsAndConditonMasterId { get; set; }
        public int Order {  get; set; }
        public string Content { get; set; } 
        public bool IsDeleted { get; set; }

        [ForeignKey("TermsAndConditonMasterId")]
        public virtual TermsAndConditons TermsAndConditons { get; set; }
        public int IsActive { get; set; }
    }
}
