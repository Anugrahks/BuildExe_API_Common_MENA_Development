using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IPayrollReportRepository
    {
        Task<string> holiday(HRSearch hRSearch);
        Task<string> leave(HRSearch hRSearch);
        Task<string> salaryhead(HRSearch hRSearch);
        Task<string> salarysetting(HRSearch hRSearch);
        Task<string> attendance(HRSearch hRSearch);

        Task<string> attendanceStatic(HRSearch hRSearch);

        
        Task<string> attendancemonthly(HRSearch hRSearch);

        Task<string> EmployeeCheckInReport(HRSearch hRSearch);

        
        Task<string> leavereport(HRSearch hRSearch);
    }
}
