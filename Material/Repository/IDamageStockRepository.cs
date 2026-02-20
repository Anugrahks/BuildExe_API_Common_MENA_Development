using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public interface IDamageStockRepository
    {
       Task <IEnumerable<DamageStock>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<DamageStock>> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(DamageStock damageStock);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task<IEnumerable<Validation>> Update(DamageStock damageStock);
        void Save();
        Task<IEnumerable<DamageStockList >> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId);
        Task<IEnumerable<DamageStockList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<DamageStockList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId);
        Task<IEnumerable<DamageStockList>> Getforview(MaterialSearch materialSearch);
        Task<string> DamageStockReport(DamageSearch damageStock);
        Task<string> GetforReport(DamageStock damageStock);
    }
}

