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

namespace BuildExeBasic.Repository
{
    public class EmailSmsWhatsappRepository : IEmailSmsWhatsappRepository
    {
        private readonly BasicContext _dbContext;
        public EmailSmsWhatsappRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            SelectByType = 6,
            Selectjournal = 7,
            SelectBalance = 8,
            GetAllHeads = 9
        }

        public async Task<IEnumerable<Validation>> Update(EmailSmsWhatsapp emailsmswhatsapp)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var MenuId = new SqlParameter("@MenuId", emailsmswhatsapp.MenuId);
                var UserId = new SqlParameter("@UserId", emailsmswhatsapp.UserId);
                var CompanyId = new SqlParameter("@CompanyId", emailsmswhatsapp.CompanyId);
                var BranchId = new SqlParameter("@BranchId", emailsmswhatsapp.BranchId);
                var EmailId = new SqlParameter("@EmailId", emailsmswhatsapp.EmailId);
                var WhatsappNumber = new SqlParameter("@WhatsappNumber", emailsmswhatsapp.WhatsappNumber);
                var Sms = new SqlParameter("@Sms", emailsmswhatsapp.Sms);
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_EmailSmsWhatsapp @Id, @MenuId, @UserId, @CompanyId, @BranchId, @EmailId, @WhatsappNumber, @Sms, @Action", Id, MenuId, UserId, CompanyId, BranchId, EmailId, WhatsappNumber, Sms, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmailSmsWhatsappList>> getactivated(int CompanyId, int BranchId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var EmailId = new SqlParameter("@EmailId", "0");
                var WhatsappNumber = new SqlParameter("@WhatsappNumber", "0");
                var Sms = new SqlParameter("@Sms", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_EmailSmsWhatsappList.FromSqlRaw("stpro_EmailSmsWhatsapp @Id, @MenuId, @UserId, @CompanyId, @BranchId, @EmailId, @WhatsappNumber, @Sms, @Action", Id, MenuId, UserId, companyId, branchId, EmailId, WhatsappNumber, Sms, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmailSmsWhatsappList>> CheckStatus(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var menuId = new SqlParameter("@MenuId", MenuId);
                var UserId = new SqlParameter("@UserId", "0");
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var EmailId = new SqlParameter("@EmailId", "0");
                var WhatsappNumber = new SqlParameter("@WhatsappNumber", "0");
                var Sms = new SqlParameter("@Sms", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_EmailSmsWhatsappList.FromSqlRaw("stpro_EmailSmsWhatsapp @Id, @MenuId, @UserId, @CompanyId, @BranchId, @EmailId, @WhatsappNumber, @Sms, @Action", Id, menuId, UserId, companyId, branchId, EmailId, WhatsappNumber, Sms, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }


}
