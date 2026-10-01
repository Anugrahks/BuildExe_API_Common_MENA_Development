using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuildExeBasic.Models
{
    public class TermsAndConditons
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PrintableConfigurationId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public bool isDeleted { get; set; }

        // NEW: saved to tbl_Content, not columns of the master table
        [NotMapped] public int MenuId { get; set; }
        [NotMapped] public int RecordId { get; set; }
        [NotMapped] public int ReferenceId { get; set; }

        [ForeignKey("PrintableConfigurationId")]
        public virtual PrintableReportConfiguration PrintableReportConfiguration { get; set; }

        public List<TermsAndConditonDetails> TermsAndCondtionDetails { get; set; }
    }
}