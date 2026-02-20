using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class Alertfew
    {
        public int AlertType { get; set; }
        public int MasterId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string AlertMesssage { get; set; }
        public string State { get; set; }
    }
}
