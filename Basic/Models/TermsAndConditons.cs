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

        [ForeignKey("PrintableConfigurationId")]
        public virtual PrintableReportConfiguration PrintableReportConfiguration { get; set; } 
        public List<TermsAndConditonDetails> TermsAndCondtionDetails { get; set; }
    }

    public class TermsAndConditionDetails {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TermsAndConditonMasterId { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int IsActive { get; set; }


        [ForeignKey("TermsAndConditonMasterId")]
        public virtual TermsAndConditons TermsAndConditons { get; set; }

        public void setTermsAndConditionDetail(TermsAndConditons termsAndConditions)
        {
            TermsAndConditons = termsAndConditions;
        }

    }

}
