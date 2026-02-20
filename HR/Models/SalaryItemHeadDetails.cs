using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    [Keyless]
    public class SalaryItemHeadDetails
    {
        public int Id { get; set; }
        public int AccountHeadid { get; set; }
        public string HeadName { get; set; }
        public string CalculateOn { get; set; }
        public char CalculationMode { get; set; }
        public decimal UpperLimit { get; set; }


    }
}
