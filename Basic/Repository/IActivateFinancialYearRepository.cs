using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IActivateFinancialYearRepository
    {
        Task ActivateFYI(int companyId, int branchId);
        Task<IEnumerable<FinancialYear>> GetActiveFinancialYear(int CompanyId, int BranchId);
        Task<FinancialYear> GetFinancilaYearByID(int FinancialYearId);
    }
}
