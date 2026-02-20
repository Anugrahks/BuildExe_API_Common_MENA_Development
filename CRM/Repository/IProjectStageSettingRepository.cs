using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IProjectWorkSettingRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectWorkSetting> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<ProjectStagePlanning> mat);
        Task<string> GetById(ProjectWorkSetting projectWorkSetting);
        Task<string> GetbyBranch(ProjectWorkSetting projectWorkSetting);


        Task<string> ViewNotificationsProject(int ProjectId, int DivisionId, int UserId, int JobId);

        Task<string> ViewNotificationsEnquiry(int Enquiry, int UserId);

        Task<string> ViewNotifications(int BranchId, int UserId, int Type);
        Task<IEnumerable<Validation>> InsertPlan(IEnumerable<ProjectStagePlanning> mat);
        Task<IEnumerable<Validation>> DeletePlan(int Id, int UserID);
        Task<string> GetPlanById(int Id);
        Task<string> GetListPlanning(int CompanyId, int BranchId, int UserId);

        Task<string> GetPlan(int ProjecId, int DivisionId, int OrderId, int JobId);
        Task<IEnumerable<Validation>> ValidationDuringShow(int ProjecId, int DivisionId, int JobId);

        Task<string> GetStages(int ProjectId, int DivisionId, int JobId);

        Task<string> GetPlanActivities(int ProjectId, int DivisionId, int OrderId, int JobId);


        Task<IEnumerable<Validation>> InsertPlanActivity(IEnumerable<StageActivityDetails> mat);

        Task<string> PlanningDashboard(int ProjectId, int DivisionId, int JobId, int PageNumber, int PageSize, int UnitId);

        Task<string> GetProjectList(int CompanyId, int BranchId, int UserId);





    }
}
