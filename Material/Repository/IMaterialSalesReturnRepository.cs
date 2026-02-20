using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;

namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialSalesReturnRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialSalesReturn> salesReturns);
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialSalesReturn> salesReturns);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<string> GetForEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> GetForReport(MaterialSearch materialSearch);
        int GetLatestCreditNote(int CompanyId, int BranchId, int FinancialYearId);

    }
}
