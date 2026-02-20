using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IOwnProjectTypeRepository
    {
        Task<IEnumerable<OwnProjectType >> Get();
        Task<IEnumerable<OwnProjectType>> Get(int CompanyId, int BranchId);
        Task<IEnumerable<OwnProjectType>> Getuser(int CompanyId, int BranchId, int UserId);
        Task<OwnProjectType> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(OwnProjectType ownProjectType);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(OwnProjectType ownProjectType);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
