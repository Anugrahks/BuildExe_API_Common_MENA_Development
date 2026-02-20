using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IContractorPaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ContractorPayment > contractorPayments);
        Task<IEnumerable<Validation>> Update(IEnumerable<ContractorPayment> contractorPayments);
        Task< IEnumerable<ContractorPayment>> GetbyID(int Id);

        Task<IEnumerable<ContractorPayment>> Get(int companyid, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<ContractorPaymentList>> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<ContractorPaymentList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<ContractorPaymentList>> Getforview(HRSearch hRSearch);
        Task<IEnumerable<ContractorPaymentList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> Getjson(HRSearch hRSearch);
    }
}
