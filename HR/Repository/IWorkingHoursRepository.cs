using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IWorkingHoursRepository
    {
        Task<IEnumerable<WorkingHours>> Get();
        Task<string> Get(int companyId,int BranchId);
        Task<string> Getuser(int companyId, int BranchId, int UserId);
        Task<WorkingHours> GetdByID(int workingHoursid);
        Task<IEnumerable<Validation>> Insert(WorkingHours workingHours);
        Task<IEnumerable<Validation>> Delete(int workingHoursid, int userid);
        Task<IEnumerable<Validation>> Update(WorkingHours workingHours);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
