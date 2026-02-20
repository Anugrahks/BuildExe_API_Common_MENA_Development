
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class OverHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? EntryDate { get; set; }
        public decimal TotalExpense { get; set; } = 0;
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ApprovedBy { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int UserId { get; set; }

        public List<OverHeadExpenseDetails> OverHeadExpenseDetails { get; set; }

    }

    public class OverHeadExpenseDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }

        public int ProjectId { get; set; } = 0;

        public int DivisionId { get; set; } = 0;
        public string ProjectName { get; set; } = string.Empty;

        public string DivisionName { get; set; } = string.Empty;
        public decimal BudgetedCost { get; set; } = 0;
        public decimal BudgetedCostPer { get; set; } = 0;
        public decimal BillGenerated { get; set; } = 0;
        public decimal BillGeneratedPer { get; set; } = 0;
        public decimal Receipt { get; set; } = 0;
        public decimal ReceiptPer { get; set; } = 0;
        public decimal PerOfAllocation { get; set; } = 0;
        public decimal Amt { get; set; } = 0;



    }

}
