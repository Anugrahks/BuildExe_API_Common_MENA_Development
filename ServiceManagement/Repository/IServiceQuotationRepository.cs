using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServiceManagement.Models;
namespace BuildExeServiceManagement.Repository
{
    public interface IServiceQuotationRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ServiceQuotation> dat);
        Task<IEnumerable<Validation>> Update(IEnumerable<ServiceQuotation> dat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetData(int CompanyId, int Branchid, int UserId, int FinancialYearId);

        Task<string> GetData(int CompanyId, int Branchid, int UserId, int FinancialYearId, int CustomerId, int JobId, int Id);


    }
}
