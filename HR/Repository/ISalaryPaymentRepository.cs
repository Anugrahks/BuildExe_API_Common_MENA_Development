using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISalaryPaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SalaryPayment > salaryPayments );
        Task<IEnumerable<Validation>> Update(IEnumerable<SalaryPayment> salaryPayments);
        Task<string> GetbyID(int Id, int EmployeeId, int CompanyId, int BranchId, int FinancialYearId);
        Task<string> GetbyID2(int Id, int EmployeeId, int CompanyId, int BranchId);

        Task<string> GetForShow(int EmployeeId, int CompanyId, int BranchId, int FinancialYearId);

        Task<string> GetForShowBulk(HRSearch hRSearch);
        Task<string> GetbyIDBulk(HRSearch hRSearch);
        Task<string> GetForEdit(int Companyid, int branchid, int userid, int FinancialYearId);
        Task<string> GetForApproval(int Companyid, int branchid,int userId, int FinancialYearId);
        Task Delete(int id, int UserId);
        Task<string> Getjson(HRSearch hRSearch);
    }
}
