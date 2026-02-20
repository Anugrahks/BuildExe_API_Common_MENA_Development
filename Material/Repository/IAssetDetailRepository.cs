using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IAssetDetailRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<IEnumerable<Validation>> Insert(IEnumerable<AssetDetailEntryMaster> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<AssetDetailEntryMaster> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);


        Task<string> GetApproval(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetApprovedData(int BranchId, int FinancialYearId, int UserId);

        

        Task<string> GetGrid(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetById(int Id);

        Task<string> GetPurchaseList(int BranchId);


        Task<string> GetMaterialList(MaterialSearch materialSearch);

        Task<string> Material(MaterialSearch materialSearch);
        Task<string> Report(MaterialSearch materialSearch);









    }
}
