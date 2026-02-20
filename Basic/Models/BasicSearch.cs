using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class BasicSearch
    {
        [Required]
        public Int16 CompanyId { get; set; }
        [Required]
        public Int16 BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? Today {  get; set; }
        public int WithOpening { get; set; }
        public int? AccountHeadId { get; set; }
        public int? ClientId { get; set; }
        public int? SupplierId { get; set; }
        public int Department { get; set; }
        public int? EmployeeId { get; set; }
        public int? withDate { get; set; }
        public int? Category { get; set; }
        public int? CategoryId { get; set; }
        public int? IsDetail { get; set; }
        public int? ProjectStatus { get; set; }
        public int? Type { get; set; }
        public string? Particular { get; set; }
        public int? BlockId { get; set; }
        public int? FloorId { get; set; }
        public int? UnitId { get; set; }
        public int? WorkName { get; set; }
        public int WithStock { get; set; }

        public int CategoryWise { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? MonthId { get; set; }
        public int? VoucherNumber { get; set; }
        public int? AccountGroupId { get; set; }
        public int AccountSubGroupId { get; set; }
        public int siteUser { get; set; }
        public int siteUserId { get; set; }
        public int userId { get; set; }
        public int? MenuId { get; set; }
        public int EnquiryId { get; set; }
        public int IsLoan { get; set; }
        public int? FromProjectId {get; set; }
        public int? ToProjectId { get; set; }
        public string? HeadName { get; set; }
        public string? EmployeeName { get; set; }
        public string Form {  get; set; }
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int? ApprovalLevel { get; set; }
        public string? AccountGroupName { get; set; }
        public int? CustomerId { get; set; }

        public string CategoryIds { get; set; }

        public int IsVirtual { get; set; }

        public int Grouping { get; set; }
        public decimal? Retentionpercentage { get; set; }
        public string Label { get; set; }
        public string? Remarks { get; set; }

        public int ProjectWise {  get; set; }


        public int? Offset { get; set; }



        public int? PageSize { get; set; }


        public int Action { get; set; }

        public int WorkNameId { get; set; }

        public int WorkCategoryId { get; set; }

        public int? IsAllRnP { get; set; }


        public string? ClientName { get; set; }


        public int CreditHeadId { get; set; }


        public int DebitHeadId { get; set; }

        public int? StockPoint { get; set; }

        public int? MaterialId { get; set; }

        public int? MaterialTypeId { get; set; }
        public string? FormType { get; set; }

        public string? Ids { get; set; }

        [JsonProperty("BatchID")]
        public string? BatchId { get; set; }



    }
}
