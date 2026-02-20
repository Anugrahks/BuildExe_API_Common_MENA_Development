using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IAttendanceRepository
    {

        Task<IEnumerable<Attendance>> Get();
        // IEnumerable<Attendance> GetbyID(int Id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<Attendance> attendance);
        Task Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> punching(AttendancePunching attendances);
        Task<IEnumerable<Validation>> BiomatrixBulkEntry(IEnumerable<AttendancePunching> PunchingList);

        Task<string> NewBiomatrix(AttendancePunching PunchingList);

        
        Task<IEnumerable<Validation>> Update(IEnumerable<Attendance> attendance);
        Task<string> GetForEdit(int companyid, int branchid, int Menuid);
        Task<string> GetForEdituser(int companyid, int branchid, int Menuid, int UserId, int FinancialYearId);

        Task<string> GetForApprovals(int companyid, int branchid, int Userid, int Menuid, int FinancialYearId);
        Task<string> GetAttendanceDetail(AttendanceList attendanceList);
        Task<string> GetPayRollAttendanceDetail(AttendanceList attendanceList);

        Task<string> Get_attendance(HRSearch hRSearch);
        Task<IEnumerable<Validation>> Getemployeevalidation(int employeeid, DateTime dateworked);
        Task<IEnumerable<Validation>> Getemployeevalidation(int employeecategory, int labourGroup, int labourhead, DateTime dateworked);
        Task<string> Getjson(HRSearch hRSearch);

        Task<IEnumerable<ValidationAttendance>> attendanceValidation(AttendanceDetail attendanceDetails);
        //IEnumerable<AttendanceForApproval> GetForEdit(int categoryId, int projectId, int unitId, int blockId, int floorId, DateTime attendaceDate);
        //IEnumerable<AttendanceForApproval> GetForApprovals(int categoryId, int projectId, int unitId, int blockId, int floorId, DateTime attendaceDate,int UserId);

        Task BulkAttendanceEntry(List<Attendance> attendances);
        Task Edit(List<Attendance> attendances);

        Task<string> GetAttendanceDetails(HRSearch hRSearch);

        Task<string> GetDefaultDate(HRSearch attendanceDetailsSearch);
        Task<string> DateWiseReport(HRSearch hRSearch);

        Task<string> WeekWiseAttendanceReport(HRSearch hRSearch);

        Task<string> TADetails(int EmployeeId);

        Task<string> TAByMonth(int EmployeeId, int MonthId, int YearId);
    }
}
