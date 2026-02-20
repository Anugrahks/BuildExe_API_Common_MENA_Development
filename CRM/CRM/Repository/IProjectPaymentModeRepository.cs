using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
  public  interface IProjectPaymentModeRepository
    {
       Task< IEnumerable<ProjectPaymentMode >> Get();
        Task<IEnumerable<ProjectPaymentMode>> GetByID(string projectType);
        Task Insert(ProjectPaymentMode projectPaymentMode);
        Task Delete(int id);
        Task Update(ProjectPaymentMode projectPaymentMode);
        void Save();
    }
}
