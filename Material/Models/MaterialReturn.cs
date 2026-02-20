//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System;

//namespace BuildExeMaterialServices.Models
//{
//    public class MaterialReturn
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        public DateTime? ReturnDate { get; set; }
//        public int? IssueMasterId { get; set; }
//        public int? ReturnId { get; set; }
//        public int? DivisionId { get; set; }
//        public int? ProjectId { get; set; }
//        public int? BlockId { get; set; }
//        public int? FloorId { get; set; }
//        public int? UnitId { get; set; }
//        public int? TypeId { get; set; }
//        public int? ApprovalStatus { get; set; }
//        public int? ApprovalLevel { get; set; }
//        public short? ApprovedBy { get; set; }
//        public DateTime? ApprovedDate { get; set; }
//        [MaxLength(500)]
//        public string ApprovalRemarks { get; set; }
//        [MaxLength(500)]
//        public string RejectRemarks { get; set; }
//        public int? IsDeleted { get; set; }
//        public int? CompanyId { get; set; }
//        public int? BranchId { get; set; }
//        public int? FinancialYearId { get; set; }
//        public int? UserId { get; set; }
//        public DateTime? EnteredOnDate { get; set; }

//        // Navigation property for related details
//        public List<MaterialReturnDetail> MaterialReturnDetail { get; set; }
//        public List<MaterialReturnVirtualDetail> MaterialReturnVirtualDetail { get; set; }
//    }

//    public class MaterialReturnDetail
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int ReturnDetailId { get; set; }
//        public int ReturnMasterId { get; set; }
//        public int? MaterialId { get; set; }
//        public decimal? IssueQuantity { get; set; }
//        public decimal? BalanceQuantity { get; set; }
//        public decimal? ReturnQuantity { get; set; }
//        public decimal? ConsumptionQuantity { get; set; }
//    }

//    public class MaterialReturnVirtualDetail
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int ReturnVirtualDetailId { get; set; }
//        public int ReturnMasterId { get; set; }
//        public int? MaterialId { get; set; }
//        public int? NoOfDays { get; set; }
//        public decimal? UnitRate { get; set; }
//        public decimal? Amount { get; set; }
//    }
//} 





using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class MaterialReturn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? ReturnId { get; set; }
        public int? DivisionId { get; set; }
        public int? ProjectId { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? UnitId { get; set; }
        public int? TypeId { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public short? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [MaxLength(500)]
        public string ApprovalRemarks { get; set; }

        [MaxLength(500)]
        public string RejectRemarks { get; set; }
        public int? IsDeleted { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? UserId { get; set; }
        public int IsReject { get; set; }
        public int MaterialTypeId { get; set; }
        public DateTime? EnteredOnDate { get; set; }
        

        // Navigation properties
        public List<MaterialReturnDetail> MaterialReturnDetail { get; set; }
        public List<MaterialReturnVirtualDetail> MaterialReturnVirtualDetail { get; set; }
    }

    public class MaterialReturnDetail
    {
        public int ReturnMasterId { get; set; }
        public int? IssueMasterId { get; set; }
        public int MaterialId { get; set; }
        public decimal IssueQuantity { get; set; }
        public decimal BalanceQuantity { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal ConsumptionQuantity { get; set; }
        public int? TransferStageId { get; set; }
        public decimal TransferQuantity { get; set; }
        public int? IssueDetailId { get; set; }
        public int? EmployeeId { get; set; }
        public string StageName { get; set; }
        public int? OrderId { get; set; }
        public string TransferStageName { get; set; }
        public int? TransferOrderId { get; set; }
        public int IsTransfered { get; set; }

        public int IssueId { get; set; }

        public decimal CoefficientFactorValue { get; set; }

        public decimal ConversionQuantity { get; set; }
        public string ConversionUnitName { get; set; }

    }

    public class MaterialReturnVirtualDetail
    {
        public int ReturnMasterId { get; set; }
        public int MaterialId { get; set; }
        public int NoOfDays { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Amount { get; set; }
        public int IssueMasterId { get; set; }
        public int IssueDetailId { get; set; }
        public int IsTransfered { get; set; }
    }
}


