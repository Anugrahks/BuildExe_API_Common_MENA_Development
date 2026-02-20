using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IWorkEnquiryStageSettingsRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<WorkEnquiryStageSettings> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<WorkEnquiryStageSettings> mat);
        Task<string> Getforedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Dashboard(WorkEnquiryStageSettingsDashBoard workEnquiryStageSettingsDashBoard);
        Task<string> JobDashboard(WorkEnquiryStageSettingsDashBoard workEnquiryStageSettingsDashBoard);
        Task<string> GetById(int Id);
        Task<string> getNormalProject(int BranchId);
        Task<string> getDataForDashboard(EnquiryReportSearch billSearch);

        Task<string> getEnquiryByMonth(EnquiryReportSearch billSearch);
        Task<string> EditDelete(int id, int userId);
        Task<string> getmessage(int UserId);


        Task<string> Report(BillSearch search);

        Task<string> forwardmessage(int Id, int UserId, int ApprovalStatus);
        Task<string> GetforeditMessage(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<Validation>> UpdateMessage(IEnumerable<GeneralMessageMaster> mat);
        Task<IEnumerable<Validation>> DeleteMessage(int Id, int UserID, int DeleteType);

        Task<IEnumerable<Validation>> InsertMessage(IEnumerable<GeneralMessageMaster> mat);
        Task<IEnumerable<Validation>> UpdateEnquiryBulk(GeneralMessageMaster mat);
        Task<string> GetDataForSync(string TableName, DateTime LastSyncDate);
        Task<string> getmessageEnquiry(int UserId);

        Task<string> getmessageIndent(int UserId);

        Task<string> closemessageIndent(int Id, int UserId);

        
        Task<string> Postforwardmessage(GeneralMessageMaster mat);

    }
}
