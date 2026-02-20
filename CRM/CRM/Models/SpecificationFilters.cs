using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Models
{
    [Keyless]
    public class SpecificationFilters
    {
       
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int SpecOrManual { get; set; }
        public int UnitId { get; set; }

        public int FloorId { get; set; }

        public int DivisionId { get; set; }

        public int EstimationId { get; set; }

        public string SubId { get; set; }


        public int EnquiryId { get; set; }


        public int ApprovalStatus { get; set; }

        public int BranchId { get; set; }

        public string ApprovalIdList { get; set; }

        public string NonApprovalIdList { get; set; }

        public int ApprovalLevel { get; set; }
        public int IsReject { get; set; }
        public string RejectRemarks { get; set; }
        public string ApprovalRemakrs { get; set; }

        public string EstimationIds { get; set; }
    }
}
