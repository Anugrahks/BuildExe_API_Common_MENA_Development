using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IForemanPaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ForemanPayment > foremanPayments );
        Task<IEnumerable<Validation>> Update(IEnumerable<ForemanPayment> foremanPayments);
        Task<IEnumerable<ForemanPayment>> GetbyID(int Id);

        Task<IEnumerable<ForemanPayment>> Get(int companyid, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);

        Task<IEnumerable<ForemanPaymentList >> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<ForemanPaymentList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<ForemanPaymentList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<ForemanPaymentList>> Getforview(HRSearch hRSearch);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> Getjson(HRSearch hRSearch);
    }
}
