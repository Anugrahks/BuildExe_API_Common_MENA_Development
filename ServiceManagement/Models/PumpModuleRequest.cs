using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeServiceManagement.Models
{
    public class PumpModuleRequest
    {
        // ---------- MASTER ----------
        public int Id { get; set; }                 // For update
        public int EntryType { get; set; }
        public int? StockPointId { get; set; }

        public int SiteServiceType { get; set; }
        public string StockPoint { get; set; }
        public int JobNumber { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int ClientId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string StationLocation { get; set; }
        public string PumpRef { get; set; }
        public string Manufacturer { get; set; }
        public string PumpModel { get; set; }
        public string PumpSerialNo { get; set; }
        public string ElectricalSpec { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }


        public int UserId { get; set; }

        public int ApprovalLevel { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public string RejectRemarks { get; set; }
        public int ApprovalStatus { get; set; }
        public string SummaryAndRecommendation { get; set; }

        public DateTime WarrantyDate { get; set; }

        // ---------- CHILD TABLES ----------
        public List<PumpDetailModel> PumpDetails { get; set; }
        public List<SparePartModel> SpareParts { get; set; }
        public List<RepairActivityModel> RepairActivities { get; set; }
        public List<AssemblyCheckModel> AssemblyChecks { get; set; }

        public List<ElectricalChecks> ElectricalChecks { get; set; }

        public List<MechanicalChecks> MechanicalChecks { get; set; }
        public List<PumpDocumentModel> DocumentDetails { get; set; }
    }



    public class PumpDetailModel
    {
        public int SlNo { get; set; }
        public string InspectionDescription { get; set; }
        public string YorN { get; set; }
        public int NA { get; set; }
        public string Remarks { get; set; }
    }

    public class SparePartModel
    {
        public int SlNo { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public string Comments { get; set; }
        public string Activity { get; set; }
    }

    public class RepairActivityModel
    {
        public int SlNo { get; set; }
        public string ScopeOfWork { get; set; }
        public decimal Qty { get; set; }
        public string Comments { get; set; }
        public string Activity { get; set; }
    }

    public class AssemblyCheckModel
    {
        public int SlNo { get; set; }
        public string Description { get; set; }
        public string YorN { get; set; }
        public int NA { get; set; }
        public string Remarks { get; set; }
    }


    public class ElectricalChecks
    {
        public int SlNo { get; set; }
        public string ItemDescription { get; set; }
        public string YorN { get; set; }
        public int NA { get; set; }
        public string Observation { get; set; }
    }


    public class MechanicalChecks
    {
        public int SlNo { get; set; }
        public string ItemDescription { get; set; }
        public string YorN { get; set; }
        public int NA { get; set; }
        public string Observation { get; set; }
    }

    public class PumpDocumentModel
    {
        public int SlNo { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }   // Base64 / URL
    }

}