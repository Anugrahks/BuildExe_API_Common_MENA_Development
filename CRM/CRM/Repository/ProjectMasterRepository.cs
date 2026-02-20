using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.ComponentModel.Design;
using Newtonsoft.Json;
using System.Reflection;
using BuildExeServices.Library;

namespace BuildExeServices.Repository
{
    public class ProjectMasterRepository : IProjectMasterRepository
    {
        private readonly ProductContext _dbContext;

        public ProjectMasterRepository(ProductContext dbContext)
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
            statusUpdate = 6,
            selectReport = 7,
            selectbyCompany = 8,
            selectbyProjectType = 9,
            selectbyProjectDepartment = 10,
            selectbyProjectPaymentMode = 11,
            selectMaterialProject = 1,
            projectidvalidation = 12,
            selectbyCompanylist = 13,
            selectProjectWithstage = 14,
            selectProjectForRefund = 15,
            selectProjectbySchedule = 16,
            projectid_and_Namevalidation = 17,
            statusname = 11,
            statusnamelist = 12


        }


        public async Task<int> ExecuteStoredProc(Project project)
        {
            int id;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                if (project.Password != null)
                {
                    project.Password = Encription.EncryptString(project.Password);
                }
                if (project.Password == null)
                    project.Password = "";
                if (project.UserName == null)
                    project.UserName = "";
                if (project.ClientUniqueName == null)
                    project.ClientUniqueName = "";
                if (project.ContactPerson == null)
                    project.ContactPerson = "";

                cmd.CommandText = "dbo.stpro_ProjectMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = project.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@ProjectTypeId", SqlDbType.NVarChar) { Value = project.ProjectTypeId });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = project.DepartmentId });
                cmd.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = project.ProjectName });
                if (project.ProjectDescription == null)
                    project.ProjectDescription = "";
                cmd.Parameters.Add(new SqlParameter("@ProjectDescription", SqlDbType.NVarChar) { Value = project.ProjectDescription });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = project.Status });

                if (project.ProjectTypeId == "OP")
                {
                    cmd.Parameters.Add(new SqlParameter("@StatusDescription", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = project.StartDate });
                    cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = project.EndDate });
                    cmd.Parameters.Add(new SqlParameter("@GST_No", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = project.ClientId });
                    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = project.EndDate });
                    cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@Pin", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@ClientUniqueName", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@LpoNo", SqlDbType.NVarChar) { Value = "" });
                    cmd.Parameters.Add(new SqlParameter("@LpoDate", SqlDbType.Date) { Value = "1990-01-01" });
                }
                else
                {
                    if (project.StatusDescription == null)
                        project.StatusDescription = "";
                    if (project.LastName == null)
                        project.LastName = "";
                    if (project.Sex == null)
                        project.Sex = "M";
                    if (project.Address == null)
                        project.Address = "";
                    if (project.Post == null)
                        project.Post = "";
                    if (project.GST_No == null)
                        project.GST_No = "";
                    if (project.Pin == null)
                        project.Pin = "";
                    if (project.PhoneNumber == null)
                        project.PhoneNumber = "";
                    if (project.MobileNumber == null)
                        project.MobileNumber = "";
                    if (project.EmailId == null)
                        project.EmailId = "";
                    if (project.ClientUniqueName == null)
                        project.ClientUniqueName = "";
                    if (project.ContactPerson == null)
                        project.ContactPerson = "";
                    if (project.LpoNo == null)
                        project.LpoNo = "";
                    if (project.LpoDate == null)
                        project.LpoDate = new DateTime(1990, 1, 1);

                    cmd.Parameters.Add(new SqlParameter("@StatusDescription", SqlDbType.NVarChar) { Value = project.StatusDescription });
                    cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = project.StartDate });
                    cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = project.EndDate });
                    cmd.Parameters.Add(new SqlParameter("@GST_No", SqlDbType.NVarChar) { Value = project.GST_No });
                    cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = project.ClientId });
                    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = project.FirstName });
                    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = project.LastName });
                    cmd.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar) { Value = project.Sex });
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = "1990-01-01" });
                    cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar) { Value = project.Address });
                    cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar) { Value = project.Post });
                    cmd.Parameters.Add(new SqlParameter("@Pin", SqlDbType.NVarChar) { Value = project.Pin });
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = project.PhoneNumber });
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar) { Value = project.MobileNumber });
                    cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar) { Value = project.EmailId });
                    cmd.Parameters.Add(new SqlParameter("@ClientUniqueName", SqlDbType.NVarChar) { Value = project.ClientUniqueName });
                    cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar) { Value = project.ContactPerson });
                    cmd.Parameters.Add(new SqlParameter("@LpoNo", SqlDbType.NVarChar) { Value = project.LpoNo });
                    cmd.Parameters.Add(new SqlParameter("@LpoDate", SqlDbType.Date) { Value = project.LpoDate });

                }

                if (project.TotalArea == null)
                    project.TotalArea = 0;
                if (project.RatePerArea == null)
                    project.RatePerArea = 0;
                if (project.TotalAmount == null)
                    project.TotalAmount = 0;
                if (project.ProjectArea == null)
                    project.ProjectArea = "";

                cmd.Parameters.Add(new SqlParameter("@TotalArea", SqlDbType.Decimal) { Value = project.TotalArea });
                cmd.Parameters.Add(new SqlParameter("@RatePerArea", SqlDbType.Decimal) { Value = project.RatePerArea });
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = project.TotalAmount });

                cmd.Parameters.Add(new SqlParameter("@PaymentModeId", SqlDbType.Int) { Value = project.PaymentModeId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = project.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = project.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = project.UserId });
                cmd.Parameters.Add(new SqlParameter("@EnquiryId", SqlDbType.Int) { Value = project.EnquiryId });

                if (project.ScheduleType == 0)
                    project.ScheduleType = 1;
                cmd.Parameters.Add(new SqlParameter("@ScheduleType", SqlDbType.Int) { Value = project.ScheduleType });

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Insert });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@IsWareHouse", SqlDbType.Bit) { Value = project.IsWareHouse });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = project.UserName });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = project.Password });
                cmd.Parameters.Add(new SqlParameter("@ProjectArea", SqlDbType.NVarChar) { Value = project.ProjectArea });
                cmd.Parameters.Add(new SqlParameter("@Latitude", SqlDbType.Decimal) { Value = project.Latitude });
                cmd.Parameters.Add(new SqlParameter("@Longitude", SqlDbType.Decimal) { Value = project.Longitude });
                string projectJson = JsonConvert.SerializeObject(project);
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = projectJson });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                await cmd.ExecuteNonQueryAsync();
                id = (int)cmd.Parameters["@id"].Value;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            return id;
        }
        public async Task<IEnumerable<ClientMaster>> GetClient(int CompanyId, int BranchId)
        {
            try
            {

                var Projectid = new SqlParameter("@ProjectId", CompanyId);
                var Unitid = new SqlParameter("@Unitid", BranchId);
                var json = new SqlParameter("@json", "{}");
                var Action = new SqlParameter("@Action", 6);
                var _product = await _dbContext.tbl_getClientMaster.FromSqlRaw("Stpro_GetClient @ProjectId,@Unitid,@json,@Action ", Projectid, Unitid, json, Action).ToListAsync();
                return _product;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Insertproject(Project project)
        {
            List<Validation> validations = new List<Validation>();
            Validation val = new Validation();

            //var det = await _dbContext.tbl_getClientMaster.Where(x => x.id != project.id).ToListAsync();

            var dett = await GetClient(project.CompanyId, project.BranchId);
            string lastname = string.IsNullOrEmpty(project.LastName) ? "" : project.LastName;
            var det = dett.Where(x => x.FirstName == project.FirstName)
                .Where(x => x.LastName == lastname).Where(c => c.ProjectId != project.id).ToList();
            var projectid = 0;
            if (det.Count == 0)
            {

                try
                {
                    projectid = await ExecuteStoredProc(project);
                    //return i;
                    val.Id = projectid;
                    val.StatusCode = 1;
                    val.Status = "SUCCESS";
                    val.ErrorMessage = "";
                }
                catch (Exception ex)
                {
                    val.Id = projectid;
                    val.StatusCode = 0;
                    val.Status = "FAILURE";
                    val.ErrorMessage = ex.Message;
                }
            }
            else
            {
                val.Id = projectid;
                val.StatusCode = 0;
                val.Status = "FAILURE";
                val.ErrorMessage = "Client name already exists";
            }
            validations.Add(val);
            return validations;


            //return (long)projectid;
            //var id = new SqlParameter("@id", "1");

            //id.Direction = System.Data.ParameterDirection.Output;
            //id.DbType = DbType.Int32;

            //var ProjectId = new SqlParameter("@ProjectId", project.ProjectId);
            //var ProjectTypeId = new SqlParameter("@ProjectTypeId", project.ProjectTypeId);
            //var DepartmentId = new SqlParameter("@DepartmentId", project.DepartmentId);
            //var ProjectName = new SqlParameter("@ProjectName", project.ProjectName);
            //var ProjectDescription = new SqlParameter("@ProjectDescription", project.ProjectDescription);
            //var Status = new SqlParameter("@Status", project.Status);
            //var StatusDescription = new SqlParameter("@StatusDescription", project.StatusDescription);
            //var StartDate = new SqlParameter("@StartDate", project.StartDate);
            //var EndDate = new SqlParameter("@EndDate", project.EndDate);
            //var GST_No = new SqlParameter("@GST_No", project.GST_No);
            //var ClientId = new SqlParameter("@ClientId", project.ClientId);
            //var FirstName = new SqlParameter("@FirstName", project.FirstName);
            //var LastName = new SqlParameter("@LastName", project.LastName);
            //var Sex = new SqlParameter("@Sex", project.Sex);
            //var DateOfBirth = new SqlParameter("@DateOfBirth", project.DateOfBirth);
            //var Address = new SqlParameter("@Address", project.Address);
            //var Post = new SqlParameter("@Post", project.Post);
            //var Pin = new SqlParameter("@Pin", project.Pin);
            //var PhoneNumber = new SqlParameter("@PhoneNumber", project.PhoneNumber);
            //var MobileNumber = new SqlParameter("@MobileNumber", project.MobileNumber);
            //var EmailId = new SqlParameter("@EmailId", project.EmailId);
            //var TotalArea = new SqlParameter("@TotalArea", project.TotalArea);
            //var RatePerArea = new SqlParameter("@RatePerArea", project.RatePerArea);
            //var TotalAmount = new SqlParameter("@TotalAmount", project.TotalAmount);
            //var PaymentModeId = new SqlParameter("@PaymentModeId", project.PaymentModeId);
            //var CompanyId = new SqlParameter("@CompanyId", project.CompanyId);
            //var BranchId = new SqlParameter("@BranchId", project.BranchId);
            //var UserId = new SqlParameter("@UserId", project.UserId);
            //var EnquiryId = new SqlParameter("@EnquiryId", project.EnquiryId);
            //var Action = new SqlParameter("@Action", Actions.Insert);

            //_dbContext.Database.ExecuteSqlRaw("stpro_ProjectMaster @id,@ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@Action ", id, ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, Action);

        }
        public async Task<IEnumerable<Validation>> UpdateStatus(int Project_Id,int DivisionId, Project project)
        {
            List<Validation> validations = new List<Validation>();
            Validation val = new Validation();
            try
            {
                var id = new SqlParameter("@id", Project_Id);
                var Status = new SqlParameter("@Status", project.Status);
                if (project.StatusDescription == null)
                    project.StatusDescription = "";
                if (project.ClientUniqueName == null)
                    project.ClientUniqueName = "";
                if (project.ContactPerson == null)
                    project.ContactPerson = "";
                var StatusDescription = new SqlParameter("@StatusDescription", project.StatusDescription);
                //var StatusDescription = new SqlParameter("@StatusDescription", project.StatusDescription ?? (object)DBNull.Value);

                var UserId = new SqlParameter("@UserId", project.UserId);
                var Action = new SqlParameter("@Action", Actions.statusUpdate);

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
                var ClientId = new SqlParameter("@ClientId", DivisionId);
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "1990-01-01");

                await _dbContext.Database.ExecuteSqlRawAsync("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, " +
                    "@StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, " +
                    "@MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action, @id, @IsWareHouse,@UserName,@Password,@ProjectArea,@Latitude,@Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json",
                    ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName,
                    LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId,
                    CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude,ClientUniqueName, ContactPerson,LpoNo,LpoDate, json);
                val.Id = Project_Id;
                val.StatusCode = 1;
                val.Status = "SUCCESS";
                val.ErrorMessage = "";
            }
            catch (Exception ex)
            {
                val.Id = Project_Id;
                val.StatusCode = 0;
                val.Status = "FAILURE";
                val.ErrorMessage = ex.Message;
            }
            validations.Add(val);
            return validations;
        }
        public async Task<IEnumerable<Validation>> Updateproject(Project project)
        {
            List<Validation> validations = new List<Validation>();
            Validation val = new Validation();


            var dett = await GetClient(project.CompanyId, project.BranchId);
            string lastname = string.IsNullOrEmpty(project.LastName) ? "" : project.LastName;
            var det = dett.Where(x => x.FirstName == project.FirstName)
                .Where(x => x.LastName == lastname).Where(c => c.ProjectId != project.id).ToList();

            if (det.Count() == 0)
            {
                try
                {
                    //_dbContext.Entry(project).State = EntityState.Modified;
                    //Save();
                    if (project.Password != null)
                    {
                        project.Password = Encription.EncryptString(project.Password);
                    }
                    if (project.Password == null)
                        project.Password = "";
                    if (project.UserName == null)
                        project.UserName = "";
                    if (project.StatusDescription == null)
                        project.StatusDescription = "";
                    if (project.LastName == null)
                        project.LastName = "";
                    if (project.Sex == null)
                        project.Sex = "M";
                    if (project.Address == null)
                        project.Address = "";
                    if (project.Post == null)
                        project.Post = "";
                    if (project.GST_No == null)
                        project.GST_No = "";
                    if (project.Pin == null)
                        project.Pin = "";
                    if (project.PhoneNumber == null)
                        project.PhoneNumber = "";
                    if (project.MobileNumber == null)
                        project.MobileNumber = "";
                    if (project.EmailId == null)
                        project.EmailId = "";
                    if (project.TotalArea == null)
                        project.TotalArea = 0;
                    if (project.RatePerArea == null)
                        project.RatePerArea = 0;
                    if (project.TotalAmount == null)
                        project.TotalAmount = 0;
                    if (project.DateOfBirth == null)
                        project.DateOfBirth = DateTime.Now;
                    if (project.ProjectArea == null)
                        project.ProjectArea = "";
                    if (project.LpoNo == null)
                        project.LpoNo = "";
                    
                    if (project.ClientUniqueName == null)
                        project.ClientUniqueName = "";
                    if (project.ContactPerson == null)
                        project.ContactPerson = "";
                    if (project.LpoDate == null)
                        project.LpoDate = new DateTime(1990, 1, 1);


                    var id = new SqlParameter("@id", project.id);
                    var ProjectId = new SqlParameter("@ProjectId", project.ProjectId);
                    var ProjectTypeId = new SqlParameter("@ProjectTypeId", project.ProjectTypeId);
                    var DepartmentId = new SqlParameter("@DepartmentId", project.DepartmentId);
                    var ProjectName = new SqlParameter("@ProjectName", project.ProjectName);
                    if (project.ProjectDescription == null)
                        project.ProjectDescription = "";
                    var ProjectDescription = new SqlParameter("@ProjectDescription", project.ProjectDescription);
                    var Status = new SqlParameter("@Status", project.Status);
                    var StatusDescription = new SqlParameter("@StatusDescription", project.StatusDescription);
                    var StartDate = new SqlParameter("@StartDate", project.StartDate);
                    var EndDate = new SqlParameter("@EndDate", project.EndDate);
                    var GST_No = new SqlParameter("@GST_No", project.GST_No);
                    var ClientId = new SqlParameter("@ClientId", project.ClientId);
                    var FirstName = new SqlParameter("@FirstName", project.FirstName);
                    var LastName = new SqlParameter("@LastName", project.LastName);
                    var Sex = new SqlParameter("@Sex", project.Sex);
                    var DateOfBirth = new SqlParameter("@DateOfBirth", project.DateOfBirth);
                    var Address = new SqlParameter("@Address", project.Address);
                    var Post = new SqlParameter("@Post", project.Post);
                    var Pin = new SqlParameter("@Pin", project.Pin);
                    var PhoneNumber = new SqlParameter("@PhoneNumber", project.PhoneNumber);
                    var MobileNumber = new SqlParameter("@MobileNumber", project.MobileNumber);
                    var EmailId = new SqlParameter("@EmailId", project.EmailId);
                    var TotalArea = new SqlParameter("@TotalArea", project.TotalArea);
                    var RatePerArea = new SqlParameter("@RatePerArea", project.RatePerArea);
                    var TotalAmount = new SqlParameter("@TotalAmount", project.TotalAmount);
                    var PaymentModeId = new SqlParameter("@PaymentModeId", project.PaymentModeId);
                    var CompanyId = new SqlParameter("@CompanyId", project.CompanyId);
                    var BranchId = new SqlParameter("@BranchId", project.BranchId);
                    var UserId = new SqlParameter("@UserId", project.UserId);
                    var EnquiryId = new SqlParameter("@EnquiryId", project.EnquiryId);
                    var ScheduleType = new SqlParameter("@ScheduleType", project.ScheduleType);
                    var IsWareHouse = new SqlParameter("@IsWareHouse", project.IsWareHouse);
                    var UserName = new SqlParameter("@UserName", project.UserName);
                    var PassWord = new SqlParameter("@Password", project.Password);
                    var Action = new SqlParameter("@Action", Actions.Update);
                    var ProjectArea = new SqlParameter("@ProjectArea", project.ProjectArea);
                    var Latitude = new SqlParameter("@Latitude", project.Latitude);
                    var Longitude = new SqlParameter("@Longitude", project.Longitude);
                    var ClientUniqueName = new SqlParameter("@ClientUniqueName", project.ClientUniqueName);
                    var ContactPerson = new SqlParameter("@ContactPerson", project.ContactPerson);
                    var LpoNo = new SqlParameter("@LpoNo", project.LpoNo);
                    var LpoDate = new SqlParameter("@LpoDate", project.LpoDate);
                    var json= new SqlParameter("@json", JsonConvert.SerializeObject(project));

                    await _dbContext.Database.ExecuteSqlRawAsync("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, " +
                        "@StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, " +
                        "@MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action, @id, @IsWareHouse,@UserName,@Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json",
                        ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName,
                        LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId,
                        CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo,LpoDate, json);

                    val.Id = project.id;
                    val.StatusCode = 1;
                    val.Status = "SUCCESS";
                    val.ErrorMessage = "";
                }
                catch (Exception ex)
                {
                    val.Id = project.id;
                    val.StatusCode = 0;
                    val.Status = "FAILURE";
                    val.ErrorMessage = ex.Message;
                }
            }
            else
            {
                val.Id = project.id;
                val.StatusCode = 0;
                val.Status = "FAILURE";
                val.ErrorMessage = "Client name already exists";
            }
            validations.Add(val);
            return validations;

        }

        public async Task<string> Validate(int id, string FirstName, string LastName)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_CheckProjectClientName";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@FName", SqlDbType.NVarChar) { Value = FirstName });
                cmd.Parameters.Add(new SqlParameter("@LName", SqlDbType.NVarChar) { Value = LastName });


                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                return details;

                //var Id = new SqlParameter("@Id", id);
                //var Fn = new SqlParameter("@FName", FirstName);
                //var Ln = new SqlParameter("@LName", LastName);

                //var std = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckProjectClientName @Id,@FName,@LName", Id, Fn, Ln).ToListAsync();

                //return std.FirstOrDefault().StatusCode.ToString();    


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Deleteproject(int projectId, int userId, int DivisionId)
        {
            try
            {
                var id = new SqlParameter("@id", projectId);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", DivisionId);
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");
                

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, " +
                    "@StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, " +
                    "@MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action, @id, @IsWareHouse,@UserName,@Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json",
                    ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName,
                    LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId,
                    CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();

                //var project = _dbContext.tbl_ProjectMaster.Find(projectId);
                //_dbContext.tbl_ProjectMaster.Remove(project);
                //Save();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Project>> Getproject()
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMaster.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, " +
                    "@ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, " +
                    "@Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId," +
                    "@UserId,@EnquiryId,@ScheduleType,@Action,@id,@IsWareHouse,@UserName,@Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription,
                    StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea,
                    RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Project>> GetprojectByID(int projectId)
        {
            try
            {
                var id = new SqlParameter("@id", projectId);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMaster.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, " +
                     "@ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, " +
                     "@Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId," +
                     "@UserId,@EnquiryId,@ScheduleType,@Action,@id,@IsWareHouse,@UserName,@Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription,
                     StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea,
                     RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo,LpoDate, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Project>> GetprojectByDepartment(int Departmentid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", Departmentid);
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectbyProjectDepartment);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMaster.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, " +
                     "@ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, " +
                     "@Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId," +
                     "@UserId,@EnquiryId,@ScheduleType,@Action,@id,@IsWareHouse,@UserName,@Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription,
                     StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea,
                     RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReport(ProjectSearch projectSearch)
        {
            try
            {

                //var ProjectTypeId = new SqlParameter("@ProjectTypeId", projTypeId);
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_ProjectReport";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(projectSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = projectSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = projectSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid, int paymentModeId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", paymentModeId);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var Action = new SqlParameter("@Action", Actions.selectbyProjectPaymentMode);
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");
                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse,UserName,PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectbyCompany);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", siteuser);
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", 22);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<ProjectList>> GetAllproject(int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", 20);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> GetAllproject(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", siteuser);
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", 21);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        
         public async Task<IEnumerable<ProjectList>> GetAllClientproject(int companyid, int branchid, string clientName)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", 24);

                var id = new SqlParameter("@id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var ClientUniqueName = new SqlParameter("@ClientUniqueName", clientName ?? "");
                var json = new SqlParameter("@json", "");

                //var result = await _dbContext.tbl_ProjectMasterlist
                //    .FromSqlRaw("EXEC stpro_ProjectMaster @CompanyId, @BranchId, @Action, @id OUTPUT, @ClientUniqueName, @json",
                //        CompanyId, BranchId, Action, id, ClientUniqueName, json)
                //    .ToListAsync();
                var result = await _dbContext.tbl_ProjectMasterlist
                    .FromSqlRaw(@"EXEC stpro_ProjectMaster 
                    @CompanyId=@CompanyId,
                    @BranchId=@BranchId,
                    @Action=@Action,
                    @ClientUniqueName=@ClientUniqueName,
                    @id=@id OUTPUT,
                    @json=@json",
        CompanyId, BranchId, Action, ClientUniqueName, id, json)
    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> GetprojectBySchedule(int companyid, int branchid, int scheduletype)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", scheduletype);
                var Action = new SqlParameter("@Action", Actions.selectProjectbySchedule);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> GetprojectByScheduleuser(int companyid, int branchid, int scheduletype, int UserId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", scheduletype);
                var Action = new SqlParameter("@Action", Actions.selectProjectbySchedule);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson,LpoNo,LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> GetBySchedule(int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", 19);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ProjectList>> GetprojectList(int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectbyCompanylist);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetprojectListuser(int companyid, int branchid, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_ProjectMaster";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@ProjectTypeId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@ProjectDescription", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@StatusDescription", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@GST_No", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = DateTime.Now });

                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Pin", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@TotalArea", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@RatePerArea", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@PaymentModeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@EnquiryId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ScheduleType", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@IsWareHouse", SqlDbType.Bit) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectbyCompanylist });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ProjectArea", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Latitude", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Longitude", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ClientUniqueName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LpoNo", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LpoDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> Getproject_withStage(int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectProjectWithstage);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<ProjectList>> Getproject_ForRefund(int Type, int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", Type);
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectProjectForRefund);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<ProjectList>> Getproject_by_type(int companyid, int branchid, string ProjetTypeid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", ProjetTypeid);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.selectbyProjectType);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IEnumerable<ProjectList>> Getproject_by_type(int companyid, int branchid, string ProjetTypeid, int userid, int siteuser)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", siteuser);
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", ProjetTypeid);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", "0");
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userid);
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", 23);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse ,@UserName, @Password, @ProjectArea, @Latitude, @Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate, @json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();
                return _product;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<ProjectCombo>> GetMaterialRecieveProject(int companyid, int branchid)
        {
            try
            {
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var ProjectStatusId = new SqlParameter("@ProjectStatusId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.selectMaterialProject);

                var _product = await _dbContext.tbl_ProjectMastercombo.FromSqlRaw("stpro_ProjectMaster_ForComboBox @ProjectTypeId, @ProjectStatusId, @CompanyId, @BranchId,@Action ", ProjectTypeId, ProjectStatusId, CompanyId, BranchId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectCombo>> GetMaterialRecieveProject(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", userId);
                var ProjectStatusId = new SqlParameter("@ProjectStatusId", siteuser);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.Update);

                var _product = await _dbContext.tbl_ProjectMastercombo.FromSqlRaw("stpro_ProjectMaster_ForComboBox @ProjectTypeId, @ProjectStatusId, @CompanyId, @BranchId,@Action ", ProjectTypeId, ProjectStatusId, CompanyId, BranchId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<long> GetProjectIdValidation(int Id, string projectid, int companyid, int branchid)
        {
            long id = 1;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_ProjectMaster";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.NVarChar) { Value = projectid });
                cmd.Parameters.Add(new SqlParameter("@ProjectTypeId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.NVarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@ProjectDescription", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@StatusDescription", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@GST_No", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = DateTime.Now });

                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Pin", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar) { Value = "" });

                cmd.Parameters.Add(new SqlParameter("@TotalArea", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@RatePerArea", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@PaymentModeId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@EnquiryId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ScheduleType", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@IsWareHouse", SqlDbType.Bit) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.projectidvalidation });
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ProjectArea", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Latitude", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Longitude", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ClientUniqueName", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LpoNo", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@LpoDate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count > 0)
                    id = Convert.ToInt64(dataTable.Rows[0][0].ToString());


            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = 1;
            }
            return id;
        }


        public async Task<IEnumerable<Validation>> Getproject_Validation(int Id, string Project_Id, string PRojectname, int companyid, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", Id);
                var ProjectId = new SqlParameter("@ProjectId", Project_Id);
                var ProjectTypeId = new SqlParameter("@ProjectTypeId", "0");
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var ProjectName = new SqlParameter("@ProjectName", PRojectname);
                var ProjectDescription = new SqlParameter("@ProjectDescription", "0");
                var Status = new SqlParameter("@Status", "0");
                var StatusDescription = new SqlParameter("@StatusDescription", "0");
                var StartDate = new SqlParameter("@StartDate", "2020-01-01");
                var EndDate = new SqlParameter("@EndDate", "2020-01-01");
                var GST_No = new SqlParameter("@GST_No", "0");
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
                var TotalArea = new SqlParameter("@TotalArea", "0");
                var RatePerArea = new SqlParameter("@RatePerArea", "0");
                var TotalAmount = new SqlParameter("@TotalAmount", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var EnquiryId = new SqlParameter("@EnquiryId", "0");
                var ScheduleType = new SqlParameter("@ScheduleType", "0");
                var Action = new SqlParameter("@Action", Actions.projectid_and_Namevalidation);
                var IsWareHouse = new SqlParameter("@IsWareHouse", "false");
                var UserName = new SqlParameter("@UserName", "");
                var PassWord = new SqlParameter("@Password", "");
                var ProjectArea = new SqlParameter("@ProjectArea", "");
                var Latitude = new SqlParameter("@Latitude", "0");
                var Longitude = new SqlParameter("@Longitude", "0");
                var ClientUniqueName = new SqlParameter("@ClientUniqueName", "");
                var ContactPerson = new SqlParameter("@ContactPerson", "");
                var LpoNo = new SqlParameter("@LpoNo", "");
                var LpoDate = new SqlParameter("@LpoDate", "1990-01-01");
                var json = new SqlParameter("@json", "");

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("stpro_ProjectMaster @ProjectId, @ProjectTypeId, @DepartmentId, @ProjectName, @ProjectDescription, @Status, @StatusDescription, @StartDate, @EndDate, @GST_No, @ClientId, @FirstName, @LastName, @Sex, @DateOfBirth, @Address, @Post, @Pin, @PhoneNumber, @MobileNumber, @EmailId, @TotalArea, @RatePerArea, @TotalAmount, @PaymentModeId, @CompanyId, @BranchId,@UserId,@EnquiryId,@ScheduleType,@Action,@id, @IsWareHouse,@UserName,@Password,@ProjectArea,@Latitude,@Longitude,@ClientUniqueName,@ContactPerson,@LpoNo,@LpoDate,@json", ProjectId, ProjectTypeId, DepartmentId, ProjectName, ProjectDescription, Status, StatusDescription, StartDate, EndDate, GST_No, ClientId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, TotalArea, RatePerArea, TotalAmount, PaymentModeId, CompanyId, BranchId, UserId, EnquiryId, ScheduleType, Action, id, IsWareHouse, UserName, PassWord, ProjectArea, Latitude, Longitude, ClientUniqueName, ContactPerson, LpoNo, LpoDate, json).ToListAsync();

                return purchaseList;

                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetClient(int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_ProjectMaster_ForComboBox";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectTypeId", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                //cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        #region ProjectFilter

        public async Task<IEnumerable<ProjectList>> GetProjectsForProjectSpecification(int Departmentid)
        {
            try
            {
                var deptId = new SqlParameter("@DepartmentId", Departmentid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForProjectSpecification @DepartmentId", deptId).ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

        public async Task<IEnumerable<ProjectList>> getprojectwithcontractors(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForContractor @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> getprojectwithcontractors(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@userId", userId);
                var SiteUser = new SqlParameter("@siteuser", siteuser);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForContractorsiteuser @CompanyId, @BranchId,@userId,@siteuser", CompanyId, BranchId, UserId, SiteUser).ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForManualBoq(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForManualBoq @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForStageInvoice(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForStageInvoice @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForStageInvoiceuser(int companyid, int branchid, int UserId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", 0);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForStageInvoice @CompanyId, @BranchId, @UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectList>> GetProjectsForStageInvoiceuser(int companyid, int branchid, int UserId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", siteuser);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForStageInvoice @CompanyId, @BranchId, @UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectList>> GetProjectsForGeneralInvoice(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForGeneralInvoice @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForGeneralInvoice(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@userId", userId);
                var SiteUser = new SqlParameter("@siteuser", siteuser);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForGeneralInvoicesiteUser @CompanyId, @BranchId, @userId, @siteuser", CompanyId, BranchId,UserId,SiteUser).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForStageReceipt(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForStageReceipt @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForStageReceipt(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@userId", userId);
                var SiteUser = new SqlParameter("@siteuser", siteuser);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForStageReceiptsiteuser @CompanyId, @BranchId, @userId, @siteuser", CompanyId, BranchId, UserId, SiteUser).ToListAsync();
                return projects;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<ProjectList>> GetProjectsForPartBill(int companyid, int branchid, int paymentModeId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var PaymentModeId = new SqlParameter("@PaymentModeId", paymentModeId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForPartBill @CompanyId, @BranchId, @PaymentModeId", CompanyId, BranchId, PaymentModeId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForBillReceipt(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForBillReceipt @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForPartBill(int companyid, int branchid, int paymentModeId, int userId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var PaymentModeId = new SqlParameter("@PaymentModeId", paymentModeId);
                var UserId = new SqlParameter("@userId", userId);
                var SiteUser = new SqlParameter("@siteuser", siteuser);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForPartBillsiteuser @CompanyId, @BranchId, @PaymentModeId,@userId,@siteuser", CompanyId, BranchId, PaymentModeId, UserId, SiteUser).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForBillReceipt(int companyid, int branchid, int userId, int siteuser)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@userId", userId);
                var SiteUser = new SqlParameter("@siteuser", siteuser);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForBillReceiptsiteuser @CompanyId, @BranchId,@userId,@siteuser", CompanyId, BranchId, UserId, SiteUser).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetOwnProjects(int companyid, int branchid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetOwnProjects @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForRateEvaluation(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@UserId", "0");
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForRateEvaluation @CompanyId, @BranchId, @UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForRateEvaluationuser(int companyid, int branchId, int UserId, int FinancialYearId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@UserId", UserId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForRateEvaluation @CompanyId, @BranchId,@UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForRateComparison(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@UserId", "0");
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForRateComparison @CompanyId, @BranchId, @UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForRateComparisonuser(int companyid, int branchId, int UserId, int FinancialYearId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@UserId", UserId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForRateComparison @CompanyId, @BranchId,@UserId", CompanyId, BranchId, userId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForWeeklyBill(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForWeeklyBill @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForRefunding(int companyid, int branchId, int type)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var Type = new SqlParameter("@Type", type);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForRefunding @CompanyId, @BranchId, @Type", CompanyId, BranchId, Type).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForAssignBlockFloors(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForAssignBlockFloors @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForClientAdvance(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForClientAdvance @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetProjectsForLabourInProject(int companyid, int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetProjectsForLabourInProject";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@siteuser", SqlDbType.Int) { Value = 0 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetProjectsForLabourInProjectnew(int companyid, int branchId, int UserId, int SiteUser)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetProjectsForLabourInProject";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@siteuser", SqlDbType.Int) { Value = SiteUser });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> GetProjectsForLabourInProjectsitemanager(int companyid, int branchId, int UserId, int SiteUser, int SitemanagerId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetProjectsForLabourInProjectWithSitemanager";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@siteuser", SqlDbType.Int) { Value = SiteUser });
                cmd.Parameters.Add(new SqlParameter("@sitemanagerId", SqlDbType.Int) { Value = SitemanagerId });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetProjectsForLabourInProjectSetting(int companyid, int branchId, int UserId, int SiteUser, int Status)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetProjectsForLabourInProjectSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@siteuser", SqlDbType.Int) { Value = SiteUser });
                cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Int) { Value = Status });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return dataTable.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForDocument(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForDocument @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProjectList>> GetProjectsForEst(int companyid, int branchId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var projects = await _dbContext.tbl_ProjectMasterlist.FromSqlRaw("stpro_GetProjectsForProjEst @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return projects;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int divisionId, int isEdit)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var DivisionId = new SqlParameter("@divisionId", divisionId);
                var IsEdit = new SqlParameter("@isEdit", isEdit);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckNormalProjectEditDelete @Id, @divisionId, @isEdit", Id, DivisionId, IsEdit).ToListAsync();
                return result;
            }
            catch (Exception ex) { throw; }

        }

        public async Task<string> StatusName(int CompanyId, int BranchId, int DivisionId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = DivisionId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.statusname });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> StatusNameList(int CompanyId, int BranchId, int ProjectId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdditionalBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.statusnamelist });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion

    }
}
