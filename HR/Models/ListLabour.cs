using Microsoft.EntityFrameworkCore;
using System;

namespace BuildExeHR.Models
{

    [Keyless]
    public class ListLabour
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProjectId { get; set; }
        public int ProjectIdInAttendanceDetail { get; set; }
        public string ProjectName { get; set; }
        public decimal Work { get; set; }
        public decimal Amount { get; set; }
        public decimal OverTime { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime? FromDate { get; set; }
        public int  UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public decimal OverTimeAmount { get; set; }
        public string FloorName { get; set; }
        public string UnitLongName { get; set; }
        public string WorkShortName { get; set; }

        public string WorkCategoryName { get; set; }



    }
}
