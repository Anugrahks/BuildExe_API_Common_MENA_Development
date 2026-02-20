using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IUserPrevilegeRepository
    {
        Task<IEnumerable<UserPrevilege >> Get(int usergroup, int companyid, int branchid);
        Task Insert(IEnumerable<UserPrevilege> level);
        Task Delete(int usergroup,int Companyid, int BranchId);
    }
}
