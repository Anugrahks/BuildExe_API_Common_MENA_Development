using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mail ;
using System.Net;
using System.Data.SqlClient;
using System.Data.Common;
using BuildExeServices.Library;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BuildExeServices.Repository
{
    
    public class CompanyRepository: ICompanyRepository 
    {
        private readonly ProductContext _dbContext;
        public CompanyRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            delete =3
        }
        public async Task<IEnumerable<Validation>> DeleteCompany(int companyId)
        {
            try
            {
                //var company = await _dbContext.tbl_Companies.FindAsync(companyId);

                //_dbContext.tbl_Companies.Remove(company);
                //await _dbContext.SaveChangesAsync();

                var item = new SqlParameter("@item","");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Company @item,@CompanyId,@BranchId,@Action", item, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        /*       public async Task<Company> GetCompanyByID(int companyId)
               {
                   try
                   {
                       // Fetch the company from the database
                       var company = await _dbContext.tbl_Companies.FindAsync(companyId);

                       if (company != null)
                       {
                           // Check if WhatsappPassword exists
                           if (!string.IsNullOrEmpty(company.WhatsappPassword))
                           {
                               // Validate if the WhatsappPassword is a valid Base64 string
                               if (IsValidBase64(company.WhatsappPassword))
                               {
                                   // Decrypt the Whatsapp password if valid Base64
                                   company.WhatsappPassword = Encription.DecryptString(company.WhatsappPassword);
                               }
                               else
                               {
                                   // Optionally log an error if it's not valid Base64
                                   Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                                       new Exception("Invalid Base64 string in WhatsappPassword"));
                                   // You can leave it empty or set to a custom message (e.g., "Invalid password format")
                               }
                           }
                           else
                           {
                               // If no password is set, handle it as needed (e.g., return null or default message)
                               company.WhatsappPassword = null;
                           }
                       }

                       return company;
                   }
                   catch (Exception ex)
                   {
                       Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                       throw;
                   }
               }

               // Helper method to check if a string is a valid Base64 string
               private bool IsValidBase64(string str)
               {
                   // Base64 string must have a length that is a multiple of 4
                   if (str.Length % 4 != 0)
                       return false;

                   // Replace padding characters to ensure it's a proper Base64 string
                   str = str.TrimEnd('=');

                   // Check if the string contains only valid Base64 characters
                   foreach (char c in str)
                   {
                       if (!char.IsLetterOrDigit(c) && c != '+' && c != '/' && c != '=')
                           return false;
                   }

                   try
                   {
                       // Attempt to decode the string and ensure it's Base64 compliant
                       Convert.FromBase64String(str);
                       return true;
                   }
                   catch
                   {
                       return false;
                   }
               }


       */


        public async Task<Company> GetCompanyByID(int companyId)
        {
            try
            {
                var company = await _dbContext.tbl_Companies.FindAsync(companyId);

                if (company != null && !string.IsNullOrEmpty(company.WhatsappPassword))
                {
                    if (IsValidBase64(company.WhatsappPassword))
                    {
                        company.WhatsappPassword = Encription.DecryptString(company.WhatsappPassword);
                    }
                }

                return company;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsValidBase64(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length % 4 != 0)
                return false;

            try
            {
                // Convert.FromBase64String will throw an exception if the string is not valid Base64
                Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Company>> GetBranchByCompany(int companyId)
        {
            try
            {

                return await _dbContext.tbl_Companies
                    .Where(c => c.ParentCompanyid == companyId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Dictionary<string, string>> CurrencyName(int companyId)
        {
            try
            {
                var company = await _dbContext.tbl_Companies.FindAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Company not found.");
                }

                var result = new Dictionary<string, string>
        {
            { "CurrencyName", company.CurrencyName.ToUpper() },
            { "Unit", company.UnitName.ToUpper() }
        };

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Dictionary<string, int>> attendancetype(int companyId)
        {
            try
            {
                var company = await _dbContext.tbl_Companies.FindAsync(companyId);
                if (company == null)
                {
                    throw new Exception("There is not such Branch");
                }

                var result = new Dictionary<string, int>
        {
            { "AttendanceType", company.AttendanceType },
            { "islabourhourly", company.islabourhourly },
            { "ismonthlyhourly", company.ismonthlyhourly }
        };

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Company>> GetBranchBycompanyid(int companyId)
        {
            try
            {
                return await _dbContext.tbl_Companies.Where(p => p.ParentCompanyid == companyId).ToListAsync(); ;
                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Company>> Getlogincompany()
        {
            try
            {
               
                
                return await _dbContext.tbl_Companies.Where(p => p.IsBranch  == 0).Where(p => p.Status == 0).Where(p => p.IsHide == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Company>> Getlogincompanynothidden()
        {
            try
            {


                return await _dbContext.tbl_Companies.Where(p => p.IsBranch == 0).Where(p => p.Status == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Company>> GetloginBranch(int companyId)
        {
            try
            {
                return await _dbContext.tbl_Companies.Where(p => p.ParentCompanyid == companyId).Where(p => p.IsBranch == 1).Where(p => p.Status == 0).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
            public async Task<IEnumerable<Company>> Getcompany()
        {
            try
            {
                return  await _dbContext.tbl_Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> InsertCompany(Company company)
        {
            if (company.WhatsappPassword != null)
            {
                company.WhatsappPassword = Encription.EncryptString(company.WhatsappPassword);
            }

            //_dbContext.Add(company);
            // Save();
            try
            {
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(company));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Company @item,@CompanyId,@BranchId,@Action", item, CompanyId, BranchId, Action).ToListAsync();


                Validation response = (Validation)purchaseList.FirstOrDefault();

                if ((response.StatusCode == 1) && (company.IsBranch == 1))
                {
                    await DefaultAdmin(company, (int)response.Id, response.ErrorMessage, response.ErrorType);

                }

                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task DefaultAdmin(Company company, int Branchid, string email, string paword)
        {
           // string pasword = company.CompanyName + "123".Replace(" ", String.Empty);
            string pasword = company.CompanyName + "123";
            try
            {
                Users companuuser = new Users();
                companuuser.UserId = "Admin";
                companuuser.UserName = "Admin";
                pasword = company.CompanyName + "123";
                companuuser.Password = pasword;
                companuuser.UserGroupId = 1;
                companuuser.FullName = company.CompanyName + " Admin";
                companuuser.Active = "Y";
                companuuser.EmailId = company.EmailId;
                companuuser.Mobile = company.MobileNumber;

                companuuser.CompanyId = company.ParentCompanyid;
                companuuser.BranchId = Branchid;
                companuuser.SiteUser = 0;
                companuuser.IsAdmin = 1;
                companuuser.SuperUser = 0;

                UserRepository tt = new UserRepository(_dbContext);
                await tt.Insert(companuuser);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            // sendMail(company.EmailId, email, paword, "Detail TO Login into your BuildExe Application  <br />  Userid=Admin, Password=" + pasword);
        }
        public void sendMail(string tomail, string frommail, string pwd, string body)
        {
            try
            {
                MailMessage mailmessage = new MailMessage();
                SmtpClient Mailclient = new SmtpClient("smtp.gmail.com", 587);
                Mailclient.Credentials = new NetworkCredential(frommail, pwd);
                Mailclient.EnableSsl = true;
                mailmessage.IsBodyHtml = true;
                mailmessage.Body = body;
                mailmessage.Subject = "BUILDEXE PASSWORD To LOGIN";
                mailmessage.To.Add(new MailAddress(tomail));  /* Reciever-mail id */
                mailmessage.From = new MailAddress(frommail); /* Sender-mail id */
                Mailclient.Send(mailmessage);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task UpdateCompany(Company company)
        {
            if (company.WhatsappPassword != null)
            {
                company.WhatsappPassword = Encription.EncryptString(company.WhatsappPassword);
            }
            try
            {
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(company));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Company @item, @CompanyId, @BranchId, @Action",
                item, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Dictionary<string, object>> SiteLimit(int branchid)
        {
            try
            {
                var company = await _dbContext.tbl_Companies.FindAsync(branchid);
                if (company == null)
                {
                    throw new Exception("Company not found.");
                }

                var result = new Dictionary<string, object>
        {
            { "SiteLimit", company.SiteLimit },     // int
            { "LimitAmount", company.LimitAmount }  // decimal
        };

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<Validation>> CheckEditDelete(int companyId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckCompanyEditDelete @CompanyId", CompanyId).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<Dictionary<string, int>> BranchEnable(int companyId)
        //{
        //    return= 0;
        // }

        public async Task<Dictionary<string, int>> BatchEnable(int companyId)
        {
            try
            {
                using var command = _dbContext.Database.GetDbConnection().CreateCommand();
                command.CommandText = "dbo.Stpro_GetBatchEnabled";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                //command.Parameters.Add(new SqlParameter("@BranchId", branchId));

                await _dbContext.Database.OpenConnectionAsync();

                var result = await command.ExecuteScalarAsync();

                if (result == null)
                    throw new Exception("There is no such Branch");

                return new Dictionary<string, int>
        {
            { "Batch Enabled", Convert.ToInt32(result) }
        };
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
        }

    }
}
