using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;

namespace BuildExeHR.Models
{
    [Keyless]
    public class SalaryHeadResponse
    {

        public Int32 MonthlyVaryingHeadId { get; set; }
        public int EmpId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int MonthId { get; set; }
        public int FinancialYearId { get; set; }

        public decimal BasicAmount { set; get; }
        public string HeadName { get; set; }
        public int HeadId { get; set; }
        public int SalaryTypeId { get; set; }
        public string AccountType { set; get; }
        public decimal Amount { set; get; }

    }
}
