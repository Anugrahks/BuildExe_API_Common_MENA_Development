using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class SubContractorWorkOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public DateTime DateOrdered { get; set; }
        public string WorkOrderNo { get; set; }
        public int SubContractorId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }

        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public string Remarks { get; set; }

        public Int16 WorkOrderStatus { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }
        public int? Category { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int Attendancetype { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }

        public int UserId { get; set; }
        public List<SubContractorWorkOrderDetails> SubContractorWorkOrderDetails { get; set; }


    }
    public class SubContractorWorkOrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubContractorWorkOrderDetailsId { get; set; }
        public int SubContractorWorkOrderId { get; set; }
        public int IndentId { get; set; }
        public int WorkId { get; set; }
        public decimal QuantityOrdered { get; set; }
        public decimal WorkRate { get; set; }
        public int UnitId { get; set; }

    }
}
