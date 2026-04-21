using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BuildExeMaterialServices.Models
{
    [Keyless]
    public class MaterialSearch
    {
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ProjectId { get; set; }
        public int? FromProjectId { get; set; }
        public int? ToProjectId { get; set; }

        public int? DivisionId { get; set; }
        public int? DivisionIdFrom { get; set; }
        public int? DivisionIdTo { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? ReturnFromDate { get; set; }
        public DateTime? ReturnToDate { get; set; }

        public DateTime? UpToDate { get; set; }
        public int? ApprovalStatus { get; set; }
        public int?  PurchaseFlag { get; set; }
        public int? PurchaseReturnType { get; set; }

        public int? CategoryId { get; set; }
        public int?  MaterialCategoryId { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? MaterialBrandId { get; set; }
        public int? TrasnsferStatusId { get; set; }
        public string? MaterialId { get; set; }
        public int? WorkCategory { get; set; }

        public int? WorkCategoryId { get; set; }

        public int? WorkNameId { get; set; }

        
        public int? ItemId { get; set; }
        public int? SupplierId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ViewType { get; set; }
        public int? MenuId { get; set; }
        public int? Id { get; set; }
        public int? IndentTypeId { get; set; }
        public int? ReportId { get; set; }

        public string? PaymentMode { get; set; }
        public string? InvoiceNo { get; set; }
        public int? TypeId { get; set; }

        public int? SubcontractorId { get; set; }

        public string PurchaseIds { get; set; }
        public string Status { get; set; }

        public string PONo { get; set; }

        public int? UnitId { get; set; }

        public int? StockPoint { get; set; }

        public int? CustomerId { get; set; }

        public string? ReferenceNo { get; set; }


        public int ActionButton { get; set; }
        public string? MaterialFor { get; set; }
        public string? InstallationType { get; set; }
        public string? Model { get; set; }
        public string? Version { get; set; }
        public string? Drive { get; set; }
        public string? Head { get; set; }
        public string? Curve { get; set; }
        public string? Power { get; set; }
        public string? Voltage { get; set; }
        public string? Frequency { get; set; }




    }
}
