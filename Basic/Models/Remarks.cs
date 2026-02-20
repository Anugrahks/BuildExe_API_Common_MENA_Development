using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class ApprovalRemarks
    {
      
        public int? Id { get; set; }
        public int? MasterId { get; set; }
        public int? MenuId { get; set; }
        public int? MenuId2 { get; set; }
        public string? Remarks { get; set; }
        public int? UserId { get; set; }
        public int? ApprovalLevel { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? FullName { get; set; }

        public int ApprovalType {  get; set; }
    }
}
