using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class Indent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IndentTypeId { get; set; }
        public int IndentCategoryId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }

        public DateTime IndentedDate { get; set; }

        public int ApprovalStatus { get; set; }
        public string Remarks { get; set; }

        public int SupplierPreferred { get; set; }

        public int SubContractorId { get; set; }

        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovedBy { get; set; }

        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int? WorkCategoryId { get; set; }
        public int? WorkNameId { get; set; }

        public Int16 UserId { get; set; }

        public int FinancialYearId { get; set; }
        public List<IndentDetails> IndentDetails { get; set; }
    }
    public class IndentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IndentDetailsId { get; set; }
        public int IndentId { get; set; }
        public int MaterialId { get; set; }
        public int WorkId { get; set; }

        public decimal QuantityRequired { get; set; }
        public DateTime? RequiredDate { get; set; }
        public decimal QuantityOrdered { get; set; }
        public int PurchaseFlag { get; set; }

    }
}
