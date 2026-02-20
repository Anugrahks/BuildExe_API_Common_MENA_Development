using System;

namespace BuildExeHR.Models
{
    public class SalaryHeadResponse2
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int MonthId { get; set; }
        public int HeadId { get; set; }
        public string HeadName { get; set; }
        public decimal? Percentage { get; set; }
        public decimal Amount { set; get; }
        public int SalaryItemHeadId { get; set; }

    }
}
