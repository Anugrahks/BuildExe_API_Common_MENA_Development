using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit >> Get(int CompanyId,int Branchid);
        Task<IEnumerable<Unit>> Get(int CompanyId, int Branchid, int UserId);

        Task<IEnumerable<Unit>> StaticGet(int CompanyId, int Branchid, int UserId);

        
        Task<Unit> GetByID(int id);
        //  IEnumerable<Unit> GetByID(int id,int companyid);
        //Task Insert(Unit unit);
        Task Delete(int id,int userid);
        //Task Update(Unit unit);
        void Save();
        Task<IEnumerable<Validation>> Insert(Unit unit);
        Task<IEnumerable<Validation>> Update(Unit unit);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
