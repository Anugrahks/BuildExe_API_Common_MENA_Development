using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class MaterialTransferList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TransferDate { get; set; }
        public int TransferMasterId { get; set; }

        public int ProjectIdFrom { get; set; }
        public string ProjectName_From { get; set; }
        public int UnitIdFrom { get; set; }
        public string? UnitName_from { get; set; }
        public int BlockIdFrom { get; set; }
        public string? BlockName_from { get; set; }
        public int FloorIdFrom { get; set; }
        public string? FloorName_From { get; set; }
        public int DivisionIdFrom { get; set; }
        public string? DivisionName_From { get; set; }
        public int ProjectIdTo { get; set; }
        public string? ProjectName_To { get; set; }
        public int UnitIdTo { get; set; }
        public string? UnitName_To { get; set; }
        public int BlockIdTo { get; set; }
        public string? BlockName_To { get; set; }
        public int FloorIdTo { get; set; }
        public string? FloorName_To { get; set; }
        public int DivisionIdTo { get; set; }
        public string? DivisionName_To { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public Int16 multicompany { get; set; }
        public Int16 ToCompany { get; set; }
        public string? CompanyName { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int Maxlevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 TransferStatusId { get; set; }

        public decimal TransportationCharge { get; set; }
        public decimal TransportationPer { get; set; }
        public decimal LoadingUnloadingCharge { get; set; }
        public decimal LoadingUnloadingPer { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal OtherChargesPer { get; set; }
        public decimal MiscellaneousExpense { get; set; }
        public decimal MiscellaneousExpensePer { get; set; }
        public decimal NetAmount { get; set; }
        public int? Category { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkNameId { get; set; }
        public int? ViewType { get; set; }
        public decimal? RoundOff { get; set; }
        public string? taxarea { get; set; }
        public string? Remarks { get; set; }
        public int SubcontractorId { get; set; }

        public int IsAsset { get; set; }
    }
}
