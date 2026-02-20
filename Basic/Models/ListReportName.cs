using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class ListReportName
    {

       public int MenuId { get; set; }
       public string MenuName { get; set; }
    }
}
