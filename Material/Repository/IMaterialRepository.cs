using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Material > material);
        Task<IEnumerable<Validation>>  Update(IEnumerable<Material> material);
        Task<string> GetbyID(int Id);
        Task<IEnumerable<Material>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Material>> Get(int CompanyId, int Branchid, int UserId);
        
        
        Task<IEnumerable<Material>> GetMaterialWithBrand(int CompanyId, int Branchid);
        Task<IEnumerable<Material>> MaterialWithBrandCategory(int CompanyId, int Branchid,int MaterialTypeId);

        
        Task<IEnumerable<Material>> GetMaterialWithBrandtype(int CompanyId, int Branchid, int MaterialTypeId);
        Task<string> GetbyMaterialId(int CompanyId, int Branchid, string MaterialId);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task <string> GetReport(MaterialSearch MaterialSearch);
        Task<IEnumerable<MaterialStockReport>> GetStockReport(MaterialSearch MaterialSearch);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

        Task<int> TransferId(int BranchId, int FinancialYearId);
        Task<int> ReceiveId(int BranchId, int FinancialYearId);
        Task<int> ConsumptionId(int BranchId, int FinancialYearId);
        Task<int> QuotationId(int BranchId, int FinancialYearId);
        Task<string> GetReport(MismatchSearch mismatchSearch);
        Task<string> getwithfinancialId(int Id, int FinancialYearId);


    }
}
