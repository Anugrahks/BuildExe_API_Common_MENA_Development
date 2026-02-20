using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISubContractorPaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorPayment> subContractorPayments );
        Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorPayment> subContractorPayments);
        Task<IEnumerable<SubContractorPayment>> GetbyID(int Id);

        Task<IEnumerable<SubContractorPayment>> Get(int companyid, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<SubContractorPaymentList>> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<SubContractorPaymentList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SubContractorPaymentList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<SubContractorPaymentList>> Getforview(HRSearch hRSearch);
    }
}
