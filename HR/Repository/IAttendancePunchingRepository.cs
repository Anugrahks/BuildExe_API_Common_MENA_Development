using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IAttendancePunchingRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<AttendancePunchingConfirm> attendance);
        Task Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> Update(IEnumerable<AttendancePunchingConfirm> attendance);
        Task<string> GetEmployee(int EmployeeId, DateTime DateWorked);
        Task<string> Getdetails(int Id);

        Task<string> GetListDetails(int BranchId);

        Task<string> PunchingDetails(IEnumerable<HRSearch> attendances);
    }
}
