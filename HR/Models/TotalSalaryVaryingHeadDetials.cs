using System.Collections;
using System.Collections.Generic;

namespace BuildExeHR.Models
{
    public class TotalSalaryVaryingHeadDetials
    {
      
       public IEnumerable<SalaryVaryingHeadDetails> SalaryVaryingHeadDetails { get; set; }
        public int EmpId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int MonthId { get; set; }
        public int FinancialYearId { get; set; }

    }
}


