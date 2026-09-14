using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildExeHR.Models
{
    public class ExpenseReimbursement
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public List<int> DepartmentId { get; set; } = new();
        public List<int> EmployeeMasterId { get; set; } = new();
        public DateTime ApprovalDate { get; set; }
        public int ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalStatus { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime EnteredOnDate { get; set; }
        public int UserId { get; set; }
        public int Maxlevel { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }

        public List<ExpenseReimbursementItem> Reimbursement { get; set; } = new();
    }

    public class ExpenseReimbursementItem
    {
        public int EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public int ExpenseHead { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}