using BuildExeHR.Models;

namespace BuildExeHR.Repository
{
    public interface IAnnualLeaveRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<AnnualLeaveMaster> annualLeaves);
        Task<IEnumerable<Validation>> Update(IEnumerable<AnnualLeaveMaster> annualLeaves);
        Task<IEnumerable<Validation>> Delete(int id, int userId);
        Task<string> GetForEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetSettlementsById(int Id);
        Task<string> GetLeaveSurrendersById(int Id);

    }
}
