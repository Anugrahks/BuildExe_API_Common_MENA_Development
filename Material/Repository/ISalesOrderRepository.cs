using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<SalesOrder> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<SalesOrder> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<int> Getorderno(int CompanyId, int Branchid, int FinancialYearId);
        Task<int> getordernoforsales(int CompanyId, int Branchid, int FinancialYearId);
    }
}
