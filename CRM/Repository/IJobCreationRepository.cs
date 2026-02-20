using BuildExeServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BuildExeServices.Repository.JobCreationRepository;

namespace BuildExeServices.Repository
{
    public interface IJobCreationRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<JobCreation> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);

        Task<IEnumerable<Validation>> Update(IEnumerable<JobCreation> mat);


        Task<IEnumerable<Validation>> UpdateStage(IEnumerable<StageActivationDeActivation> mat);

        Task<string> Getedit(int BranchId, int UserId, int FinancialYearId);

        Task<string> GetApproval(int BranchId, int UserId, int FinancialYearId);


        Task<string> getStages(int ProjectId, int DivisionId);

        

        Task<string> getTaskId(int BranchId);

        Task<string> getProject(int DivisionId);
        Task<string> getJobByProject(int ProjectId, int DivisionId);

    }
}
