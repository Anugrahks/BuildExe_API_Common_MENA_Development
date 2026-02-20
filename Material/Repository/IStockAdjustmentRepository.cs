using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IStockAdjustmentRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<StockAdjustment> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<StockAdjustment> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
    }
}
