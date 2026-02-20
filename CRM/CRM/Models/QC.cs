using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    // Represents tbl_Questionnaire
    public class QC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionnaireId { get; set; }

        public int StageDetailId { get; set; }           // FK to tbl_WorkEnquiryStageSettingDetails
        public int MasterId { get; set; }                // FK to tbl_WorkEnquiryStageSetting
        public string? StageName { get; set; }
        public int OrderId { get; set; }
        public string? QuestionnaireName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Child collection (optional, for JSON serialization)
        public List<QCQuestion>? QuestionnaireQuestions { get; set; }
    }

    // Represents tbl_QuestionnaireQuestions
    public class QCQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        public int QuestionnaireId { get; set; }          // FK to tbl_Questionnaire.QuestionnaireId
        public string? QuestionText { get; set; }
        public int IsMandatory { get; set; } = 0;         // 0 = No, 1 = Yes

        public string QuestionHeader { get; set; }
        public int? OrderId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
