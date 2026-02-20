using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class WeeklyBillList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int BillType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public decimal BillAmount { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public int Maxlevel { get; set; }
    }
}
