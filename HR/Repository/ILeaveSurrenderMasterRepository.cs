
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ILeaveSurrenderMasterRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<IEnumerable<Validation>> Insert(IEnumerable<LeaveSurrenderMaster> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<LeaveSurrenderMaster> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);


        Task<string> GetApproval(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetApprovedData(int BranchId, int FinancialYearId, int UserId);

        

        Task<string> GetGrid(int BranchId, int FinancialYearId, int UserId);

        Task<string> GetById(int Id,int CompanyId, int BranchId, int FinancialYearId);

      







    }
}
