using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ILoanRepaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<LoanRepayment> loans);
        Task<IEnumerable<Validation>> Update(IEnumerable<LoanRepayment> loans);
        Task Delete(int Id, int userId);
        Task<string> GetForEdituser(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<string> GetForApprovals(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<string> GetdetailbyId(int Id, int EmployeeId);
    }
}
