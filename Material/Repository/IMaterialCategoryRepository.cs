using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialCategoryRepository
    {

       Task< IEnumerable<MaterialCategory >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<MaterialCategory>> Get(int CompanyId, int Branchid, int UserId);
        Task<MaterialCategory> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(MaterialCategory materialCategory);
        Task Delete(int id,int userid);
        Task<IEnumerable<Validation>> Update(MaterialCategory materialCategory);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
