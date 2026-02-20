using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IOTDetailsRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<OTDetails> EmployeeJoining);
        Task<IEnumerable<Validation>> Update(IEnumerable<OTDetails> EmployeeJoining);
        Task<IEnumerable<Validation>> Delete(int id, int Userid);
        Task<string> GetByBranch(int BranchId);

    }
}
