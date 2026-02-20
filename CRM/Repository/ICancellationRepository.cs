using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ICancellationRepository
    {
        Task<string> Indent(int ProjectId, int UnitId, int BlockId, int Division, int FloorId, int Type);
        Task<string> PurchaseOrder(int ProjectId, int UnitId, int BlockId, int Division, int FloorId, int Type);
        Task<IEnumerable<Validation>> Insert(IEnumerable<Cancellation> cancellations);
        Task<string> Project(int CompanyId, int BranchId, int UserId, int SiteUser, int TypeId);
        Task<IEnumerable<Validation>> Put(AdvanceSetting advanceSetting);
        Task<string> AdvanceSettingFinancialYear(int BranchId, int FinancialYearId);
        Task<string> AdvanceSetting(int BranchId);
        Task<string> ApprovalView(int BranchId, int FinancialYearId, int MenuId);
    }
}
