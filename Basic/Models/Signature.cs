using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuildExeBasic.Models
{
    public class Signature
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PrintableConfigurationId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public bool isDeleted { get; set; }

        [ForeignKey("PrintableConfigurationId")]
        public virtual PrintableReportConfiguration PrintableReportConfiguration { get; set; }
        public List<SignatureDetails> SignatureDetails { get; set; }
    }

    public class SignatureDetails
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SignatureMasterId { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }


        public int IsActive { get; set; }

        [ForeignKey("SignatureMasterId")]
        public virtual Signature Signature { get; set; }

        public void setDynamicContentDetails(Signature Signature)
        {
            Signature = Signature;
        }

    }

}
