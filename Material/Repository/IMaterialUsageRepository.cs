using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialUsageRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialUsage > materialUsage);
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialUsage> materialUsage);
        Task<IEnumerable<MaterialUsage>> GetbyID(int Id);
        Task<IEnumerable<MaterialUsage>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task<IEnumerable<MaterialusageList >> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<MaterialusageList>> GetforEdit(int companyId, int branchid, int userid,int FinancialYearId, int bulkentry);
        Task<IEnumerable<MaterialusageList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId, int bulkentry);
        Task<IEnumerable<MaterialusageList>> Getforview(MaterialSearch materialSearch);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<IEnumerable<Validation>> Getvalidation(int ProjectId, DateTime UsageDate, int FinancialYearId);
    }
}
