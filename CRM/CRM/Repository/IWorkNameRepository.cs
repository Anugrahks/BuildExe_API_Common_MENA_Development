using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IWorkNameRepository
    {
        Task<IEnumerable<Validation>> Insert(WorkName workName);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(WorkName workName);
        //void Save();

        Task<IEnumerable<WorkName>> Get(int Companyid, int Branchid);
        Task<string> Getbycompany(int Companyid, int Branchid);
        Task<WorkName> GetByID(int id);
        Task<IEnumerable<WorkName>> Getforforms(int projectId, int unitid, int blockid, int floorId, int categoryId);
        Task<IEnumerable<Validation>> CheckEditDelete(int worknameId);

    }
}
