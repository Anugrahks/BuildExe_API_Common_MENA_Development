using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System;

namespace BuildExeServices.Models
{
    public class StageActivityDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public DateTime TRDate {  get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string StageName { get; set; }
        public int? DivisionId { get; set; }
        public int? ProjectId { get; set; }
        public int JobId { get; set; }
        public decimal? PercentageCompleted { get; set; }
        public DateTime? ActualCompletedDate { get; set; }
        public int IsCompleted { get; set; }
        public string? Feedback { get; set; }


    }
}