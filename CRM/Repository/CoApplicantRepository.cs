using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class CoApplicantRepository:ICoApplicantRepository 
    {
        private readonly ProductContext _dbContext;

        public CoApplicantRepository(ProductContext dbContext)
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
            Select = 5
        }

        public async Task<IEnumerable<CoApplicant>> GetByID(int id)
        {
            try { 
            return await _dbContext.tbl_CoApplicant .Where(x => x.UnitId  == id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<CoApplicant>> Get()
        {
            try
            {
                return await _dbContext.tbl_CoApplicant.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
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

        public async Task Insert(IEnumerable<CoApplicant> coApplicant)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(coApplicant));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);



                // var UnitId = new SqlParameter("@UnitId", coApplicant.UnitId);
                //var CoApplicantName = new SqlParameter("@CoApplicantName", SafeDbObject(coApplicant.CoApplicantName));
                //var CoApplicantAddress = new SqlParameter("@CoApplicantAddress", SafeDbObject(coApplicant.CoApplicantAddress));
                //var Phone = new SqlParameter("@Phone", SafeDbObject(coApplicant.Phone));
                //var Mobile = new SqlParameter("@Mobile", SafeDbObject(coApplicant.Mobile));
                //var Email = new SqlParameter("@Email", SafeDbObject(coApplicant.Email));
                //var CoApplicantSex = new SqlParameter("@CoApplicantSex", SafeDbObject(coApplicant.CoApplicantSex));
                //var coapplicantDateOfBirth = new SqlParameter("@coapplicantDateOfBirth", SafeDbObject(coApplicant.coapplicantDateOfBirth));
                //var Pin = new SqlParameter("@Pin", SafeDbObject(coApplicant.Pin));

                //var Sonof = new SqlParameter("@Sonof", SafeDbObject(coApplicant.Sonof));
                //var SpouseName = new SqlParameter("@SpouseName", SafeDbObject(coApplicant.SpouseName));

                //var PanNO = new SqlParameter("@PanNO", SafeDbObject(coApplicant.PanNO));
                //var AdharNo = new SqlParameter("@AdharNo", SafeDbObject(coApplicant.AdharNo));
                //var MaritalStatus = new SqlParameter("@MaritalStatus", SafeDbObject(coApplicant.MaritalStatus));
                //var WeddingAnniversary = new SqlParameter("@WeddingAnniversary", SafeDbObject(coApplicant.WeddingAnniversary));

                //var Nationality = new SqlParameter("@Nationality", SafeDbObject(coApplicant.Nationality));
                //var State = new SqlParameter("@State", SafeDbObject(coApplicant.State));
                //var District = new SqlParameter("@District", SafeDbObject(coApplicant.District));
                //var HouseName = new SqlParameter("@HouseName", SafeDbObject(coApplicant.HouseName));

                //var Permanentaddress = new SqlParameter("@Permanentaddress", SafeDbObject(coApplicant.Permanentaddress));
                //var Permanentdistrict = new SqlParameter("@Permanentdistrict", SafeDbObject(coApplicant.Permanentdistrict));
                //var PermanentState = new SqlParameter("@PermanentState", SafeDbObject(coApplicant.PermanentState));
                //var PermanentPin = new SqlParameter("@PermanentPin", SafeDbObject(coApplicant.PermanentPin));
                //var villege = new SqlParameter("@villege", SafeDbObject(coApplicant.villege));
                //var Amsom = new SqlParameter("@Amsom", SafeDbObject(coApplicant.Amsom));
                //var desom = new SqlParameter("@desom", SafeDbObject(coApplicant.desom));
                //var Taluk = new SqlParameter("@Taluk", SafeDbObject(coApplicant.Taluk));

                //var NameOfPowerofattorny = new SqlParameter("@NameOfPowerofattorny", SafeDbObject(coApplicant.NameOfPowerofattorny));
                //var ReidentialStatus = new SqlParameter("@ReidentialStatus", SafeDbObject(coApplicant.ReidentialStatus));
                //var NameOfOrganization = new SqlParameter("@NameOfOrganization", SafeDbObject(coApplicant.NameOfOrganization));
                //var Addressoforganization = new SqlParameter("@Addressoforganization", SafeDbObject(coApplicant.Addressoforganization));
                //var OrganizationType = new SqlParameter("@OrganizationType", SafeDbObject(coApplicant.OrganizationType));
                //var ClientImage = new SqlParameter("@ClientImage", SafeDbObject(coApplicant.ClientImage));

               // await _dbContext.Database.ExecuteSqlRawAsync("stpro_coapplicant @Id,@UnitId, @CoApplicantName, @CoApplicantAddress, @Phone, @Mobile, @Email, @CoApplicantSex, @coapplicantDateOfBirth, @Pin, @Sonof, @SpouseName,@PanNO,@AdharNo,@MaritalStatus, @WeddingAnniversary, @Nationality,@State,@District,@HouseName,@Permanentaddress,@Permanentdistrict,@PermanentState,@PermanentPin,@villege,@Amsom,@desom,@Taluk,@NameOfPowerofattorny,@ReidentialStatus,@NameOfOrganization,@Addressoforganization,@OrganizationType,@ClientImage,@Action ", Id, UnitId, CoApplicantName, CoApplicantAddress, Phone, Mobile, Email, CoApplicantSex, coapplicantDateOfBirth, Pin, Sonof, SpouseName, PanNO, AdharNo, MaritalStatus, WeddingAnniversary, Nationality, State, District, HouseName, Permanentaddress, Permanentdistrict, PermanentState, PermanentPin, villege, Amsom, desom, Taluk, NameOfPowerofattorny, ReidentialStatus, NameOfOrganization, Addressoforganization, OrganizationType, ClientImage, Action);
                await _dbContext.Database.ExecuteSqlRawAsync("stpro_coapplicant @Id, @json,@CompanyId,@BranchId,@UserId,@Action ", Id,item, CompanyId,BranchId,UserId, Action);


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
