using BuildExeMaterialServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BuildExeMaterialServices.Repository.TaskMasterRepository;

namespace BuildExeMaterialServices.Repository
{
    public interface ITaskMasterRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<TaskMaster> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);

        Task<IEnumerable<Validation>> Update(IEnumerable<TaskMaster> mat);

        Task<string> Getedit(int BranchId, int UserId, int FinancialYearId);

        Task<string> GetApproval(int BranchId, int UserId, int FinancialYearId);

        Task<string> GetById(int Id);
        Task<string> getTaskId(int BranchId);

        Task<string> getTasksAssigned(TaskDashboard mat);

        Task<string> getTasksCreated(TaskDashboard mat);
        Task<string> getTasksForApproval(int BranchId, int UserId);

        Task<IEnumerable<Validation>> Submission(IEnumerable<TaskSubmission> mat);
        Task<IEnumerable<Validation>> Extension(IEnumerable<TaskExtensionRequest> mat);
        Task<IEnumerable<Validation>> StatusUpdation(IEnumerable<TaskStatusUpdation> mat);
        Task<string> getTaskExtensionRequests(int TaskId,int UserId);
        Task<string> getTaskforExtension(int TaskId, int UserId, int TypeId);
        Task<string> getTaskDetails(int TaskId, int UserId);
        Task<IEnumerable<Validation>> ExtensionApproval(IEnumerable<TaskExtensionRequest> mat);

        Task<string> postTasksForApproval(int IsApproved, int Id, IEnumerable<TaskUpdation> mat);
        Task<string> Notifications(int BranchId, int UserId, int TypeId);

        Task<string> SubmissionView(int TaskId, int UserId);

        Task<IEnumerable<Validation>> SubmissionApproval(IEnumerable<TaskSubmission> mat);
        Task<string> getTasksGeneral(TaskDashboard mat);

        Task<string> CalenderDashboard(TaskDashboard mat);

        
    }
}
