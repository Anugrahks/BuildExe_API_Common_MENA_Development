using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class PercentageBill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
        public string BillNumber { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }


        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        public decimal PurchaseTotal { get; set; }
        public decimal LabourTotal { get; set; }
        public decimal ExpenseTotal { get; set; }
        public decimal StockTotal { get; set; }
        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }

        public Int16 ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }

        public string WorkDescription { get; set; }
        public string Taxarea { get; set; }
        public string TaxType { get; set; }

        public decimal ServiceChargePer { get; set; }
        public decimal ServiceChargeAmount { get; set; }

        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }

        public decimal RetensionPercent { get; set; }
        public decimal RetensionAmount { get; set; }
        public decimal LDPercent { get; set; }
        public decimal LDAmount { get; set; }

        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmount { get; set; }

        public decimal SGSTPer { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal CGSTAmount { get; set; }

        public Int16 IsDeleted { get; set; }
    }
}
