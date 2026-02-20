using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ProjectStage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int OwnProjectDetailsiId { get; set; }
        public string  StageName { get; set; }
        public int StageStatusId { get; set; }
        public string? StageRemarks { get; set; }
        
        public DateTime DateToStart { get; set; }
        public DateTime DateToComplete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateDue { get; set; }


        public decimal  PaymentPercentage { get; set; }
        public decimal? StageAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public string?  StageType { get; set; }
        public string? SacCode { get; set; }
        public string TaxInclusive { get; set; }
        public string TaxArea { get; set; }
        public int PaymentModeId { get; set; }

        public decimal SGSTPercent { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal CGSTPercent { get; set; }
        public decimal cGSTAmt { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal LabourWelfarePercent { get; set; }
        public decimal LabourWelfareAmount { get; set; }
        public decimal TdsPercent { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal Kfcper { get; set; }
        public decimal KfcAmt { get; set; }
        public decimal Discount { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AmountBalance { get; set; }

        public int? VoucherTypeId { get; set; }
        public int? VoucherNumber { get; set; }
        public int  UserId { get; set; }
        public int PercentageWise { get; set; }
        public decimal StageTotalAmount { get; set; }
        public decimal StageRoundOff { get; set; }
        public decimal StageNetAmount { get; set; }
        public int DivisionId { get; set; }

        public decimal StageInitialAmount { get; set; }

        public int OrderId { get; set; }
        public string? ReferenceNo { get; set; }

        public int WorkCategoryId { get; set; }


    }
}
