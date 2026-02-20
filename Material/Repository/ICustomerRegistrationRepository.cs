using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface ICustomerRegistrationRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<CustomerRegistration> mat);
        Task<IEnumerable<Validation>> Insert(IEnumerable<CustomerRegistration> mat);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId);
    }
}
