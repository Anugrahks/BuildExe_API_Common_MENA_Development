using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialProductionRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialProduction> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialProduction> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> getforshow(int Id);
        Task<string> getforeditshow(int MaterialId, int Id);
        Task<string> GetforReport(MaterialSearch materialSearch);
    }
}
