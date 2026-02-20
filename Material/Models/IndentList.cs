using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeMaterialServices.Models
{
    public class IndentList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IndentTypeId { get; set; }
        public int IndentCategoryId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }

        public DateTime IndentedDate { get; set; }

        public int ApprovalStatus { get; set; }
        public string Remarks { get; set; }

        public int SupplierPreferred { get; set; }
        public string? SupplierName { get; set; }

        public int SubContractorId { get; set; }
        public string? FullName { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public int   Maxlevel { get; set; }
        public int MaterialTypeId { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? ViewType { get; set; }
        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }
        public int DivisionId { get; set; }
        public string? DivisionShortName { get; set; }
        public string? UserName { get; set; }
        public int? SlNo { get; set; }
        public int? MaterialCategoryId { get; set; }
        [NotMapped]
        public List<IndentDetailItems> IndentDetailItems { get; set; }

    }

    public class IndentDetailItems
    {
        public string? MaterialName { get; set; }
        public decimal QuantityRequired { get; set; }
        public DateTime? RequiredDate { get; set; }

    }

}
