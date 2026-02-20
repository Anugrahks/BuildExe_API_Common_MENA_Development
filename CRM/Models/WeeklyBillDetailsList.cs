using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class WeeklyBillDetailsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeeklyBillDetailsListId { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int I_d { get; set; }
        public string? Spec_Id { get; set; }
        public string? SpecNumber { get; set; }
        public string? SpecName { get; set; }

        public string? SacCode { get; set; }
        public string? SpecDescription { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int WorkTypeId { get; set; }
        public string? WorkTypeName { get; set; }
        public int UnitId { get; set; }
        public string? Unit { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Decimal SpecUnit { get; set; }
        public Decimal RatePerUnit { get; set; }
        public Decimal DeptRatePerUnit { get; set; }
        public Decimal WaterElectricityCharge { get; set; }
        public Decimal LabourAdditionalCharge { get; set; }
        public Decimal SubcontractAdditionalCharge { get; set; }
        public Decimal ContractorProfit { get; set; }
        public Decimal ContractorProfitAmt { get; set; }
        public Decimal other_expense { get; set; }
        public Decimal Tax { get; set; }
        public Decimal TaxAmount { get; set; }
        public Decimal ScheduledQty { get; set; }
        public Decimal CurrentQty { get; set; }
        public Decimal? PreviousQty { get; set; }
    }
}
