using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class DSRForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? EntryDate { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public string Remarks { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        public int IsReject { get; set; } = 0;
        public int IsDeleted { get; set; } = 0;

        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int UserId { get; set; } = 0;

        public DateTime EnteredOnDate { get; set; } = DateTime.Now;

        public List<DSRFormDetails> DSRFormDetails { get; set; }
    }

    public class DSRFormDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        public int MasterId { get; set; }

        public string WorkDetails { get; set; }
        public string Description { get; set; }
        public string QuantityofWork { get; set; }
        public decimal ManPowerUsed { get; set; } = 0;
        public string Remarks { get; set; }
        public string ContractorOrCompany { get; set; }
        public string DailyMaterialConsumption { get; set; }
    }
}
