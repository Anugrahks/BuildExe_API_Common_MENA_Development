using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class StaffTA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }
        public int ApprovalStatus { get; set; }
        public int ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }

        public string? ApprovalRemarks { get; set; }
        public string? RejectRemarks { get; set; }
        public int IsReject { get; set; }
        public int IsDeleted { get; set; }
        public decimal TotalAmt { get; set; }
        public List<StaffTADetails> StaffTADetails { get; set; }

    }
    public class StaffTADetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffTADetailsId { get; set; }
        public int MasterId { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public decimal TotalKm { get; set; }
        public int TATypeId { get; set; }
        public string TAType { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string DocumentName { get; set; }
        public string Document {  get; set; }
        public decimal TARate { get; set; }
        public decimal Total { get; set; }


    }

}
