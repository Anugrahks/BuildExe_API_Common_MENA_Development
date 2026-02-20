using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class TaskDashboard
    {

        public int? Id { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? TaskStatus { get; set; }

        public int? LiveStatus { get; set; }

        public int UserId { get; set; }
        public int BranchId { get; set; }

        public int? IsTaskApproved { get; set; }

        public int? CreatedBy { get; set; }


        public int? AssignedTo { get; set; }

        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int? ProjectId { get; set; }
        public int? DivisionId { get; set; }


        public int ActionButton { get; set; }

    }

}
