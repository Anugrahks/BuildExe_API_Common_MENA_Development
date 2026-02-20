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
using Newtonsoft.Json;

namespace BuildExeBasic.Repository
{
    public class EmailSmsWhatsappActivationRepository : IEmailSmsWhatsappActivationRepository
    {
        private readonly BasicContext _dbContext;
        public EmailSmsWhatsappActivationRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Get = 4,
            InsertDivision = 5,
            UpdateDivision = 8,
            UpdateWhatsapp = 10,
            status=11,
        }
        public async Task<string> EmailSmsWhatsappActivation(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmailSmsWhatsappActivation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });

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


        public async Task<IEnumerable<Validation>> Update(IEnumerable<EmailSmsWhatsappActivation> emailSmsWhatsappActivation)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(emailSmsWhatsappActivation));
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmailSmsWhatsappActivation @MenuId, @CompanyId, @BranchId,@json, @Action", MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> SavePath(string file)
        {
            try
            {
                if (!string.IsNullOrEmpty(file))
                {
                    // Decode the base64 string to get the HTML content
                    byte[] data = Convert.FromBase64String(file);
                    string htmlString = Encoding.UTF8.GetString(data);

                    // Replace special characters if needed
                    htmlString = htmlString.Replace("�", "\u00A9");

                    // Initialize HTML to PDF converter
                    HtmlToPdf converter = new HtmlToPdf();

                    // Convert the HTML string to a PDF document
                    PdfDocument doc = converter.ConvertHtmlString(htmlString);

                    // Generate the output file path
                    string outputPath = Path.Combine("Upload", "Documents", $"Document_{DateTime.Now:yyyy_MM_dd_HH_mm_ss_fff}_report.pdf");

                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                    // Save the PDF document
                    doc.Save(outputPath);

                    // Return the output path
                    return outputPath;
                }
                else
                {
                    throw new ArgumentException("File content is null or empty.", nameof(file));
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Post(EmailSmsWhatsappActivation emailSmsWhatsappActivation)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(emailSmsWhatsappActivation));
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmailSmsWhatsappActivation @MenuId, @CompanyId, @BranchId,@json, @Action", MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


     
        public async Task<IEnumerable<Validation>> PutWhatsapp(EmailSmsWhatsappActivation emailSmsWhatsappActivation)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(emailSmsWhatsappActivation));
                var Action = new SqlParameter("@Action", Actions.UpdateWhatsapp);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmailSmsWhatsappActivation @MenuId, @CompanyId, @BranchId,@json, @Action", MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> PutStatus(EmailSmsWhatsappActivation emailSmsWhatsappActivation)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", emailSmsWhatsappActivation.MenuId);
                var CompanyId = new SqlParameter("@CompanyId", emailSmsWhatsappActivation.Id);
                var BranchId = new SqlParameter("@BranchId", emailSmsWhatsappActivation.IsActive);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(emailSmsWhatsappActivation));
                var Action = new SqlParameter("@Action", Actions.status);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmailSmsWhatsappActivation @MenuId, @CompanyId, @BranchId,@json, @Action", MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> WhatsAppConfiguration(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmailSmsWhatsappActivation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });

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

        public async Task<string> GetByMenu(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmailSmsWhatsappActivation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = MenuId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });

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

    }
}
