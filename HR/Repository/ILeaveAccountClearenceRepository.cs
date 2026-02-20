using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface ILeaveAccountClearenceRepository
    {
        Task<IEnumerable<Validation>> Insert(LeaveAccountClearence LeaveAccountClearence);
        Task<IEnumerable<Validation>> Update(LeaveAccountClearence LeaveAccountClearence);
        Task<IEnumerable<Validation>> Delete(int id, int Userid);
        Task<IEnumerable<LeaveAccountClearence>> GetbyID(int Idworkorder);
        Task<IEnumerable<LeaveAccountClearenceList>> GetforApproval(int companyid, int branchid, int Userid);
        Task<IEnumerable<LeaveAccountClearenceList>> GetforEdit(int companyid, int branchid);
    }
}
