using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialRecieveList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReceiveMasterId { get; set; }
        public int TransferId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ProjectIdTo { get; set; }
        public string ProjectName { get; set; }
        public int UnitIdTo { get; set; }
        public string? UnitName { get; set; }
        public int BlockIdTo { get; set; }
        public string? BlockName { get; set; }
        public int FloorIdTo { get; set; }
        public string? FloorName { get; set; }
        public int DivisionIdTo { get; set; }
        public string? DivisionName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
      
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public DateTime ReceiveApprovedDate { get; set; }
        public Int16 ReceiveApprovedBy { get; set; }


        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal MiscellaneousExpense { get; set; }
        public decimal MiscellaneousExpensePer { get; set; }

        public decimal NetAmount { get; set; }
        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public int IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }

        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? ViewType { get; set; }
        public decimal? RoundOff { get; set; }
        public string Remarks { get; set; }
        public int IsAsset { get; set; }

    }
}
