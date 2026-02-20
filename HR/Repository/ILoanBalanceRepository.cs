using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public  interface ILoanBalanceRepository
    {
       Task <IEnumerable<LoanBalance>> Get(int companyId, int Branchid, int employeeId);
        Task<decimal> GetBalance(int CompanyId, int Branchid, int employeeId);
    }
}
