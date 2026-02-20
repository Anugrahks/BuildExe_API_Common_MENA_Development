using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public  interface IWorkCategoryRepository
    {
       Task < IEnumerable<WorkCategory>> Get(int companyId, int branchid);
        Task<WorkCategory> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(WorkCategory workCategory );
        Task<IEnumerable<Validation>> Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> Update(WorkCategory workCategory );
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
