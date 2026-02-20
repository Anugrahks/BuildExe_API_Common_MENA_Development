using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class ForemanWorkOrderList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string WorkName { get; set; }
        public DateTime DateOrdered { get; set; }
        public int ForemanId { get; set; }
        public string? FullName { get; set; }
        public int WorkTypeId { get; set; }
        public string  workTypeName { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string ProjectName { get; set; }
        public string DivisionShortName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string Description { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
      
        public Int16 WorkStatus { get; set; }
        public Int16 UserId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public int Maxlevel { get; set; }
        public int? viewType { get; set; }
        public decimal? BudgetedAmount { get; set; }
    }
}
