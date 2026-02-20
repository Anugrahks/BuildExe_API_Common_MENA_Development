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
    public class ProjectStagePlanning
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? PlanDate { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }
        public int JobId { get; set; }
        public int CompanyId { get; set; }

        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }


        public List<ProjectStagePlanningDetails> ProjectStagePlanningDetails { get; set; }

    }


    public class ProjectStagePlanningDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectStagePlanningId { get; set; }

        public int MasterId { get; set; }

        public string? StageName { get; set; }

        public int? OrderId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? NoOfDays { get; set; }

        public decimal? NoOfHours { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ActualCompletedDate { get; set; }

        public int IsCompleted { get; set; }
        public int Rescheduled { get; set; }

        public List<ProjectStagePlanningUsers> ProjectStagePlanningUsers { get; set; }

    }

    public class ProjectStagePlanningUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectPlanningUserId { get; set; }

        public int MasterId { get; set; }
        public string? StageName { get; set; }

        public int? OrderId { get; set; }
        public int? UserId { get; set; }


    }
}
