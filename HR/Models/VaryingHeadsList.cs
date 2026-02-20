using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    [Keyless]
    public class VaryingHeadsList
    {
        public int EmpId { get; set; }
        public string HeadName { get; set; }
        public int HeadId { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public decimal Amount { get; set; }
        public int LeaveApplicationId { get; set; }


    }
}
