using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IMonthlyVaryingHeadSettingRepository
    {
        Task<string> Get(int companyId, int Branchid, int UserId, int MonthId, int YearId,int EmployeeId, int DurationId, DateTime FromDate, DateTime ToDate);
        Task<string> GetByUser(int companyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetByApproval(int companyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<Validation>> Insert(IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters);
        Task<IEnumerable<Validation>> Update(IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters);
        Task<IEnumerable<Validation>> Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> CheckEditDelete(int Id);
    }
}
