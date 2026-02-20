
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IOverHeadRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<IEnumerable<Validation>> Insert(IEnumerable<OverHead> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<OverHead> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);


        Task<string> GetApproval(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetGrid(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetLatestFrom(int FinancialYearId);


        Task<string> GetTotalExpense(MaterialSearch materialSearch);

        Task<string> GetProjectList(MaterialSearch materialSearch);



    }
}
