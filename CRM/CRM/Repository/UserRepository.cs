using BuildExeServices.DBContexts;
using BuildExeServices.Library;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Newtonsoft.Json.Serialization;

namespace BuildExeServices.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _dbContext;
        private static string key = "XLTRPNZ7ZsKGr5RKOLSNsJe9rgcPLLjn";
        public UserRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Users @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Users> GetByID(int id, int Key)
        {
            try
            {
                Users user;
                user = await _dbContext.tbl_Users.FindAsync(id);
                if (Key == 4260)
                {
                    user.Password = Encription.DecryptString(user.Password);
                }
                    return user;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Users>> GetUsers(int Companyid, int BranchId)
        {
            try
            {
                if (BranchId == 0)
                {
                    var list = await _dbContext.tbl_Users.Where(p => p.CompanyId == Companyid).ToListAsync();
                    //foreach (var detail in list)
                    //{
                    //    detail.Password = Encription.DecryptString(detail.Password);

                    //}
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_Users.Where(p => p.CompanyId == Companyid).Where(p => p.BranchId == BranchId).ToListAsync();
                    //foreach (var detail in list)
                    //{
                    //    detail.Password = Encription.DecryptString(detail.Password);

                    //}
                    return list;
                }

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<String> GetUser(int Companyid, int BranchId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_Users
                                  join b in _dbContext.tbl_UserGroup on a.UserGroupId equals b.UserGroupId
                                  where a.CompanyId == Companyid
                                  && a.BranchId == BranchId
                                  && a.UserGroupId != 1
                                  select new
                                  {
                                      id = a.Id,
                                      userId = a.UserId,
                                      userName = a.UserName,
                                      password = "",
                                      userGroupId = a.UserGroupId,
                                      userGroupName = b.UserGroupName,
                                      fullName = a.FullName,
                                      active = a.Active,
                                      emailId = a.EmailId,
                                      mobile = a.Mobile,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      siteUser = a.SiteUser,
                                      isAdmin = a.IsAdmin,
                                      superUser = a.SuperUser,
                                      yearClosingRights = a.YearClosingRights,
                                      dashBoardPrivilege = a.DashBoardPrivilege,
                                      sitemanager= a.Sitemanager,
                                      sitemanagerId=a.SitemanagerId,
                                      centralizedUserEnquiry=a.CentralizedUserEnquiry,
                                      centralizedUserProject=a.CentralizedUserProject,
                                      branchAdmin=a.BranchAdmin,
                                      employeeUser=a.EmployeeUser,
                                      personalLedgerPermission=a.PersonalLedgerPermission,
                                      employeeMasterPermission=a.EmployeeMasterPermission,
                                      departmentIds = a.DepartmentIds,
                                      departmentName = a.DepartmentName,
                                      client = a.Client,
                                      clientNameId = a.ClientNameId,
                                      clientName= a.ClientName,
                                      normalUser= a.NormalUser,
                                      userAssignedProject = string.Join(",", _dbContext.tbl_UserAssignedProject
                                                                          .Where(s => s.UserId == a.Id)
                                                                          .Select(s => s.ProjectId))
    }).OrderByDescending(x => x.id)
                                  .ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Users>> GetBySiteType(int Companyid, int BranchId, int siteUser)
        {
            try
            {
                if (BranchId == 0)
                {
                    var list = await _dbContext.tbl_Users.Where(p => p.CompanyId == Companyid).Where(p => p.SiteUser == siteUser).ToListAsync();
                    foreach (var detail in list)
                    {
                        detail.Password = Encription.DecryptString(detail.Password);

                    }
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_Users.Where(p => p.CompanyId == Companyid).Where(p => p.BranchId == BranchId).Where(p => p.SiteUser == siteUser).ToListAsync();
                    foreach (var detail in list)
                    {
                        detail.Password = Encription.DecryptString(detail.Password);

                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<int> Getforlogin(int Companyid, int BranchId, string UserName, string Password)
        {
            try
            {
                int userid = 0;
                Users user;
                var list = await _dbContext.tbl_Users.Where(p => p.UserId == UserName).Where(p => p.CompanyId == Companyid).Where(p => p.BranchId == BranchId).Where(p => p.Active == "Y").ToListAsync();
                foreach (var detail in list)
                {
                    detail.Password = Encription.DecryptString(detail.Password);
                    if (detail.Password == Password)
                    {
                        userid = detail.Id;
                        break;
                    }
                }

                return userid;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private string GenerateJwtToken(Users user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);


            var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost",
                        audience: "https://localhost",
                        claims: new List<Claim>(),
                        expires: DateTime.UtcNow.AddDays(15),
                        signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        public async Task<string> GetforNewLogin(Users users, string srcApp)
        {
            try
            {
                string details = "";
                int userId = 0;

                // Fetch user from the database
                var list = await _dbContext.tbl_Users
                    .Where(p => p.UserId == users.UserId && p.Active == "Y")
                    .ToListAsync();

                if (list.Count > 0)
                {
                    foreach (var detail in list)
                    {
                        var encryptedPwd = detail.Password;
                        detail.Password = Encription.DecryptString(detail.Password);

                        if (detail.Password == users.Password)
                        {
                            userId = detail.Id;
                            users.Password = encryptedPwd;

                            // Generate JWT token for the user
                            string jwtToken = GenerateJwtToken(users);

                            // Validate user and get details
                            details = await CheckUserAndValidate(users, srcApp);

                            if (!string.IsNullOrEmpty(details))
                            {
                                // Parse the existing JSON details into a dynamic object
                                var parsedDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(details);

                                if (parsedDetails != null && parsedDetails.Count > 0)
                                {
                                    // Add the JWT token to the first object in the array
                                    parsedDetails[0].jwtToken = jwtToken;

                                    // Convert back to JSON string
                                    details = Newtonsoft.Json.JsonConvert.SerializeObject(parsedDetails);
                                }
                            }
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(details))
                {
                    details = "[{\"statusCode\":0,\"id\":0,\"status\":\"FAILED\",\"errorMessage\":\"Invalid Username and Password\",\"errorType\":null}]";
                }

                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> CheckUserAndValidate(Users search, string srcApp)
        {
            try
            {


                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();


                if (srcApp == "Mobile")
                {
                    cmd.CommandText = "dbo.Stpro_AuthenticateMobileUser";
                }
                else { 
                    cmd.CommandText = "dbo.Stpro_LoginCheck"; 
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = search.UserId });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = search.Password });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = search.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = search.BranchId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }


        public async Task InsertLoginLog(string username, string ipAddress, DateTime loginTime)
        {
            using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Stpro_InsertLoginLog";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username });
            cmd.Parameters.Add(new SqlParameter("@IpAddress", SqlDbType.NVarChar) { Value = ipAddress });
            cmd.Parameters.Add(new SqlParameter("@LoginTime", SqlDbType.DateTime) { Value = DateTime.Now });

            if (cmd.Connection.State != ConnectionState.Open)
                await cmd.Connection.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<string> GetStarted(int action)
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
                string startDet = "";

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                var username = config["InstallerUsername"];
                var password = Encription.EncryptString(config["InstallerPassword"]);
                cmd.CommandText = "dbo.Stpro_GetStarted";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar) { Value = username });
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = action });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                //startDet = username + " - " + password;
                var dataTable = new DataTable();
                dataTable.Load(reader);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    startDet = startDet + dataTable.Rows[i][0].ToString();
                }

                return startDet;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<int> Getforlogin(Users users)
        {
            try
            {
                int userid = 0;
                Users user;
                var list = await _dbContext.tbl_Users.Where(p => p.UserId == users.UserId).Where(p => p.CompanyId == users.CompanyId).Where(p => p.BranchId == users.BranchId).Where(p => p.Active == "Y").ToListAsync();
                foreach (var detail in list)
                {
                    detail.Password = Encription.DecryptString(detail.Password);
                    if (detail.Password == users.Password)
                    {
                        userid = detail.Id;
                        //GenerateToken(detail);
                        break;
                    }
                }

                return userid;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Users>> GetByUserID(string id, int Key)
        {
            try
            {
                //List<Users> _user;
                //_user = _dbContext.tbl_Users.Where(p => p.UserId == id).ToList();

                var list = await _dbContext.tbl_Users.Where(p => p.UserId == id).ToListAsync();
                if (Key == 4260)
                {
                    foreach (var detail in list)
                    {
                        detail.Password = Encription.DecryptString(detail.Password);

                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Users>> Getkey(int Key)
        {
            try
            {
                var list = await _dbContext.tbl_Users.ToListAsync();
                if (Key == 4260)
                {

                    foreach (var detail in list)
                    {
                        detail.Password = Encription.DecryptString(detail.Password);
                    }
                }
                else
                {

                    foreach (var detail in list)
                    {
                        detail.UserName = "Not Available";
                        detail.Password = "Not Available";
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Insert(Users users)
        {
            try
            {
                if (users.Active == null)
                    users.Active = "Y";

                users.UserId = users.UserName;
                users.Password = Encription.EncryptString(users.Password);

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(users));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Users @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
                //await _dbContext.AddAsync(users);
                //await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Update(Users users)
        {
            try
            {
                if (users.Active == null)
                    users.Active = "Y";

                users.UserId = users.UserName;
                users.Password = Encription.EncryptString(users.Password);
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(users));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Users @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;


                //_dbContext.Entry(users).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> changePassword(UsersChangePassword users)
        {
            try
            {

                users.NewPassword = Encription.EncryptString(users.NewPassword);
                var Id = new SqlParameter("@id", users.UserId);
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(users));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", "5");

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Users @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;


                //_dbContext.Entry(users).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetUserDetails(int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_UserDetailForMain";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = UserId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string userdetail = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    userdetail = userdetail + dataTable.Rows[i][0].ToString();
                }
                return userdetail;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

public async Task<UserPermissionResponse> GetUserFinAdmin(int companyId, int branchId, int userId)
{
    try
    {
        var user = await _dbContext.tbl_Users
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.CompanyId == companyId &&
                (p.BranchId == branchId || p.BranchId == 0) &&
                p.Id == userId);

        if (user == null)
        {
            return new UserPermissionResponse
            {
                Key = "Error",
                Value = "This user does not exist"
            };
        }

        return new UserPermissionResponse
        {
            Key = "YearAdminRights",
            Value = user.YearClosingRights == 1 ? "Yes" : "No"
        };
    }
    catch (Exception ex)
    {
        Logger.ErrorLog(GetType().Name, nameof(GetUserFinAdmin), ex);
        return new UserPermissionResponse
        {
            Key = "Error",
            Value = "An internal error occurred"
        };
    }
}


    }
}
