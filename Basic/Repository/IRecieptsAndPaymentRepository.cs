using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public interface IRecieptsAndPaymentRepository
    {
        Task<IEnumerable<DayBook>> RecieptsAndPayment(BasicSearch basicSearch);


        Task<string> RecieptsAndPaymentReport(BasicSearch basicSearch);

        
        Task<string> GetStartDateEndDateForProject(int ProjectId);
    }
}
