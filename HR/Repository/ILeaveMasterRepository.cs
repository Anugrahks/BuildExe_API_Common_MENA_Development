using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface ILeaveMasterRepository
    {
       Task< IEnumerable<LeaveMaster >> Get(int companyid, int Branchid);
        Task<string> Getleave(int companyid, int branchid, int UserId);
        Task<string> Getleavewithholiday(int companyid, int branchid, int EmployeeId, DateTime DateWorked);
        Task<string> Getleavewithemployee(int companyid, int branchid, int EmployeeId, int MonthId, int YearId);
        Task<string> GetByID(int Id);

        Task<string> AutoFetch(int branchId);

        
        Task<IEnumerable<Validation>> Insert(LeaveMaster leaveMasters);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> Update(LeaveMaster leaveMasters);
        Task<string> SalaryPerDay(LeaveSettingsDet leaveSettings);
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int branchId);

        Task<string> GetleavewithemployeeMobile(int companyid, int branchid, int EmployeeId, int MonthId, int YearId);


        Task<string> SalaryPerDayMobile(LeaveSettingsDet leaveSettings);

    }
}
