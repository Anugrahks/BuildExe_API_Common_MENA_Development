using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IWorkTypeRepository
    {
        Task<IEnumerable<WorkType >> Get(int Companyid,int Branchid);
        Task<IEnumerable<WorkType>> Get();
        Task<WorkType> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(WorkType workType);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(WorkType workType);
        Task<IEnumerable<Validation>> CheckEditDelete(int worktypeId);
        void Save();
    }
}
