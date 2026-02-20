using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IOwnProjectRepository
    {
        Task<IEnumerable<OwnProject>> Get();
        Task<IEnumerable<OwnProject>> GetByID(int id);
        Task<IEnumerable<OwnProject>> GetUnitByID(int id);
        
        Task<IEnumerable<OwnProjectList>> GetForBooking(int id);
        Task<IEnumerable<OwnProjectList>> SelectoneUnit(int unitidid);
        Task Insert(IEnumerable<OwnProject> ownProjects);
        Task Delete(int id);
        Task Update(IEnumerable<OwnProject> ownProjects);
        void Save();
        Task<IEnumerable<OwnProject>> GetUnitsForGeneralInvoice(int projectId);
        Task<IEnumerable<OwnProject>> GetUnitsForStageInvoice(int projectId);
        Task<IEnumerable<OwnProject>> GetUnitsForClientAdvance(int projectId);
        Task<IEnumerable<OwnProject>> GetUnitsForStageReceipt(int projectId);
    }
}
