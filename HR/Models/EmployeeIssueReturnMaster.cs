using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Models
{

    [Keyless]
    public class EmployeeIssueReturnMaster
    {


        public int Id { get; set; }
        public int EntryType { get; set; }
        public DateTime EntryDate { get; set; }

        public string? EmployeeName { get; set; }
        public int EmployeeId { get; set; }

        public int ProjectIdMaster { get; set; }
        public string ProjectNameMaster { get; set; } 

        public int DivisionIdMaster { get; set; }
        public string DivisionNameMaster { get; set; } 

        public string? ReferenceNo { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }

        public int ApprovalLevel { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }

        public int IsReject { get; set; }
        public int UserId { get; set; }

        public string? Remarks { get; set; }


        public List<EmployeeIssueReturnDetails> EmployeeIssueReturnDetails { get; set; }



    }



    [Keyless]
    public class EmployeeIssueReturnDetails
    {
        public int DetailId { get; set; }
        public int IssueMasterId { get; set; }

        public DateTime IssueDate { get; set; }

        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }

        public string? QrCode { get; set; }
        public string? BarCode { get; set; }

        public int ItemId { get; set; }
        public int AssetMasterId { get; set; }
        public int AssetQuantityNumber { get; set; }

        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }

        public int DivisionId { get; set; }
        public string? DivisionName { get; set; }


        public int IsDeleted { get; set; }

    }




}
