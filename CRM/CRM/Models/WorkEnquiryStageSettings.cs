using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.SignalR;

namespace BuildExeServices.Models
{
    public class WorkEnquiryStageSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public int StageType { get; set; }
        public int DivisionId { get; set; }
        public int EnquiryForId {  get; set; }
        public int UserId {  get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public List<WorkEnquiryStageSettingsDetails> WorkEnquiryStageSettingsDetails { get; set; }
    }
    public class WorkEnquiryStageSettingsDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string StageName { get; set; }
        public string Remarks { get; set; }
        public int  OrderId { get; set; }
        public List<WorkEnquiryStageSettingsUsers> WorkEnquiryStageSettingsUsers { get; set; }

        [NotMapped]
        public List<QuestionnaireQuestionsJson> Questions { get; set; }
    }
    public class WorkEnquiryStageSettingsUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StageName { get; set; }

        public int OrderId { get; set; }
        public int UserId { get; set; }
    }

    [NotMapped]
    public class QuestionnaireQuestionsJson
    {
        // Matches CROSS APPLY OPENJSON(Questions) WITH (...)
        public string QuestionText { get; set; }
        public int IsMandatory { get; set; }

        public string QuestionHeader { get; set; }
    }


}
