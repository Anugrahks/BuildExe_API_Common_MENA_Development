using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IClientRepository
    {
        Task<IEnumerable<ClientMaster >> GetClientMasters (int projectId, int UnitId);
        Task<string> GetClientMastersnew(int projectId, int unitId);
        Task<IEnumerable<ClientMaster>> GetClient(int Companyid, int Branchid);
        Task<IEnumerable<ClientMaster>> Get();
        Task<string> GetUniqueNames(int ProjectId, int CompanyId, int BranchId);
    }
}
