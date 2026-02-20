using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface ITendorWorkOrderRepository
    {
        Task<IEnumerable<TendorWorkOrder >> Get();
        Task<IEnumerable<TendorWorkOrder>> GetByID(int projectid);
        Task<string> GetByID2(int projectid);
        Task<IEnumerable<Validation>> Insert(TendorWorkOrderMaster tendorWorkOrder);
        Task Delete(int id);
        Task<IEnumerable<Validation>> Update(TendorWorkOrderMaster tendorWorkOrder );
        void Save();
        Task<string> getBudgetedAmount(int id, int divisionId);
    }
}
