using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IAttendancePayRollRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<AttendancePayroll> attendance);
        Task Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> Update(IEnumerable<AttendancePayroll> attendance);
        Task<string> GetPayRollAttendanceDetail(int Id, int EmployeeId,  int Isdetail);
        Task<string> GetPayRollAttendancebyid(AttendancePayrollList attendanceList);
        Task<IEnumerable<Validation>> Getvalidationpayroll(DateTime dateworked, int companyid, int branchid, int departmentId);
        Task<string>  LatePenaltyDetails(DateTime dateworked, int EmployeeId);
    }
}
