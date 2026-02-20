using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class QuotationList
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuotationMasterId { get; set; }
        public string QuotationNumber { get; set; }
        public DateTime QuotationDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }

        public int FinancialYearId { get; set; }
        public Int16 IsDeleted { get; set; }
        public Int16 ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public String? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public Int16 IsReject { get; set; }
        public int Maxlevel { get; set; }


    }
}
