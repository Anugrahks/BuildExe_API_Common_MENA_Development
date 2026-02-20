using BuildExeMaterialServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
    public interface IItemIntakeRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ItemIntake> ItemIntake);
        Task<IEnumerable<Validation>> Update(IEnumerable<ItemIntake> ItemIntake);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);
        Task<string> Get(int CompanyId, int BranchId,int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<int> Getbillno(int CompanyId, int Branchid, int FinancialYearId);
        Task<string> Get_Schedulerate(int CompanyId, int Branchid, int Materialtypeid, int ProjectId, int UnitId, int Blockid, int Floorid);
        Task<string> taxArea(int SupplierId);

        Task<string> GetforReport(MaterialSearch materialSearch);

        Task<string> GetStaticReport(MaterialSearch materialSearch);
    }
}

