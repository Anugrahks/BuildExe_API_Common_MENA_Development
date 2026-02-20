using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ILabourAdvanceMasterRepository
    {
        Task<IEnumerable<LabourAdvanceMaster>> Get(int Companyid, int BranchId);
        Task<IEnumerable<LabourAdvanceMaster>> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(LabourAdvanceMaster labourAdvanceMaster);
        Task<string> Report(HRSearch search);
        Task<IEnumerable<Validation>> Delete(int labourAdvanceMasterId, int userId);
        Task<IEnumerable<Validation>> Update(LabourAdvanceMaster labourAdvanceMaster);
        void Save();
        Task<IEnumerable<LabourAdvanceMasterList>> GetforEdit(int CompanyId, int Branchid, int menuid);
        Task<IEnumerable<LabourAdvanceMasterList>> GetforEdit(int CompanyId, int Branchid, int UserId, int menuid, int FinancialYearId);

        Task<IEnumerable<LabourAdvanceMasterList>> GetforApproval(int CompanyId, int Branchid, int userId, int menuid, int FinancialYearId);
        Task<string> Getjson(HRSearch hRSearch);
        Task<string> ForReport(HRSearch hRSearch);
        Task<IEnumerable<LabourAdvanceMasterList>> Getforview(HRSearch hRSearch);

        Task<string> GetAdvanceAdjustment(int BranchId, int ProjectId, int EmployeeCategoryId, int EmployeeId, int SupplierId);
    }
}
