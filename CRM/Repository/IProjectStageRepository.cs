using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
   public  interface IProjectStageRepository
    {
        Task<IEnumerable<ProjectStage>> Get();
        Task<IEnumerable<ProjectStage>> GetByID(int id, int divisionId);
        Task<IEnumerable<ProjectStage>> GetforStatusUpdate(int projectId,int Unitid, int DivisionId);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectStage> projectStage);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(IEnumerable<ProjectStage> projectStage);
        Task<IEnumerable<Validation>> UpdateStatus(int financialyearid, IEnumerable<ProjectStage> projectStage);
        Task<string> Getjson(BillSearch billSearch);
        // void StatusUpdate(IEnumerable<ProjectStage> projectStage);

        Task<IEnumerable<ProjectStage>> GetForSuperAdmin(int ProjectId, int UnitId, int DivisionId);
    }
}
