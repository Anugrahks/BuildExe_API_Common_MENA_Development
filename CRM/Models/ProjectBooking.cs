using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class ProjectBooking
    {
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        // public int OwnProjectId { get; set; }
        public string? UnitId { get; set; }
        public int? EnquiryId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Post { get; set; }
        public string? Pin { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string?  GST_No { get; set; }
        public int? PaymentModeId { get; set; }

        //---------------------------------
        public string? CustomerID { get; set; }
        public string? Sonof { get; set; }
        public string? SpouseName { get; set; }
      
        public string? PanNO { get; set; }
        public string? AdharNo { get; set; }
        public string? MaritalStatus { get; set; }
        public DateTime? WeddingAnniversary { get; set; }

        public string? Nationality { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? HouseName { get; set; }


        public string? Permanentaddress { get; set; }
        public string? Permanentdistrict { get; set; }
        public string? PermanentState { get; set; }
        public string? PermanentPin { get; set; }
        public string? villege { get; set; }
        public string? Amsom { get; set; }
        public string? desom { get; set; }
        public string? Taluk { get; set; }


        public string? NameOfPowerofattorny { get; set; }
        public string? ReidentialStatus { get; set; }

        public string? NameOfOrganization { get; set; }
        public string? Addressoforganization { get; set; }

        public string? OrganizationType { get; set; }
        public string?  ClientImage { get; set; }
        
        //------------------------------------
       // public decimal TotalAmount { get; set; }
       // public decimal RatePerArea { get; set; }
      //  public decimal TerraceRate { get; set; }
      //  public decimal LandRate { get; set; }
      //  -----------------------------------------------
        public decimal TotalAmount { get; set; }
        public decimal CarpetArea { get; set; }
        public decimal CommonArea { get; set; }
        public decimal BalconyArea { get; set; }
        public decimal WorkArea { get; set; }
        public decimal RatePerArea { get; set; }
        public decimal PrivateTerrace { get; set; }

        public decimal TerraceRate { get; set; }
        public decimal CarParking { get; set; }
        public decimal gst { get; set; }
        public decimal kfc { get; set; }

        public decimal LandCost { get; set; }
        public decimal LandRate { get; set; }
        public decimal landgst { get; set; }
        public decimal landkfc { get; set; }
       // ------------------------------------------
        public decimal ClientAmount { get; set; }
        public decimal ClientPer { get; set; }
        public decimal BankAmount { get; set; }
        public decimal BankPer { get; set; }
        //-----------------------------------


        public DateTime?  BookingDate { get; set; }
        public decimal? BookingAmount { get; set; }
        public int? BankId { get; set; }
        public string? BookingAmountType { get; set; }
        public string? Chequeno { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? VoucherNumber { get; set; }
        public Int16? FinancialYearId { get; set; }

        //-------------------------------------
        public string? ClientName { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? AccountNo { get; set; }
        public string? IFSCCode { get; set; }

        //----------------------------------
        public DateTime? ApplicationDate { get; set; }
        public string? ApplicationNo { get; set; }
        public string? BasementNo { get; set; }
        public string? SlotNo { get; set; }
        //----------------------------------

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }

        public int? SlNo { get; set; }
        public string? Block { get; set; }
        public string? Floor { get; set; }
    }
}
