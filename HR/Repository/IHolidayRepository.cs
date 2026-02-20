using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IHolidayRepository
    {
        // void Insert(IEnumerable<Holiday> holiday);
        //void Update(IEnumerable<Holiday> holiday);
        Task<IEnumerable<Validation>> Insert(Holiday holiday);
        Task<IEnumerable<Validation>> Update(Holiday holiday);
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<IEnumerable<Holiday>> GetbyID(int Id);
        Task<IEnumerable<Holiday>> GetbyDate(int companyid, int BranchId,DateTime date);
        Task<IEnumerable<Holiday>> Get(int compid, int branchid, int monthId, int Financialyearid);
        Task<IEnumerable<Holiday>> Get(int companyid, int BranchId);
        //void Delete(int id,int userid);
        Task<IEnumerable<Validation>> CheckEditDelete(DateTime Date, int branchId);
    }
}
