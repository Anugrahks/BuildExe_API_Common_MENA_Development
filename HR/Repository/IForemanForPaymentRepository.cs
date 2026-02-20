using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IForemanForPaymentRepository
    {
       Task <IEnumerable<ForemanForPayment>> Get(int Employeeid, int sitemanagerid, int financialyearId, DateTime date);
        Task<IEnumerable<ForemanForPayment>> GetforEdit(int id);


        Task<string> GetforEditabstract(int id);
        Task<IEnumerable<ForemanForPaymentAbstract>> GetAbstract(int Employeeid, int sitemanagerid, int financialyearId, DateTime date);
        Task<IEnumerable<Validation>> BillValidation(int foremanId, DateTime date);
    }
}
