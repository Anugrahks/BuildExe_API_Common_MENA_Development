using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class EmployeeJoiningList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int JoiningType { get; set; }
        public string? JoiningTypeName { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? VisaDesignation { get; set; }
        public decimal VisaCharge { get; set; }
        public DateTime VisaChargeFrom { get; set; }
        public DateTime VisaChargeTo { get; set; }
        public decimal LeaveSalary { get; set; }
        public DateTime LeaveSalaryFrom { get; set; }
        public DateTime LeaveSalaryTo { get; set; }
        public decimal Gratuity { get; set; }
        public DateTime GratuityFrom { get; set; }
        public DateTime GratuityTo { get; set; }
        public decimal Gratuity2 { get; set; }

        public DateTime Gratuity2From { get; set; }
        public DateTime Gratuity2To { get; set; }

        public Decimal? LeaveSalary_CalculatedOn { get; set; }
        public Decimal? Gratuity1_CalculatedOn { get; set; }
        public Decimal? Gratuity2_CalculatedOn { get; set; }




        public decimal Insurance { get; set; }
        public DateTime InsuranceFrom { get; set; }
        public DateTime InsuranceTo { get; set; }
        public decimal Airticket { get; set; }
        public DateTime AirticketFrom { get; set; }
        public DateTime AirticketTo { get; set; }

        public DateTime? RejoiningDate { get; set; }
        public DateTime? ApprovedLeaveFrom { get; set; }
        public DateTime? ApprovedLeaveTo { get; set; }
        public int? ApprovedNumberOfDays { get; set; }

        




        public int ApprovalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
        public int ApprovalLevel { get; set; }
        public int IsDeleted { get; set; }
        public string? ApprovalRemarks { get; set; }
        public int IsReject { get; set; }
        public int Maxlevel { get; set; }
        public int? viewType { get; set; }
    }
}
