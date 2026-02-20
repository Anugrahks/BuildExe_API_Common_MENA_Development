using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BuildExeHR.Models
{
    [Keyless]
    public class SalaryVaryingHeadResponse
    {
        
        public decimal EarningHeadTot { get; set; }
        public decimal DeductionHeadTot { get; set; }
        public decimal NetAmount { get; set; }
//        Set NetAmount = BasicSalary + OverTime + EarningHeadTot - LeaveDeduction - DeductionHeadTot


    }
}
