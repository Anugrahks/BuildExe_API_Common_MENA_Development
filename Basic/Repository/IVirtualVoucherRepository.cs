using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
    public interface IVirtualVoucherRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<VirtualVoucher> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<VirtualVoucher> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);

        Task<string> getAccountHead(int BranchId);
    }
}
