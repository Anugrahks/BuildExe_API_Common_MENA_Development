using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BuildExeServices.Models
{
    [Keyless]
    public class WorkEnquiryStageSettingsDashBoard
    {
        public int? MonthId { get; set; }
        public int? EnquiryFor { get; set; }
        public DateTime? ReminderDate { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int TypeId { get; set; }

        public int? Status { get; set; }

        public int Job { get; set; }

        public int? UnitId { get; set; }

        public int? UserId { get; set; }
    }
}
