using Microsoft.EntityFrameworkCore;
using System;

namespace BuildExeHR.Models
{

    [Keyless]
    public class LabourForPayment
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int EmployeeId { get; set; }
        public int SitemanagerId { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime DateTo { get; set; }
        public string EmployeeIdList { get; set; }

        public int EmployeeLabourGroupId { get; set; }




    }
}
