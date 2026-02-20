using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ProjectList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectTypeId { get; set; }
        public string ProjectType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentShortName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string GST_No { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Post { get; set; }
        public string Pin { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public decimal TotalArea { get; set; }
        public decimal RatePerArea { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentModeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int EnquiryId { get; set; }
        public int ScheduleType { get; set; }
        public bool IsWareHouse { get; set; }
        public int IsBlockFloorExists { get; set; }

        //public string ClientName { get; set; }
    }
}
