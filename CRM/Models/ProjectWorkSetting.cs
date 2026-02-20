using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ProjectWorkSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TRDate { get; set; }
        public string? Description { get; set; }
        public string? Remarks { get; set; }
        public string? Feedback { get; set; }
        public string? FeedbackLabel { get; set; }
        public string? DocumentName { get; set; }
        public string? Document { get; set; }
        public int PreviousStatus { get; set; }
        public int NewStatus { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int JobId { get; set; }
        public string StageName { get; set; }
        public int ForwardCentralized { get; set; }
        public int ForwardAssigned { get; set; }

        [NotMapped]
        public List<QuestionnaireResponse>? QuestionnaireResponses { get; set; }
    }

    [NotMapped]
    public class QuestionnaireResponse
    {
        public int QuestionnaireId { get; set; }      // from tbl_Questionnaire
        public int StageDetailId { get; set; }        // from tbl_WorkEnquiryStageSettingDetails
        public int UserId { get; set; }
        public string? Remarks { get; set; }

        // Nested answers list
        public List<QuestionnaireAnswer>? Answers { get; set; }
    }

    [NotMapped]
    public class QuestionnaireAnswer
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int IsMandatory { get; set; }

        public string QuestionHeader { get; set; }
        public int? Answer { get; set; }             // 1 = Yes, 2 = No
        public string? Remarks { get; set; }
    }
}
