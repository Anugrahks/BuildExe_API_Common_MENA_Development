using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IFinancialYearRepository
    {
        IEnumerable<FinancialYear > GetFinancilaYear();
        IEnumerable<FinancialYear> GetActiveFinancialYear(int CompanyId, int BranchId);
        IEnumerable<FinancialYear> GetFinancialYear(int CompanyId, int BranchId);
        FinancialYear GetFinancilaYearByID(int FinancialYearId);
        void InsertFinancilaYear(FinancialYear financialYear);
        void DeleteFinancilaYear(int financialYearid);
        void UpdateFinancilaYear(FinancialYear financialYear);
        void Save();
    }
}
