using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ProjectBookingRepository:IProjectBookingRepository 
    {
        private readonly ProductContext _dbContext;

        public ProjectBookingRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            SelectForReport = 6,
            Selectbasedoncompany = 7
        }

        public static Object SafeDbObject(Object input)
        {
            if (input == null)
            {
                return DBNull.Value;
            }
            else
            {
                return input;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(ProjectBooking projectBooking)
        {
            try
            {

                if (projectBooking.Chequeno == null)
                    projectBooking.Chequeno = "";
                if (projectBooking.BankId == null)
                    projectBooking.BankId = 0;


                //var id = new SqlParameter("@id", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", projectBooking.Id);
                var ProjectId = new SqlParameter("@ProjectId", projectBooking.ProjectId);
               
                var EnquiryId = new SqlParameter("@EnquiryId", SafeDbObject(projectBooking.EnquiryId));
                var ClientId = new SqlParameter("@ClientId", projectBooking.ClientId);

                var FirstName = new SqlParameter("@FirstName", projectBooking.FirstName);
                var LastName = new SqlParameter("@LastName", projectBooking.LastName);
                var Sex = new SqlParameter("@Sex", SafeDbObject(projectBooking.Sex));
                var DateOfBirth = new SqlParameter("@DateOfBirth", SafeDbObject(projectBooking.DateOfBirth));
                var Address = new SqlParameter("@Address", SafeDbObject(projectBooking.Address));
                var Post = new SqlParameter("@Post", SafeDbObject(projectBooking.Post));
                var Pin = new SqlParameter("@Pin", SafeDbObject(projectBooking.Pin));
                var PhoneNumber = new SqlParameter("@PhoneNumber", SafeDbObject(projectBooking.PhoneNumber));
                var MobileNumber = new SqlParameter("@MobileNumber", SafeDbObject(projectBooking.MobileNumber));
                var EmailId = new SqlParameter("@EmailId", SafeDbObject(projectBooking.EmailId));

                //-----------------------------------
                var CustomerID = new SqlParameter("@CustomerID", SafeDbObject(projectBooking.CustomerID));
                var Sonof = new SqlParameter("@Sonof", SafeDbObject(projectBooking.Sonof));
                var SpouseName = new SqlParameter("@SpouseName", SafeDbObject(projectBooking.SpouseName));

                var PanNO = new SqlParameter("@PanNO", SafeDbObject(projectBooking.PanNO));
                var AdharNo = new SqlParameter("@AdharNo", SafeDbObject(projectBooking.AdharNo));
                var MaritalStatus = new SqlParameter("@MaritalStatus", SafeDbObject(projectBooking.MaritalStatus));
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", SafeDbObject(projectBooking.WeddingAnniversary));

                var Nationality = new SqlParameter("@Nationality", SafeDbObject(projectBooking.Nationality));
                var State = new SqlParameter("@State", SafeDbObject(projectBooking.State));
                var District = new SqlParameter("@District", SafeDbObject(projectBooking.District));
                var HouseName = new SqlParameter("@HouseName", SafeDbObject(projectBooking.HouseName));

                var Permanentaddress = new SqlParameter("@Permanentaddress", SafeDbObject(projectBooking.Permanentaddress));
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", SafeDbObject(projectBooking.Permanentdistrict));
                var PermanentState = new SqlParameter("@PermanentState", SafeDbObject(projectBooking.PermanentState));
                var PermanentPin = new SqlParameter("@PermanentPin", SafeDbObject(projectBooking.PermanentPin));
                var villege = new SqlParameter("@villege", SafeDbObject(projectBooking.villege));
                var Amsom = new SqlParameter("@Amsom", SafeDbObject(projectBooking.Amsom));
                var desom = new SqlParameter("@desom", SafeDbObject(projectBooking.desom));
                var Taluk = new SqlParameter("@Taluk", SafeDbObject(projectBooking.Taluk));

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", SafeDbObject(projectBooking.NameOfPowerofattorny));
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", SafeDbObject(projectBooking.ReidentialStatus));
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", SafeDbObject(projectBooking.NameOfOrganization));
                var Addressoforganization = new SqlParameter("@Addressoforganization", SafeDbObject(projectBooking.Addressoforganization));
                var OrganizationType = new SqlParameter("@OrganizationType", SafeDbObject(projectBooking.OrganizationType));
                var ClientImage = new SqlParameter("@ClientImage", SafeDbObject(projectBooking.ClientImage));

                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount",projectBooking.TotalAmount);
                var CarpetArea = new SqlParameter("@CarpetArea", projectBooking.CarpetArea);
                var CommonArea = new SqlParameter("@CommonArea", projectBooking.CommonArea);
                var BalconyArea = new SqlParameter("@BalconyArea", projectBooking.BalconyArea);
                var WorkArea = new SqlParameter("@WorkArea", projectBooking.WorkArea);
                var RatePerArea = new SqlParameter("@RatePerArea", projectBooking.RatePerArea);
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", projectBooking.PrivateTerrace);
                var TerraceRate = new SqlParameter("@TerraceRate", projectBooking.TerraceRate);
                var CarParking = new SqlParameter("@CarParking", projectBooking.CarParking);
                var gst = new SqlParameter("@gst", projectBooking.gst);
                var kfc = new SqlParameter("@kfc", projectBooking.kfc);
                var LandCost = new SqlParameter("@LandCost", projectBooking.LandCost);
                var LandRate = new SqlParameter("@LandRate", projectBooking.LandRate);
                var landgst = new SqlParameter("@landgst", projectBooking.landgst);
                var landkfc = new SqlParameter("@landkfc", projectBooking.landkfc);
              

                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", projectBooking.ClientAmount);
                var ClientPer = new SqlParameter("@ClientPer", projectBooking.ClientPer);
                var BankAmount = new SqlParameter("@BankAmount", projectBooking.BankAmount);
                var BankPer = new SqlParameter("@BankPer", projectBooking.BankPer);
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", SafeDbObject(projectBooking.BookingDate));
                var BookingAmount = new SqlParameter("@BookingAmount", SafeDbObject(projectBooking.BookingAmount));
                var BankId = new SqlParameter("@BankId", SafeDbObject(projectBooking.BankId));
                var BookingAmountType = new SqlParameter("@BookingAmountType", SafeDbObject(projectBooking.BookingAmountType));
                var Chequeno = new SqlParameter("@Chequeno", SafeDbObject(projectBooking.Chequeno));
                var FinancialYearId = new SqlParameter("@FinancialYearId", projectBooking.FinancialYearId);
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", SafeDbObject(projectBooking.ClientName));
                var BankName = new SqlParameter("@BankName", SafeDbObject(projectBooking.BankName));
                var Branch = new SqlParameter("@Branch", SafeDbObject(projectBooking.Branch));
                var AccountNo = new SqlParameter("@AccountNo", SafeDbObject(projectBooking.AccountNo));
                var IFSCCode = new SqlParameter("@IFSCCode", SafeDbObject(projectBooking.IFSCCode));
                
                  
                //--------------------------------------
                var CompanyId = new SqlParameter("@CompanyId", projectBooking.CompanyId);
                var BranchId = new SqlParameter("@BranchId", projectBooking.BranchId);
                var UserId = new SqlParameter("@UserId", projectBooking.UserId);

                var GST_No = new SqlParameter("@GST_No", SafeDbObject(projectBooking.GST_No));
                var PaymentModeId = new SqlParameter("@PaymentModeId", SafeDbObject(projectBooking.PaymentModeId));
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", SafeDbObject(projectBooking.ApplicationDate));
                var ApplicationNo = new SqlParameter("@ApplicationNo", SafeDbObject(projectBooking.ApplicationNo));
                var BasementNo = new SqlParameter("@BasementNo", SafeDbObject(projectBooking.BasementNo));
                var SlotNo = new SqlParameter("@SlotNo", SafeDbObject(projectBooking.SlotNo) );

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validation.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();

                // _dbContext.Database.ExecuteSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CoApplicantName, @CoApplicantAddress, @Relationship,@CoApplicantSex,@coapplicantDateOfBirth, @CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId,@Action ", OwnProjectId, ProjectId,  EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CoApplicantName, CoApplicantAddress, Relationship, CoApplicantSex, coapplicantDateOfBirth, CompanyId, BranchId, UserId, GST_No, PaymentModeId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(ProjectBooking projectBooking)
        {
            try
            {
                if (projectBooking.Chequeno == null)
                    projectBooking.Chequeno = "";
                if (projectBooking.BankId == null)
                    projectBooking.BankId = 0;

                //var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectBooking.ProjectId);
                var OwnProjectId = new SqlParameter("@OwnProjectId", projectBooking.Id);
                var EnquiryId = new SqlParameter("@EnquiryId", SafeDbObject(projectBooking.EnquiryId));
                var ClientId = new SqlParameter("@ClientId", projectBooking.ClientId);

                var FirstName = new SqlParameter("@FirstName", projectBooking.FirstName);
                var LastName = new SqlParameter("@LastName", projectBooking.LastName);
                var Sex = new SqlParameter("@Sex", SafeDbObject(projectBooking.Sex));
                var DateOfBirth = new SqlParameter("@DateOfBirth", SafeDbObject(projectBooking.DateOfBirth));
                var Address = new SqlParameter("@Address", SafeDbObject(projectBooking.Address));
                var Post = new SqlParameter("@Post", SafeDbObject(projectBooking.Post));
                var Pin = new SqlParameter("@Pin", SafeDbObject(projectBooking.Pin));
                var PhoneNumber = new SqlParameter("@PhoneNumber", SafeDbObject(projectBooking.PhoneNumber));
                var MobileNumber = new SqlParameter("@MobileNumber", SafeDbObject(projectBooking.MobileNumber));
                var EmailId = new SqlParameter("@EmailId", SafeDbObject(projectBooking.EmailId));

                //-----------------------------------
                var CustomerID = new SqlParameter("@CustomerID", projectBooking.CustomerID);
                var Sonof = new SqlParameter("@Sonof", SafeDbObject(projectBooking.Sonof));
                var SpouseName = new SqlParameter("@SpouseName", SafeDbObject(projectBooking.SpouseName));

                var PanNO = new SqlParameter("@PanNO", SafeDbObject(projectBooking.PanNO));
                var AdharNo = new SqlParameter("@AdharNo", SafeDbObject(projectBooking.AdharNo));
               var MaritalStatus = new SqlParameter("@MaritalStatus", SafeDbObject(projectBooking.MaritalStatus));
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", SafeDbObject(projectBooking.WeddingAnniversary));

                var Nationality = new SqlParameter("@Nationality", SafeDbObject(projectBooking.Nationality));
                var State = new SqlParameter("@State", SafeDbObject(projectBooking.State));
                var District = new SqlParameter("@District", SafeDbObject(projectBooking.District));
                var HouseName = new SqlParameter("@HouseName", SafeDbObject(projectBooking.HouseName));

                var Permanentaddress = new SqlParameter("@Permanentaddress", SafeDbObject(projectBooking.Permanentaddress));
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", SafeDbObject(projectBooking.Permanentdistrict));
                var PermanentState = new SqlParameter("@PermanentState", SafeDbObject(projectBooking.PermanentState));
                var PermanentPin = new SqlParameter("@PermanentPin", SafeDbObject(projectBooking.PermanentPin)); 
                var villege = new SqlParameter("@villege", SafeDbObject(projectBooking.villege));
                var Amsom = new SqlParameter("@Amsom", SafeDbObject(projectBooking.Amsom));
                var desom = new SqlParameter("@desom", SafeDbObject(projectBooking.desom));
                var Taluk = new SqlParameter("@Taluk", SafeDbObject(projectBooking.Taluk));

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", SafeDbObject(projectBooking.NameOfPowerofattorny));
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", SafeDbObject(projectBooking.ReidentialStatus));
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", SafeDbObject(projectBooking.NameOfOrganization));
                var Addressoforganization = new SqlParameter("@Addressoforganization", SafeDbObject(projectBooking.Addressoforganization));
                var OrganizationType = new SqlParameter("@OrganizationType", SafeDbObject(projectBooking.OrganizationType));
                var ClientImage = new SqlParameter("@ClientImage", SafeDbObject(projectBooking.ClientImage));

                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", projectBooking.TotalAmount);
                var CarpetArea = new SqlParameter("@CarpetArea", projectBooking.CarpetArea);
                var CommonArea = new SqlParameter("@CommonArea", projectBooking.CommonArea);
                var BalconyArea = new SqlParameter("@BalconyArea", projectBooking.BalconyArea);
                var WorkArea = new SqlParameter("@WorkArea", projectBooking.WorkArea);
                var RatePerArea = new SqlParameter("@RatePerArea", projectBooking.RatePerArea);
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", projectBooking.PrivateTerrace);
                var TerraceRate = new SqlParameter("@TerraceRate", projectBooking.TerraceRate);
                var CarParking = new SqlParameter("@CarParking", projectBooking.CarParking);
                var gst = new SqlParameter("@gst", projectBooking.gst);
                var kfc = new SqlParameter("@kfc", projectBooking.kfc);
                var LandCost = new SqlParameter("@LandCost", projectBooking.LandCost);
                var LandRate = new SqlParameter("@LandRate", projectBooking.LandRate);
                var landgst = new SqlParameter("@landgst", projectBooking.landgst);
                var landkfc = new SqlParameter("@landkfc", projectBooking.landkfc);


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", projectBooking.ClientAmount);
                var ClientPer = new SqlParameter("@ClientPer", projectBooking.ClientPer);
                var BankAmount = new SqlParameter("@BankAmount", projectBooking.BankAmount);
                var BankPer = new SqlParameter("@BankPer", projectBooking.BankPer);
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", SafeDbObject(projectBooking.BookingDate));
                var BookingAmount = new SqlParameter("@BookingAmount", SafeDbObject(projectBooking.BookingAmount));
                var BankId = new SqlParameter("@BankId", SafeDbObject(projectBooking.BankId));
                var BookingAmountType = new SqlParameter("@BookingAmountType", SafeDbObject(projectBooking.BookingAmountType));
                var Chequeno = new SqlParameter("@Chequeno", SafeDbObject(projectBooking.Chequeno));
                var FinancialYearId = new SqlParameter("@FinancialYearId", projectBooking.FinancialYearId);
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", SafeDbObject(projectBooking.ClientName));
                var BankName = new SqlParameter("@BankName", SafeDbObject(projectBooking.BankName));
                var Branch = new SqlParameter("@Branch", SafeDbObject(projectBooking.Branch));
                var AccountNo = new SqlParameter("@AccountNo", SafeDbObject(projectBooking.AccountNo));
                var IFSCCode = new SqlParameter("@IFSCCode", SafeDbObject(projectBooking.IFSCCode));

                //--------------------------------------


                var CompanyId = new SqlParameter("@CompanyId", projectBooking.CompanyId);
                var BranchId = new SqlParameter("@BranchId", projectBooking.BranchId);
                var UserId = new SqlParameter("@UserId", projectBooking.UserId);

                var GST_No = new SqlParameter("@GST_No", SafeDbObject(projectBooking.GST_No));
                var PaymentModeId = new SqlParameter("@PaymentModeId", SafeDbObject(projectBooking.PaymentModeId));
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", SafeDbObject(projectBooking.ApplicationDate));
                var ApplicationNo = new SqlParameter("@ApplicationNo", SafeDbObject(projectBooking.ApplicationNo));
                var BasementNo = new SqlParameter("@BasementNo", SafeDbObject(projectBooking.BasementNo));
                var SlotNo = new SqlParameter("@SlotNo", SafeDbObject(projectBooking.SlotNo));

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validation.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();

                //  _dbContext.Database.ExecuteSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CoApplicantName, @CoApplicantAddress, @Relationship,@CoApplicantSex,@coapplicantDateOfBirth, @CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CoApplicantName, CoApplicantAddress, Relationship, CoApplicantSex, coapplicantDateOfBirth, CompanyId, BranchId, UserId, GST_No, PaymentModeId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectBooking>> Get()
        {
            try
            {
                //  var id = new SqlParameter("@id", Id);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");
                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID","");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");

                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectBooking>> Get(int companyId,int branchid)
        {
            try
            {
                //  var id = new SqlParameter("@id", Id);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");
                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");

                //---------------------------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");
               
               //-----------------------------------------------------------------
               var Action = new SqlParameter("@Action", Actions.Selectbasedoncompany);
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBooking>> Getuser(int companyId, int branchid, int UserId)
        {
            try
            {
                //  var id = new SqlParameter("@id", Id);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");
                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");

                //---------------------------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------

                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", UserId);

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.Selectbasedoncompany);
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, Userid, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectBooking >> GetByID(int Id)
        {
            try
            {
              
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", Id);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");


                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");
                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.Select );
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
            public async Task<IEnumerable<Validation>> Delete(int Id, int userID)
        {
            try
            {
              //  var id = new SqlParameter("@id", Id);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var OwnProjectId = new SqlParameter("@OwnProjectId", Id);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");

                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");
                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userID);

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectBooking>> GetForReport(int Id)
        {
            try
            {

                var ProjectId = new SqlParameter("@ProjectId", Id);
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");


                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");
                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.SelectForReport);
                var _product =await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBooking>> GetForReport(int companyId, int branchId, int Id)
        {
            try
            {

                var ProjectId = new SqlParameter("@ProjectId", Id);
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");


                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");
                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var UserId = new SqlParameter("@UserId", "0");

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", Actions.SelectForReport);
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBooking>> GetForReportWithId(int companyId, int branchId, int Id, int ReportId)
        {
            try
            {

                var ProjectId = new SqlParameter("@ProjectId", Id);
                var OwnProjectId = new SqlParameter("@OwnProjectId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ClientId = new SqlParameter("@ClientId", "0");

                var FirstName = new SqlParameter("@FirstName", "0");
                var LastName = new SqlParameter("@LastName", "0");
                var Sex = new SqlParameter("@Sex", "0");
                var DateOfBirth = new SqlParameter("@DateOfBirth", "2020-01-01");
                var Address = new SqlParameter("@Address", "0");
                var Post = new SqlParameter("@Post", "0");
                var Pin = new SqlParameter("@Pin", "0");
                var PhoneNumber = new SqlParameter("@PhoneNumber", "0");
                var MobileNumber = new SqlParameter("@MobileNumber", "0");
                var EmailId = new SqlParameter("@EmailId", "0");


                //---------------------------------------
                var CustomerID = new SqlParameter("@CustomerID", "");
                var Sonof = new SqlParameter("@Sonof", "");
                var SpouseName = new SqlParameter("@SpouseName", "");

                var PanNO = new SqlParameter("@PanNO", "");
                var AdharNo = new SqlParameter("@AdharNo", "");
                var MaritalStatus = new SqlParameter("@MaritalStatus", "");
                var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", "2020-01-01");

                var Nationality = new SqlParameter("@Nationality", "");
                var State = new SqlParameter("@State", "");
                var District = new SqlParameter("@District", "");
                var HouseName = new SqlParameter("@HouseName", "");

                var Permanentaddress = new SqlParameter("@Permanentaddress", "");
                var Permanentdistrict = new SqlParameter("@Permanentdistrict", "");
                var PermanentState = new SqlParameter("@PermanentState", "");
                var PermanentPin = new SqlParameter("@PermanentPin", "");
                var villege = new SqlParameter("@villege", "");
                var Amsom = new SqlParameter("@Amsom", "");
                var desom = new SqlParameter("@desom", "");
                var Taluk = new SqlParameter("@Taluk", "");

                var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", "");
                var ReidentialStatus = new SqlParameter("@ReidentialStatus", "");
                var NameOfOrganization = new SqlParameter("@NameOfOrganization", "");
                var Addressoforganization = new SqlParameter("@Addressoforganization", "");
                var OrganizationType = new SqlParameter("@OrganizationType", "");
                var ClientImage = new SqlParameter("@ClientImage", "");
                //---------------------------------------
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var CarpetArea = new SqlParameter("@CarpetArea", "0");
                var CommonArea = new SqlParameter("@CommonArea", "0");
                var BalconyArea = new SqlParameter("@BalconyArea", "0");
                var WorkArea = new SqlParameter("@WorkArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var PrivateTerrace = new SqlParameter("@PrivateTerrace", "0");
                var TerraceRate = new SqlParameter("@TerraceRate", "0");
                var CarParking = new SqlParameter("@CarParking", "0");
                var gst = new SqlParameter("@gst", "0");
                var kfc = new SqlParameter("@kfc", "0");
                var LandCost = new SqlParameter("@LandCost", "0");
                var LandRate = new SqlParameter("@LandRate", "0");
                var landgst = new SqlParameter("@landgst", "0");
                var landkfc = new SqlParameter("@landkfc", "0");


                //---------------------------------------------------------------------
                var ClientAmount = new SqlParameter("@ClientAmount", "0");
                var ClientPer = new SqlParameter("@ClientPer", "0");
                var BankAmount = new SqlParameter("@BankAmount", "0");
                var BankPer = new SqlParameter("@BankPer", "0");
                //---------------------------------------
                var BookingDate = new SqlParameter("@BookingDate", "");
                var BookingAmount = new SqlParameter("@BookingAmount", "0");
                var BankId = new SqlParameter("@BankId", "0");
                var BookingAmountType = new SqlParameter("@BookingAmountType", "");
                var Chequeno = new SqlParameter("@Chequeno", "");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                //-------------------------------
                var ClientName = new SqlParameter("@ClientName", "");
                var BankName = new SqlParameter("@BankName", "");
                var Branch = new SqlParameter("@Branch", "");
                var AccountNo = new SqlParameter("@AccountNo", "");
                var IFSCCode = new SqlParameter("@IFSCCode", "");


                //--------------------------------------
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var UserId = new SqlParameter("@UserId", ReportId);

                var GST_No = new SqlParameter("@GST_No", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //----------------------------------------------------------------
                var ApplicationDate = new SqlParameter("@ApplicationDate", "");
                var ApplicationNo = new SqlParameter("@ApplicationNo", "");
                var BasementNo = new SqlParameter("@BasementNo", "");
                var SlotNo = new SqlParameter("@SlotNo", "");

                //-----------------------------------------------------------------
                var Action = new SqlParameter("@Action", 8);
                var _product = await _dbContext.tbl_ClientMaster.FromSqlRaw("stpro_ProjectBooking @OwnProjectId,@ProjectId, @EnquiryId, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @CustomerID, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@TotalAmount, @CarpetArea, @CommonArea, @BalconyArea, @WorkArea, @RatePerArea, @PrivateTerrace, @TerraceRate, @CarParking, @gst, @kfc, @LandCost, @LandRate, @landgst, @landkfc, @ClientAmount, @ClientPer, @BankAmount, @BankPer, @BookingDate, @BookingAmount, @BankId, @BookingAmountType, @Chequeno, @FinancialYearId, @ClientName, @BankName, @Branch, @AccountNo, @IFSCCode,@CompanyId, @BranchId,@UserId,@GST_No,@PaymentModeId, @ApplicationDate,@ApplicationNo,@BasementNo,@SlotNo,@Action ", OwnProjectId, ProjectId, EnquiryId, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CustomerID, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, TotalAmount, CarpetArea, CommonArea, BalconyArea, WorkArea, RatePerArea, PrivateTerrace, TerraceRate, CarParking, gst, kfc, LandCost, LandRate, landgst, landkfc, ClientAmount, ClientPer, BankAmount, BankPer, BookingDate, BookingAmount, BankId, BookingAmountType, Chequeno, FinancialYearId, ClientName, BankName, Branch, AccountNo, IFSCCode, CompanyId, BranchId, UserId, GST_No, PaymentModeId, ApplicationDate, ApplicationNo, BasementNo, SlotNo, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckProjectBookingEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

    }   
}
