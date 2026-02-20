using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Debug;

namespace BuildExeServices.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectTypeId { get; set; }

        public int DepartmentId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public  DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public  string? LastName { get; set; }
        public string? Sex { get; set; }
        public string? GST_No { get; set; }
        public  DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Post { get; set; }
        public string? Pin { get; set; }
        public  string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public  decimal? TotalArea { get; set; }
        public decimal? RatePerArea { get; set; }
        public  decimal? TotalAmount { get; set; }
        public  int PaymentModeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int EnquiryId { get; set; }

        public int ScheduleType { get; set; }

        public bool IsWareHouse { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        //public GovernmentProject GovtProj { get; set; }
        public string? ProjectArea { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ClientUniqueName { get; set; }
        public string ContactPerson { get; set; }
        public string? LpoNo { get; set; }
        public DateTime? LpoDate { get; set; }

        public string ProjectInsurance  { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DLP {  get; set; }

        public DateTime DatePeriodFrom { get; set;}
        public DateTime DatePeriodTo { get; set; }

        //public string? OpeningType { get; set; }
        //public decimal? OpeningBalance { get; set; }

        //public int? FinancialYearId { get; set; }
    }

    //public class GovernmentProject
    //{
    //    public int GovtProjId { get; set; }
    //    public string GovtProjName { get; set; }
    //}
}
