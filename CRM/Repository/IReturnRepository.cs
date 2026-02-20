using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;

namespace BuildExeServices.Repository
{
    public interface IReturnRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<ReturnMaster> returns);
        Task<IEnumerable<Validation>> Update(IEnumerable<ReturnMaster> returns);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<string> GetForEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetById(int Id);
        Task<string> GetForReport(BillSearch billSearch);
        int GetLatestCreditNote(int CompanyId, int BranchId, int FinancialYearId);
        Task<string> GetSpecificsForReturn(ReturnMaster returnMaster);
        Task<string> GetSpecificsForReturnAmount(ReturnMaster returnMaster);

    }
}
