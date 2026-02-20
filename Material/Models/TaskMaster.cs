using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class TaskMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string TaskTitle { get; set; }
        
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public DateTime SubmissionDate  { get; set; }

        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }

        public string RefNo { get; set; }
        public int CreatedBy { get; set; }
        public int? AssignedTo { get; set; } 
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int? ApprovalLevel { get; set; } 
        public int? ApprovalStatus { get; set; } 
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public int IsReject { get; set; } = 0;

        public int FinancialYearId { get; set; } = 0;

        public List<TaskDetails> TaskDetails { get; set; }
    }
    public class TaskDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TaskDetailId { get; set; }
        public int TaskMasterId { get; set; }
        public int TaskNo { get; set; }     
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? CompletionDate { get; set; } 
        public DateTime? SubmissionDate { get; set; } 
        public string Remarks { get; set; }
        public decimal NoOfDays  { get; set; }
    }

    public class TaskExtensionRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int TaskMasterId { get; set; }
        public int TaskNo { get; set; }
        public DateTime ExtensionDate { get; set; }
        public decimal RequestedNoOfDays { get; set; }
        public DateTime RequestedSubmissionDate { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public decimal ApprovedNoOfDays  { get; set; }

        public DateTime ApprovedSubmissionDate { get; set; }

        public int IsApproved { get; set; }

        public string IsApprovedRemarks { get; set; }

        public string IsRejectRemarks { get; set; }
    }



    public class TaskSubmission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int TaskMasterId { get; set; }
        public int TaskNo { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualCompletedDate { get; set; }
        public DateTime ActualSubmissionDate { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int IsApproved { get; set; }
        public string IsApprovedRemarks { get; set; }
        public string IsRejectRemarks { get; set; }
        public int ForwardedUserId { get; set; }
    }


    public class TaskStatusUpdation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int TaskMasterId { get; set; }
        public int TaskNo { get; set; }
        public DateTime UpdationDate { get; set; }
        public string Feedback { get; set; }
        public string Remarks { get; set; }
        public decimal PercentageOfCompletion { get; set; }
        public int CurrentStatus { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
    }

    [Keyless]
    public class TaskUpdation
    {
        public string IsApprovedRemarks { get; set; }
        public string IsRejectRemarks { get; set; }
    }
}
