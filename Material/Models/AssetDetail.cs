using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class AssetDetailEntryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int PurchaseId { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public string InvoiceNo { get; set; } = "";
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ApprovedBy { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalLevel { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; } = 0;
        public int UserId { get; set; }

        public int ProjectId { get; set; } = 0;
        public string ProjectName { get; set; } = string.Empty;

        public int DivisionId { get; set; } = 0;
        public string DivisionName { get; set; } = string.Empty;

        public List<AssetDetailEntryDetail> AssetDetailEntryDetail { get; set; }

    }

    public class AssetDetailEntryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }

        public int MaterialId { get; set; } = 0;
        public string MaterialName { get; set; } = string.Empty;
        public int UnitId { get; set; } = 0;
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 0;
        public string? AssetName { get; set; }
        public string? Description { get; set; }
        public string? AssetCategory { get; set; }
        public string? ModelMake { get; set; }
        public string? ManufactorerName { get; set; }
        public string? WarrantyDetails { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? ExpectedLifespan { get; set; }
        public string? ServiceProviderDetails { get; set; }
        public string? MaintenanceInterval { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public string? InsuranceCoverageType { get; set; }
        public DateTime? InsuranceExpirationDate { get; set; }
        public string? SoftwareLicenseKeys { get; set; }
        public DateTime? SoftwareRenewalDate { get; set; }
        public string? SoftwareVersion { get; set; }

        public List<AssetDetailEntryRegulatoryCompliance> AssetDetailEntryRegulatoryCompliance { get; set; }
        public List<AssetMaterialCode> AssetMaterialCode { get; set; }

        public List<AssetDocumentDetails> AssetDocumentDetails { get; set; }

    }

    public class AssetDetailEntryRegulatoryCompliance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int MaterialId { get; set; } = 0;
        public string? ComplianceCertficate { get; set; }
        public string? ComplianceLicense { get; set; }

    }

    public class AssetMaterialCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int MaterialId { get; set; } = 0;
        public int QuantityNumber { get; set; } = 0;
        public string MaterialCode { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public decimal MaterialCodeDecimal { get; set; } = 0;

        public int IsAccount { get; set; } = 0;

    }

    public class AssetDocumentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public int MaterialId { get; set; } = 0;
        public string DocumentName { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public DateTime Expiry { get; set; } 

    }
}
