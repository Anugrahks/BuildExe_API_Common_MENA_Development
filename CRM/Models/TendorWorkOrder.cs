using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{

    public class TendorWorkOrderMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public string? StatusDescription { get; set; }
        public int? FinancialYearId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? VouNo { get; set; }
        public int? VouTypeId { get; set; }
        public int? DepositStatus { get; set; }
        public List<TendorWorkOrder> TendorWorkOrderDetails { get; set; }

    }
    public class TendorWorkOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public DateTime? WorkorderDate { get; set; }
        public int? ProjectId { get; set; }
        public int DivisionId { get; set; }
        public decimal? SecurityDepositAmount { get; set; }
        public string? SecurityDepositType { get; set; }
        public int? SecurityDepositTypeId { get; set; }
        public decimal? PerformanceGuarantee { get; set; }
        public string? SecurityDepositNarration { get; set; }
        public string? SecurityDepositNo { get; set; }
        public string? WorkOrderNarration { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public string? StatusDescription { get; set; }

        public int? FinancialYearId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? VouNo { get; set; }
        public int? VouTypeId { get; set; }
        public int? DepositStatus { get; set; }
        public decimal? BudgetedAmount { get; set; }

    }
}
