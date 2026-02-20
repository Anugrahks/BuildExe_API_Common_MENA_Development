using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SelectPdf;
using PdfDocument = SelectPdf.PdfDocument;
using System.Reflection;
using System.Data.Common;
using System.Data;
using System.ComponentModel.Design;

namespace BuildExeBasic.Repository
{
    public class EmailConfigurationRepository : IEmailConfigurationRepository
    {
        private readonly BasicContext _dbContext;
        public EmailConfigurationRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailConfiguration> Post(EmailConfiguration configuration)
        {
            try
            {

                var entity = await _dbContext.tbl_EmailConfiguration.AddAsync(configuration);
                await _dbContext.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<BulkMailWhatsAppSMS> PostBulk(BulkMailWhatsAppSMS configuration)
        {
            try
            {

                var entity = await _dbContext.tbl_BulkMailWhatsAppSMS.AddAsync(configuration);
                await _dbContext.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<BulkMailWhatsAppSMS> EditBulk(BulkMailWhatsAppSMS model)
        {
            try
            {
                var entity = await _dbContext.tbl_BulkMailWhatsAppSMS
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                if (entity != null)
                {
                    entity.CompanyId = model.CompanyId;
                    entity.BranchId = model.BranchId;
                    entity.FinancialYearId = model.FinancialYearId;

                    entity.UserId = model.UserId;
                    entity.Remarks = model.Remarks;
                    entity.IsDeleted = model.IsDeleted;

                    entity.ProspectIds = model.ProspectIds;
                    entity.ClientIds = model.ClientIds;
                    entity.SupplierIds = model.SupplierIds;
                    entity.LabourIds = model.LabourIds;
                    entity.GroupLabourIds = model.GroupLabourIds;
                    entity.ForemanIds = model.ForemanIds;
                    entity.SubcontractorIds = model.SubcontractorIds;
                    entity.ContractorIds = model.ContractorIds;
                    entity.EntryUserIds = model.EntryUserIds;
                    entity.EmployeeIds = model.EmployeeIds;

                    entity.EmailTemplateId = model.EmailTemplateId;
                    entity.EmailTemplateName = model.EmailTemplateName;

                    entity.WhatsappTemplateId = model.WhatsappTemplateId;
                    entity.WhatappTemplateName = model.WhatappTemplateName;

                    entity.SmsTemplateId = model.SmsTemplateId;
                    entity.SmsTemplateName = model.SmsTemplateName;

                    await _dbContext.SaveChangesAsync();
                    return entity;
                }

                return null;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task DeleteBulk(int id)
        {
            try
            {
                var entity = await _dbContext.tbl_BulkMailWhatsAppSMS
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity != null)
                {
                    entity.IsDeleted = 1;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name,
                                MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<BulkMailWhatsAppSMS>> GetListBulk(int CompanyId, int BranchId)
        {
            try
            {
                var emailConfigurations = await _dbContext.tbl_BulkMailWhatsAppSMS
                 .Where(e => e.CompanyId == CompanyId && e.BranchId == BranchId  && e.IsDeleted == 0)
                 .ToListAsync();
                return emailConfigurations;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<String> SendEmailAsync(int CompanyId, int BranchId, int MenuId, int Id, string file, string Content)
        {
            try
            {
                var mail = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.CompanyId == CompanyId && x.BranchId == BranchId && x.MenuId == MenuId && x.IsActive == true);
                var id = new SqlParameter("@Id", Id);
                if (mail != null)
                {
                    var MenuAndProcedureList = MenuAndProcedure.MenuAndProcedures;
                    foreach (var MenuAndProcedure in MenuAndProcedureList)
                    {
                        if (MenuId == MenuAndProcedure.MenuId)
                        {
                            var To = await GetProcedure(MenuAndProcedure.Procedure, id);
                            if (To != null)
                            {
                                if (IsEmailFormat(To))
                                {
                                    var mailBody = Content;
                                    mail.Content = mailBody;
                                    return await SendMail(mail, To, file);
                                }
                                return "Invalid Email";
                            }
                            return "Invalid Email";
                        }
                    }
                }
                return "Menu does contain Email Configuration";
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<String> GetContent(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                var entity = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.MenuId == MenuId && x.CompanyId == CompanyId && x.BranchId == BranchId && x.IsActive == true);
                return entity.Content;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmailConfiguration>> GetList(int CompanyId, int BranchId)
        {
            try
            {
                var emailConfigurations = await _dbContext.tbl_EmailConfiguration
                 .Where(e => e.CompanyId == CompanyId && e.BranchId == BranchId)
                 .ToListAsync();
                return emailConfigurations;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




        public async Task<String> ChangeStatus(int id, bool status)
        {
            try
            {
                var entity = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.Id == id);
                if (status)
                {
                    entity.IsActive = status;
                    var MenuId = entity.MenuId;
                    var Id = entity.Id;
                    var list = await _dbContext.tbl_EmailConfiguration.Where(x => x.MenuId == MenuId && x.Id != Id).ToListAsync();
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (item.IsActive)
                            {
                                item.IsActive = false;
                            }
                        }
                    }
                }
                else
                {
                    entity.IsActive = status;
                }



                await _dbContext.SaveChangesAsync();
                return "success";

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Boolean> CheckStatus(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                var enitity = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.MenuId == MenuId && x.CompanyId == CompanyId && x.BranchId == BranchId && x.IsActive == true);
                if (enitity == null)
                {
                    return false;
                }
                return enitity.IsActive;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<EmailConfiguration> EditEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            try
            {
                var entity = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.Id == emailConfiguration.Id);
                if (entity != null)
                {
                    entity.FromEmail = emailConfiguration.FromEmail;
                    entity.MenuId = emailConfiguration.MenuId;
                    entity.Subject = emailConfiguration.Subject;
                    entity.AppPassword = emailConfiguration.AppPassword;
                    entity.Content = emailConfiguration.Content;
                    entity.TemplateId = emailConfiguration.TemplateId;

                    await _dbContext.SaveChangesAsync();
                    return entity;
                }
                return null;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<String> GetTemplate(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                var template = await _dbContext.tbl_EmailConfiguration.FirstOrDefaultAsync(x => x.MenuId == MenuId && x.CompanyId == CompanyId && x.BranchId == BranchId && x.IsActive == true);
                if (template == null)
                {
                    return null;
                }
                return template.TemplateId.ToString();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private async Task<string> SendMail(EmailConfiguration mail, string To, string file)
        {
            var result = new SendMailResult();
            try
            {
                var Client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail.FromEmail, mail.AppPassword)
                };
                var mailMessage = new MailMessage(from: mail.FromEmail, to: To, subject: mail.Subject, body: mail.Content)
                {
                    IsBodyHtml = true
                };

                if (file != null)
                {
                    byte[] data = Convert.FromBase64String(file);
                    string htmlString = Encoding.UTF8.GetString(data);
                    htmlString = htmlString.Replace("�", "\u00A9");
                    HtmlToPdf converter = new HtmlToPdf();
                    PdfDocument doc = converter.ConvertHtmlString(htmlString);
                    string outputPath = Path.Combine("Upload", "Documents", $"Document_{DateTime.Now:yyyy_MM_dd_HH_mm_ss_fff}_report.pdf");
                    doc.Save(outputPath);
                    var attachment = new Attachment(outputPath);
                    mailMessage.Attachments.Add(attachment);
                    Client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            result.IsSuccess = true;
            return result.ToString();
        }

        public async Task<string> GetProcedure(string procedureName, SqlParameter id)
        {
            string sqlQuery = $"{procedureName} @Id";

            var To = (await _dbContext.tbl_EmailReciever.FromSqlRaw(sqlQuery, id).ToListAsync()).FirstOrDefault();
            if (string.IsNullOrEmpty(To.EmailId))
            {
                return null;
            }
            return To.EmailId;

        }

        static bool IsEmailFormat(string input)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(input);
        }

        public async Task<string> WhatsappSmsFetch(int Id, int MenuId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WhatsAppSmsFetch";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = MenuId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });

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

        public async Task<string> SendBulkEmailAsync(BulkEmailRequest request)
        {
            try
            {
                // 1. Get sender configuration
                var mailConfig = await _dbContext.tbl_EmailConfiguration
                    .FirstOrDefaultAsync(x =>
                        x.CompanyId == request.CompanyId &&
                        x.BranchId == request.BranchId && x.MenuId == 99999 &&
                        x.IsActive == true);

                if (mailConfig == null)
                    return "No email configuration found.";

                // 2. Split all recipients
                var recipients = request.ToEmails
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

                if (!recipients.Any())
                    return "No valid email addresses found.";

                // 3. Loop and send emails
                foreach (var email in recipients)
                {
                    await SendSingleMail(
                        mailConfig,
                        email,
                        request.Subject,
                        request.Body,
                        request.AttachmentBase64Html
                    );
                }

                return $"Emails sent successfully to {recipients.Count} recipients.";
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name,
                                MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        private async Task SendSingleMail(
    EmailConfiguration mailConfig,
    string toEmail,
    string subject,
    string body,
    string? attachmentHtmlBase64)
        {
            try
            {
                using var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mailConfig.FromEmail, mailConfig.AppPassword)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(mailConfig.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(toEmail);

                // Optional HTML -> PDF attachment
                if (!string.IsNullOrWhiteSpace(attachmentHtmlBase64))
                {
                    byte[] data = Convert.FromBase64String(attachmentHtmlBase64);
                    string html = Encoding.UTF8.GetString(data);

                    HtmlToPdf converter = new HtmlToPdf();
                    PdfDocument doc = converter.ConvertHtmlString(html);

                    string path = Path.Combine("Upload", "Documents",
                        $"Bulk_{DateTime.Now:yyyyMMddHHmmssfff}.pdf");

                    doc.Save(path);
                    message.Attachments.Add(new Attachment(path));
                }

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name,
                                MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> getDataForEmail(string FormType, int BranchId, string Ids)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_BulkDropdownData";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Ids", SqlDbType.NVarChar) { Value = Ids });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = FormType });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }

}
