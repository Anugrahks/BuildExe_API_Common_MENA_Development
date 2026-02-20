using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class ContractorWorkOrderList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateOrdered { get; set; }
        public string WorkOrderNo { get; set; }
        public int ContractorId { get; set; }
        public string? FullName { get; set; }
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
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillAmountBalance { get; set; }
        public decimal NegotiatedAmount { get; set; }
        public string TaxType { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public string Remarks { get; set; }
        public int Category { get; set; }
        public int VoucherNumber { get; set; }
        public int VoucherTypeId { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int ApprovedBy { get; set; }

        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int Maxlevel { get; set; }
        public int? ViewType { get; set; }
        public int? WorkNameId { get; set; }

        public decimal TotalWorkRoundOff { get; set; }
    }
}
