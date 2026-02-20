using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{
    [Keyless]
    public class HolidayLeaveList
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int MonthId { get; set; }
        public string Month { get; set; }
        public Int16 FinancialYearId { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
    }
}
