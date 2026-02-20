using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuildExeHR.Models
{
    [Keyless]
    public class ListEmployeeByCategory
    {
        public string FullName { get; set; }
        public int EmployeeId { get; set; }
        public int HasWorkingHours { get; set; }
        public int LabourHeadId { get; set; }
    }
}
