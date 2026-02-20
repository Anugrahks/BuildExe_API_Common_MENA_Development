using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class Punching
    {
        public int EmployeeId { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime Date { get; set; }
        public string SerialNumber { get; set; }


    }
}
