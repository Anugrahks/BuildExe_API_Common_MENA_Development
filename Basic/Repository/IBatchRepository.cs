using BuildExeBasic.Models;
using BuildExeBasic.Models.DTO;
using Org.BouncyCastle.Tls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Interfaces
{
    public interface IBatchRepository
    {
        Task<Batch> AddBatchAsync(Batch batch);
        Task<List<Batch>> GetAllBatchesAsync(int CompanyId, int Branchid);
        Task<BatchDTO> GetBatchByIdAsync(long id);

        Task<string> GenerateBatchNumberAsync(int companyId, int branchid,int sitemanagerId, bool batchvalidate);
        Task<bool> UpdateCloseStateAsync(long batchId);

        Task<List<Batch>> GetBatchBySiteManagerAsync(int siteManagerId);

        Task<IEnumerable<Batch>> GetAllBatchesBySiteManagerAsync(int siteManagerId);
    }
}
