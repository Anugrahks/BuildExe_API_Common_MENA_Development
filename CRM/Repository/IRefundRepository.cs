using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IRefundRepository
    {
        Task<IEnumerable<Validation>> Insert(Refund refund );
        Task<IEnumerable<Validation>> Update(Refund refund);
        Task<IEnumerable<Validation>> Delete(int id, int userid);

        Task<IEnumerable<Refund>> Get();
        Task<IEnumerable<Refund >> GetByID(int id);
        Task<IEnumerable<Refund>> GetByID(int CompanyId,int BranchId);

        Task<string> getrefund(int CompanyId, int BranchId);


        Task<string> getrefunduser(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> getforApproval(int CompanyId, int BranchId, int UserId, int  FinancialYearId);

        Task<string> GetRefundAmount(int ProjectId, int RefundType);

        Task<string> GetRefundType(int ProjectId, int RefundType);

    }
}
