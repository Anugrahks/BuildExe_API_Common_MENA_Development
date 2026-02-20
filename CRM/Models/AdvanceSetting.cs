using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class AdvanceSetting
    {

        public int LabourAdvanceProject { get; set; }
        public int GroupLabourAdvanceProject { get; set; }
        public int ForemanAdvanceProject { get; set; }
        public int SupplierAdvanceProject { get; set; }
        public int FinancialYearId { get; set; }

    }
}
