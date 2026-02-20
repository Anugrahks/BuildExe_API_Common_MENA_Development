using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISubContractorForPaymentRepository
    {
       Task <IEnumerable<SubContractorForPayment>> Get(int Employeeid, int sitemanagerid, int financialyearId);
        Task<IEnumerable<SubContractorForPayment>> Get(int Id);
    }
}
