using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class JobCreation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int DivisionId { get; set; }

        public int ProjectId { get; set; }
        public int JobId { get; set; }

        public string RefNo { get; set; }

        public string JobTitle { get; set; }


        public string JobDescription { get; set; }
        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int UserId { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovalStatus { get; set; }

        public int ApprovedBy { get; set; }

        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int FinancialYearId { get; set; }

    }
}
