
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IAssetAppreciationRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<IEnumerable<Validation>> Insert(IEnumerable<AssetAppreciation> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<AssetAppreciation> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);


        Task<string> GetApproval(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetApprovedData(int BranchId, int FinancialYearId, int UserId);

        

        Task<string> GetGrid(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetById(int Id,int CompanyId, int BranchId, int FinancialYearId);

      







    }
}
