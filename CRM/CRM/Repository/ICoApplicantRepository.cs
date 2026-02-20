using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface ICoApplicantRepository
    {
       Task < IEnumerable<CoApplicant >> Get();
        Task<IEnumerable<CoApplicant>> GetByID(int unitid);
        Task Insert(IEnumerable<CoApplicant> coApplicant );
    }
}
