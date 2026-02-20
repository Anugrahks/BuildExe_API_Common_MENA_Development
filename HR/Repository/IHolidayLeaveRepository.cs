using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IHolidayLeaveRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<HolidayLeave> holidayLeaves);
        Task<IEnumerable<Validation>> Update(IEnumerable<HolidayLeave> holidayLeaves);
        Task<IEnumerable<Validation>> Delete(int employeeId, int monthid, int finid, int userid);
        Task<IEnumerable<HolidayLeaveList>> GetForEdit(int Companyid, int Branchid, int finid);
        Task<IEnumerable<HolidayLeave>> Get(int EmployeeId, int Month, int finid);

    }
}
