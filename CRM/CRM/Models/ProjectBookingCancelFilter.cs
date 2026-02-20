using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class ProjectBookingCancelFilter
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // Approval Status

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
    }

}
