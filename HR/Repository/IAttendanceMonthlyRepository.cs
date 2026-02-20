using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
 public interface IAttendanceMonthlyRepository
    {
       Task <IEnumerable<AttendanceMonthly >> Get();
        Task<IEnumerable<AttendanceMonthly>> GetbyID(int Id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<AttendanceMonthly> attendances);
        Task Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<AttendanceMonthly> attendances);
        Task<IEnumerable<AttendanceMonthlyList >> GetforEdit(int companyId, int Branchid,int MenuId, int userID, int FinancialYearId);
        Task<IEnumerable<AttendanceMonthlyList>> GetforApproval(int companyId, int Branchid, int userID, int MenuId, int FinancialYearId);
        Task<string> GetDetailsbyid(int id, int employeeid, int financialyearid);
        Task<string> Getleavebyid(int id, int employeeid);
        Task<string> Showdetails(int monthid,int yearId, int companyid, int branchid, int financialyearid,int DurationId,DateTime FromDate,DateTime ToDate, int EmployeeId );
        Task<IEnumerable<Validation>> Datevalidation(int monthid, int financialyearid, int branchid, int DurationId, DateTime FromDate, DateTime ToDate, int employeeId);
    }
}
