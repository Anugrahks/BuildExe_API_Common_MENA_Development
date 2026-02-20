using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IFinancialYearRepository
    {
       Task  <IEnumerable<FinancialYear> >GetFinancilaYear();
        Task<IEnumerable<FinancialYear>> Get(int CompanyId, int branchId);
        Task<IEnumerable<Validation>> Getregeneratebalance(int companyid,int branchid,int financialyearid);
        Task<IEnumerable<FinancialYear>> GetActiveFinancialYear(int CompanyId, int BranchId);
        Task<string> GetFinancilaYearByID(int FinancialYearId);
        Task<IEnumerable<Validation>> InsertFinancilaYear(FinancialYear financialYear);
        Task DeleteFinancilaYear(int financialYearid);
        Task UpdateFinancilaYear(FinancialYear financialYear);
        void Save();   
        Task<IEnumerable<Validation>> getvalidation1(FinancialYear financialyear);
        Task<IEnumerable<Validation>> getvalidation2(FinancialYear financialyear);
        Task<IEnumerable<Validation>> FinancialYearStatusChange(int Companyid, int BranchId, int FinancialYearId, int Type);
        Task<IEnumerable<Validation>> FinancialYearValidation(int Companyid, int BranchId, int Type);
    }
}
