using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class EmailStatusModel
    {
        public int id { get; set; }
        public bool status { get; set; }
    }
}
