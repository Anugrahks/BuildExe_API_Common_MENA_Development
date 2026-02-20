using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ITendorAnalysisRepository
    {
        Task<IEnumerable<TendorAnalysis >> Get();
        Task<IEnumerable<TendorAnalysis>> GetByID(int projectid);
        Task<IEnumerable<Validation>> Insert(IEnumerable<TendorAnalysis> tendorAnalysis);
        Task Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(IEnumerable<TendorAnalysis> tendorAnalysis);
        Task<string> GetReport(BillSearch billSearch);
        Task<string> GetGovReport(BillSearch billSearch);
        Task<string> GetEmdReport(BillSearch billSearch);
        void Save();
    }
}
