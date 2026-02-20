using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
  public  interface IPercentageBillRepository
    {
       Task Insert(IEnumerable<PercentageBill > percentageBills );
        Task Update(IEnumerable<PercentageBill> percentageBills);
        Task<IEnumerable<PercentageBill>> GetbyID(int Id);

        Task<IEnumerable<PercentageBill>> Get();
        Task<IEnumerable<PercentageBill>> Get(int companyid, int branchid);
        Task Delete(int id, int userid);
        Task<IEnumerable<PercentageBillReport >> GetReport(BillSearch billSearch);
    }
}
