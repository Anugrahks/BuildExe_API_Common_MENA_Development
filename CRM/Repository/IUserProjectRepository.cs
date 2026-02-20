using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IUserProjectRepository
    {
        Task Insert(IEnumerable<UserProject > userProjects );
        Task Delete( int userid);
        Task Update(IEnumerable<UserProject> userProjects);
        Task< IEnumerable<UserProject>> Get();
        Task<IEnumerable<UserProject>> GetByID(int id);
    }
}
