using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ITendorSubmittedRepository
    {
        Task<IEnumerable<TendorSubmitted>> Get();
        Task<IEnumerable<TendorSubmitted>> GetByID(int projectid);
        Task<IEnumerable<Validation>> Insert(TendorSubmitted tendorSubmitted);
        Task Delete(int id);
        Task<IEnumerable<Validation>> Update(TendorSubmitted tendorSubmitted);
        void Save();
    }
}
