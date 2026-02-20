using BuildExeBasic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface ICurrencyMasterRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<CurrencyMaster> currency);

        Task<IEnumerable<Validation>> Update(IEnumerable<CurrencyMaster> currency);

        Task<IEnumerable<Validation>> Delete(int id, int userId);

        Task<string> Get(int CompanyId, int BranchId);

        Task<string> GetParent(int CompanyId);
    }
}
