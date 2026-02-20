using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IReportHeaderSettingsRepository
    {
        IEnumerable<ReportHeaderSettings> GetHeader();
        ReportHeaderSettings GetByID(int id);
        IEnumerable<ReportHeaderSettings> GetByCompanyAndBranch(int companyId, int branchId);
        Task<IEnumerable<Validation>> Insert(ReportHeaderSettings reportHeaderSettings);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<IEnumerable<Validation>> Update(ReportHeaderSettings reportHeaderSettings);
        Task<IEnumerable<Validation>> HeaderUpdate(int companyid, int branchID, int id, string status);
        Task<IEnumerable<ReportHeaderSettings>> HeaderStatusByType(int companyid, int branchID, int id, int type, string status);
        Task<IEnumerable<Validation>> HeaderNameValidation(int companyid, int branchID, int id, string headerName);
    }
}
