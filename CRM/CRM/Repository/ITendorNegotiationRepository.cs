using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ITendorNegotiationRepository
    {
        Task<IEnumerable<TendorNegotiation>> Get();
        Task<IEnumerable<TendorNegotiation>> GetByID(int projectid);
        Task Insert(TendorNegotiation tendorNegotiation);
        Task Delete(int id);
        Task Update(TendorNegotiation tendorNegotiation);
        void Save();
        Task<string> GetprojectSpec_Negotiated(int ProjectID);
        Task Insert(IEnumerable<SpecificationNegotiation> specificationNegotiation);
    }
}
