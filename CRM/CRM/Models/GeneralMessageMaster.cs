using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.SignalR;

namespace BuildExeServices.Models
{
    public class GeneralMessageMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TRDate { get; set; }
        public string GeneralMessage { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int TargetUser { get; set; }

        public int? SitemanagerId { get; set; }

        public int ApprovalStatus { get; set; }

        public string ApprovalRemarks { get; set; }

        public string RejectRemarks { get; set; }



        public int ApprovalLevel { get; set; }

        public int ApprovedBy { get; set; }
        
        public int Isreject { get; set; }
    }
}