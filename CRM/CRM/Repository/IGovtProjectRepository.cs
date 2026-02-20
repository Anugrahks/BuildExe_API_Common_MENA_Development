using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IGovtProjectRepository
    {
        Task<IEnumerable<GovtProject >> Get();
        Task<IEnumerable<GovtProject>> GetByID(int projectId);

        Task Insert(GovtProject govtProject);
        Task Delete(int projectId);
        Task Update(GovtProject govtProject);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

    }
  
    }
