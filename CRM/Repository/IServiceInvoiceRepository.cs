using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface IServiceInvoiceRepository
    {
        Task<string> GetPendingServiceInvoices(int jobId, int companyId, int branchId);
        Task<string> GetPendingServiceInvoicesEdit(int jobId, int companyId, int branchId, int id);
        Task<string> GetServiceInvoiceCustomer(int jobId, int companyId, int branchId, int Id);
    }
}