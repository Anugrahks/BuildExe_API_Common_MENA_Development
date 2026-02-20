using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IContractorForPaymentRepository
    {
        Task<IEnumerable<ContractorForPayment >> Get(int Employeeid, int sitemanagerid, int financialyearId);
        Task<IEnumerable<ContractorForPayment>> Get(int Id);
    }
}
