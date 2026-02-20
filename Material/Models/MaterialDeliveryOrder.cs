using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeMaterialServices.Models
{
    public class MaterialDeliveryOrderMaster
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

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public DateTime? DateOrdered { get; set; }
        public int? DeliveryNo { get; set; }

        public string ReferenceNo { get; set; }
        public string FinalDesignation { get; set; }
        public string ModeOfTransport { get; set; }

        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }

        public int ApprovalLevel { get; set; } = 0;
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; } = 0;

        public int UserId { get; set; } = 0;
        public DateTime? EnteredOnDate { get; set; }

        public string RejectRemarks { get; set; }
        public int ApprovalStatus { get; set; } = 0;

        // Child Collections (details)
        public List<MaterialDeliveryOrderDetails> MaterialDeliveryOrderDetails { get; set; }
    }


    public class MaterialDeliveryOrderDetails
    {
        public int DeliveryOrderDetailId { get; set; }
        public int? DeliveryOrderId { get; set; }

        public string? ScheduleNo { get; set; }

        public int? SpecId { get; set; }
        public string SpecName { get; set; }

        public string SpecDescription { get; set; }

        public decimal OrderedQuantity { get; set; }
        public decimal? DeliveredQuantity { get; set; }
        public decimal? BalanceQuantity { get; set; }

        public decimal? InputtedQuantity { get; set; }

        public decimal? Rate { get; set; }

        public int ScheduleUniqueId { get; set; }
    }

}