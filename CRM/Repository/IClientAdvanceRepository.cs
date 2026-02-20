using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IClientAdvanceRepository
    {
        Task<IEnumerable<ClientAdvance>> Get(int CompanyId, int BranchId);
        Task<IEnumerable<ClientAdvance>> GetByID(int Id);

        Task<IEnumerable<Validation>> Insert(ClientAdvance clientAdvance);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> Update(ClientAdvance clientAdvance);
        Task<IEnumerable<ClientAdvanceList>> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId);
        Task<IEnumerable<ClientAdvanceList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<ClientAdvanceList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId);
        Task<string> GetReport(BillSearch billSearch);
    }
}
