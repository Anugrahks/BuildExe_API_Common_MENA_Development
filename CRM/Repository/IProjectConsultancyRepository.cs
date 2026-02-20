using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
   public  interface IProjectConsultancyRepository
    {
       Task< IEnumerable<ProjectConsultancy >> Get();
        Task<IEnumerable<ProjectConsultancy>> GetByID(int id);
        Task<string> GetConsultancy(int projectid);
        Task Insert(IEnumerable<ProjectConsultancy> projectConsultancy);
        Task Delete(int id,int userid);
        Task Update(IEnumerable<ProjectConsultancy> projectConsultancy);

        Task<IEnumerable<Validation>> CheckEditDelete(int id);

    }
}
