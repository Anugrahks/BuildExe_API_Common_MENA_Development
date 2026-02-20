using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BuildExeHR.Repository
{
    public interface ITableAttendanceRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<TableAttendance> attendance);
        Task<IEnumerable<TableAttendanceGet>> GetForEdit(int companyid, int branchid, int EmployeeId, string Dateworked);

        Task<IEnumerable<Validation>> Delete(string fromdate, string todate, int projectid, int employeeid, int companyId, int branchId, int isgroup);
    }
}
