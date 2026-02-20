using BuildExeMaterialServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
    public interface IItemReturnTransferRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ItemReturnTransfer> ItemReturn);
        Task<IEnumerable<Validation>> Update(IEnumerable<ItemReturnTransfer> ItemReturn);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);
        Task<int> Getbillno(int CompanyId, int Branchid, int FinancialYearId);
        Task<string> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> Get(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> Show(ItemSearch itemSearch);
        Task<string> GetbyId(int Id);
        Task<string> MaterialList(ItemSearch itemSearch);
        Task<string> MaterialListApproved(ItemSearch itemSearch);

    }
}
