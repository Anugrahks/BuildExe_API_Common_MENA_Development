using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class BillSearch
    {
        public int? ProjectId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? DivisionId { get; set; }

        
        public int? UnitId { get; set; }
        public int? StageStatusId { get; set; }

        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }

        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? WorkNameId { get; set; }

        public int? ViewType { get; set; }
        public int? MenuId { get; set; }

        public int? OldBrandId { get; set; }
        public int? NewBrandId { get; set; }
        public int? ItemId { get; set; }
        public int? MasStatus { get; set; }
        public int? SupplierId { get; set; }
        public int? ReportId { get; set; }

        public int PaymentModeId{ get; set; }
        public int? DepartmentId { get; set; }
        public int? Status { get; set; }

        public string EnquiryIds { get; set; } // comma-separated Enquiry IDs
        public int AttendedStaff { get; set; }
        public string Remarks { get; set; }
        public string Feedback { get; set; }
        public DateTime? FollowupDate { get; set; }
        public DateTime? NextFollowup { get; set; }
        public int MarketingTeam { get; set; }

        public int TargetUser { get; set; }

        public int DeleteType { get; set; }

        public int EnteredUser { get; set; }

        public string? StageName { get; set; }
        public int OrderId { get; set; }

        public int ActionButton { get; set; }
        public int DataId { get; set; }

        public int ClientId { get; set; }
    }
}
