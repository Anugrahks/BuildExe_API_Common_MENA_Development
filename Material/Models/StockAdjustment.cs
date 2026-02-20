using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeMaterialServices.Models
{
    public class StockAdjustment
    {
        public int Id { get; set; }

        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }

        public int? UnitId { get; set; }
        public string UnitName { get; set; }

        public int? BlockId { get; set; }
        public string BlockName { get; set; }

        public int? FloorId { get; set; }
        public string FloorName { get; set; }

        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int? MaterialId { get; set; }
        public string MaterialName { get; set; }

        public decimal StockIn { get; set; } = 0;
        public decimal StockOut { get; set; } = 0;

        public DateTime? TrDate { get; set; }
        public string Remarks { get; set; }

        public int IsDeleted { get; set; } = 0;

        public int ApprovalStatus { get; set; } = 0;
        public int ApprovalLevel { get; set; } = 0;
        public int IsReject { get; set; } = 0;

        public string ApprovalRemarks { get; set; }
        public string RejectRemarks { get; set; }

        public int CompanyId { get; set; } = 0;
        public int BranchId { get; set; } = 0;
        public int FinancialYearId { get; set; } = 0;
        public int UserId { get; set; } = 0;

        public int MaterialTypeId { get; set; } = 0;

    }

}