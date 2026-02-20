using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IAccountSubGroupRepository
    {
        Task<IEnumerable<AccountSubGroup >> Get();
        Task<IEnumerable<AccountSubGroup>> GetSubGroup(int CompanyId, int Branchid, int AccountGroupId);
        Task<IEnumerable<AccountSubGroup>> GetByID(int AccountGroupId);
    }
}
