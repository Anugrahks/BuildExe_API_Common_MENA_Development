using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank >> Get(int CompanyId, int Branchid);
        Task <Bank> GetByID(int Id);
        Task<IEnumerable<Validation>> Insert(Bank bank );
        Task<IEnumerable<Validation>> Delete(int Id ,int userID);
        Task<IEnumerable<Validation>> Update(Bank bank );
        Task<IEnumerable<Validation>> CheckEditDelete(int id);

        Task<IEnumerable<Bank>> getwithfinancial(int companyId, int branchid, int financialYearId);
    }
}
