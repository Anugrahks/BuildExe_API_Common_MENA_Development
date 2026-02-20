using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
 public interface IBrandRepository
    {
        Task<IEnumerable<Brand >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Brand>> Get(int CompanyId, int Branchid, int userId);
        Task<Brand> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(Brand brand);
        Task Delete(int id,int userid);
        Task<IEnumerable<Validation>> Update(Brand brand);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
