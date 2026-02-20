using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISalaryIncrementRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SalaryIncrement> loans);
        Task<IEnumerable<Validation>> Update(IEnumerable<SalaryIncrement> loans);
        Task Delete(int Id, int userId);
        Task<string> GetForEdituser(int companyid, int branchid, int UserId);
        Task<string> GetForApprovals(int companyid, int branchid, int UserId);
        Task<string> GetdetailbyId(int Id, int EmployeeId);
    }
}
