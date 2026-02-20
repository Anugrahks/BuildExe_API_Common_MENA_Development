using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BuildExeServices.Models
{
    public class TimeSchedulerMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }

        public DateTime?MasterFromDate { get; set; }

        public DateTime? MasterToDate { get; set; }

        public DateTime StatusDate { get; set; }

        public int ProjectId { get; set; } = 0;
        public int DivisionId { get; set; } = 0;

        public int ScheduleType { get; set; } = 0;

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        public int IsReject { get; set; }
        public int UserId { get; set; }
        public List<TimeScheduleDetails> TimeScheduleDetails { get; set; }

    }

    public class TimeScheduleDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        public int MasterId { get; set; }
        public int ScheduleNumber { get; set; }
        public int WorkNameId { get; set; } = 0;
        public int SpecTypeId { get; set; }
        public string SpecName { get; set; }
        public string SpecDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public DateTime CurrentStartDate { get; set; }
        public DateTime CurrentCompletionDate { get; set; }

        public DateTime NewStartDate { get; set; }
        public DateTime NewCompletionDate { get; set; }
        public int OrderId { get; set; }
        public decimal NoOfDays { get; set; }
        public decimal NoOfHours { get; set; }
        public string RelatedTo { get; set; }
        public string RelationGroup { get; set; }
        public int RelationNumber { get; set; }
        public int Rescheduled { get; set; }
        public int RescheduledFrom { get; set; }

        public int IsCompleted { get; set; }
        public decimal LagByLeadBy { get; set; }



        public List<TimeScheduleSubWorkDetails> TimeScheduleSubWorkDetails { get; set; }

        public List<TimeScheduleProgressUpdates> TimeScheduleProgressUpdates { get; set; }



    }

    public class TimeScheduleSubWorkDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        public int MasterId { get; set; }
        public int ScheduleNumber { get; set; }
        public int WorkNameId { get; set; } = 0;
        public int SpecTypeId { get; set; }
        public string SpecName { get; set; }

        public string SubWorkName { get; set; }

        public string SubWorkDescription { get; set; }
        public DateTime SubworkStartDate { get; set; }
        public DateTime SubworkCompletionDate { get; set; }

        public DateTime NewSubworkStartDate { get; set; }
        public DateTime NewSubworkCompletionDate { get; set; }

        public int OrderId { get; set; }
        public decimal NoOfDays { get; set; }
        public decimal NoOfHours { get; set; }
        public string RelatedTo { get; set; }
        public string RelationGroup { get; set; }
        public int RelationNumber { get; set; }
        public int Rescheduled { get; set; }
        public int RescheduledFrom { get; set; }
        public int RescheduledFromMaster { get; set; }

        public int IsCompleted { get; set; }

        public decimal LagByLeadBy { get; set; }
        public List<TimeScheduleSubWorkProgressUpdates> TimeScheduleSubWorkProgressUpdates { get; set; }

    }



    public class TimeScheduleProgressUpdates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        public int ProjectId { get; set; }

        public int DivisionId { get; set; }
        public int ScheduleNumber { get; set; }

        public DateTime ProgressDate { get; set; }

        public string Feedback { get; set; }

        public decimal PercentageCompletion { get; set; }

        public decimal LagBy { get; set; }

        public decimal LeadBy { get; set; }


        public string? DocumentName { get; set; }

        public string? Document { get; set; }



    }


    public class TimeScheduleSubWorkProgressUpdates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        public int ProjectId { get; set; }

        public int DivisionId { get; set; }
        public int ScheduleNumber { get; set; }

        public string SubWorkName { get; set; }

        public DateTime ProgressDate { get; set; }

        public string Feedback { get; set; }

        public decimal PercentageCompletion { get; set; }

        public decimal LagBy { get; set; }

        public decimal LeadBy { get; set; }



    }

}
