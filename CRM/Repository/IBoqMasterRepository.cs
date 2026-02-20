using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IBoqMasterRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<BoqMaster > boqMasters );
        Task<IEnumerable<Validation>> Update(IEnumerable<BoqMaster> boqMasters);
        Task Delete(int id, int userid);
        Task<IEnumerable<BoqMaster>> GetbyID(int Idworkorder);
        Task<IEnumerable<BoqMasterList >> GetForEdit(int CompanyId,int BranchId);

        Task<string> GetForEdituser(int companyid, int Branchid, int UserId, int FinancialYearId);

        Task<string> getuserSteel(int companyid, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<BoqMasterList>> GetForApproval(int CompanyId, int BranchId,int UserId, int FinancialYearId);
         Task<IEnumerable<BoqDetailsList >> GetDetails(int Id);
        Task<IEnumerable<Validation>> GetValidation(int Projectid, int UnitId, int Blockid, int FloorId, int WorkNameid,int Id);
        Task<string> GetFor_MasApproval(int ProjectId);
        Task Update_MAsStatus(BillSearch billSearch);
    }
}
