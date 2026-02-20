using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BuildExeBasic.Repository
{
    public class PrintableReportConfigurationRepository : IPrintableReportConfigurationRepository
    {
        private readonly BasicContext _dbContext;
        public PrintableReportConfigurationRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Delete = 3,
            GetById = 4,
            GetByMenuId = 5,
            GetByMenu=6

        }
        public async Task<IEnumerable<Validation>> Insert(PrintableReportConfiguration reportConfiguration)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", reportConfiguration.MenuId);
                var CompanyId = new SqlParameter("@CompanyId", reportConfiguration.CompanyId);
                var BranchId = new SqlParameter("@BranchId", reportConfiguration.BranchId);
                var UserId = new SqlParameter("@UserId", reportConfiguration.UserId);
                var templateName = new SqlParameter("@TemplateName", reportConfiguration.TemplateName);
                var template = new SqlParameter("@Template", reportConfiguration.TemplateStructure);
                var watermarkText = new SqlParameter("@WatermarkText", reportConfiguration.WatermarkText);
                var pageSize = new SqlParameter("@PageSize", reportConfiguration.PageSize);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_PrintableReportConfigurations @MenuId, @CompanyId, @BranchId, @UserId, @Template, @TemplateName, @WatermarkText, @PageSize",
                    MenuId, CompanyId, BranchId, UserId, template, templateName, watermarkText, pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int UserID)
        {
            try
            {
                var id = new SqlParameter("@id", Id);
                var MenuId = new SqlParameter("@menuId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_PrintableReportConfiguration @id,@menuId, @item, @CompanyId, @BranchId, @UserId, @Action", id, MenuId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<PrintableReportConfiguration> GetByID(int id)
        {
            return await _dbContext.tbl_PrintableReportConfiguration
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> GetByMenuID(int menuid, int companyId, int branchId)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PrintableReportConfiguration";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = menuid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByMenuId });
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
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationList()
        {
            try
            {
                var Companyid = new SqlParameter("@CompanyId", "0");
                var Branchid = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", 1);
                var pritableReportConfigurationList = await _dbContext.tbl_PrintableReportConfigurationList.
                FromSqlRaw("sprc_listprintableconfigurationreport @CompanyId, @BranchId, @Action", Action, Companyid, Branchid)
                .ToListAsync();
                return pritableReportConfigurationList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationlistcompanybranch(int CompanyId, int BranchId)
        //{
        //    try
        //    {
        //        var Companyid = new SqlParameter("@CompanyId", CompanyId);
        //        var Branchid = new SqlParameter("@BranchId", BranchId);
        //        var Action = new SqlParameter("@Action", 2);
        //        return await _dbContext.tbl_PrintableReportConfigurationList.FromSqlRaw("sprc_listprintableconfigurationreport  @CompanyId, @BranchId, @Action", Companyid, Branchid, Action).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<string> GetPrintableReportConfigurationlistcompanybranch(int CompanyId, int BranchId)
        {
            try
            {
                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.sprc_listprintableconfigurationreport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                var result = new StringBuilder();
                await using var reader = await cmd.ExecuteReaderAsync();

                // SQL returns only one column (the JSON array)
                while (await reader.ReadAsync())
                {
                    result.Append(reader.GetString(0));
                }

                return result.Length > 0 ? result.ToString() : "[]";
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListById(int MenuId)
        {
            try
            {
                var companyid = new SqlParameter("@CompanyId", "0");
                var branchid = new SqlParameter("@BranchId", "0");
                var menuId = new SqlParameter("@MenuId", MenuId);
                var action = new SqlParameter("@Action", 1);
                var pritableReportConfigurationListById = await _dbContext.tbl_PrintableReportConfigurationList.
                    FromSqlRaw("stpro_PrintableConfigurationReportById @MenuId, @CompanyId, @BranchId, @Action", menuId, companyid, branchid, action)
                    .ToListAsync();
                return pritableReportConfigurationListById;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListBycompanybranchId(int MenuId, int CompanyId,int BranchId)
        {
            try
            {
                var menuId = new SqlParameter("@MenuId", MenuId);
                var companyid = new SqlParameter("@CompanyId", CompanyId);
                var branchid = new SqlParameter("@BranchId", BranchId);
                var action = new SqlParameter("@Action", 2);
                var pritableReportConfigurationListById = await _dbContext.tbl_PrintableReportConfigurationList.
                    FromSqlRaw("stpro_PrintableConfigurationReportById @MenuId, @CompanyId, @BranchId, @Action", menuId, companyid, branchid, action)
                    .ToListAsync();
                return pritableReportConfigurationListById;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListBycompanybranchIddynamic(int MenuId, int CompanyId, int BranchId)
        {
            try
            {
                var menuId = new SqlParameter("@MenuId", MenuId);
                var companyid = new SqlParameter("@CompanyId", CompanyId);
                var branchid = new SqlParameter("@BranchId", BranchId);
                var action = new SqlParameter("@Action", 6);
                var pritableReportConfigurationListById = await _dbContext.tbl_PrintableReportConfigurationList.
                    FromSqlRaw("stpro_PrintableConfigurationReportById @MenuId, @CompanyId, @BranchId, @Action", menuId, companyid, branchid, action)
                    .ToListAsync();
                return pritableReportConfigurationListById;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task UpdatePrintableReportConfiguration(int Id, int MenuId, string TemplateName, string TemplateStructure, string WatermarkText,  string PageSize)
        {
            try
            {
                var printableReportConfiguration = _dbContext.tbl_PrintableReportConfiguration.FirstOrDefault(x => x.Id == Id);
                printableReportConfiguration.TemplateStructure = TemplateStructure;
                printableReportConfiguration.TemplateName = TemplateName;
                printableReportConfiguration.WatermarkText = WatermarkText;
                printableReportConfiguration.PageSize = PageSize;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task DeletePrintableReportConfiguration(int id)
        {
            try
            {
                var entityToDelete = _dbContext.tbl_PrintableReportConfiguration.FirstOrDefault(x => x.Id == id);
                if (entityToDelete != null)
                {
                    _dbContext.tbl_PrintableReportConfiguration.Remove(entityToDelete);
                    _dbContext.SaveChanges();
                }

                return;



            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<PrintableReportImageConfiguration> GetPrintableReportConfigurationImage(int PrintableReportConfigurationId, int ImageType)
        {
            try
            {
                var Pid = new SqlParameter("@PId", PrintableReportConfigurationId);
                var imageType = new SqlParameter("@ImageType", ImageType);
                return _dbContext.tbl_PrintableReportConfigurationImage.FirstOrDefault(x => x.PrintableReportConfigurationId == PrintableReportConfigurationId && x.ImageType == ImageType);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task PrintableReportConfigurationUpload(PrintableReportConfigurationImage configurations)
        {
            try
            {
                //var Id = new SqlParameter("@Id", configurations.Id);
                var PrintableReportConfigurationId = new SqlParameter("@PrintableReportConfigurationId", configurations.PrintableReportConfigurationId);
                var Text = new SqlParameter("@Text", configurations.Text);
                var Image = new SqlParameter("@Image", configurations.fileName);
                var Margin = new SqlParameter("@Margin", configurations.Margin);
                var Width = new SqlParameter("@Width", configurations.Width);
                var Height = new SqlParameter("@Height", configurations.Height);
                var FontSize = new SqlParameter("@FontSize", configurations.FontSize);
                var Color = new SqlParameter("@Color", configurations.Color);
                var ImageType = new SqlParameter("@ImageType", configurations.ImageType);

                var newConfiguration = new PrintableReportImageConfiguration()
                {
                    PrintableReportConfigurationId = configurations.PrintableReportConfigurationId,
                    Text = configurations.Text,
                    Image = configurations.fileName,
                    Margin = configurations.Margin,
                    Width = configurations.Width,
                    Height = configurations.Height,
                    FontSize = configurations.FontSize,
                    Color = configurations.Color,
                    ImageType = configurations.ImageType
                };
                _dbContext.tbl_PrintableReportConfigurationImage.Add(newConfiguration);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<List<PurchaseOrderReprintModelView>> PurchaseOrderReprint(int ProjectId, int SupplierId, int ApprovedStatus, DateTime? toDate, DateTime? fromDate)
        {
            try
            {
                var projectIdParam = new SqlParameter("@ProjectId", ProjectId);
                var supplierIdParam = new SqlParameter("@SupplierId", SupplierId);
                var approvedStatusParam = new SqlParameter("@ApprovedStatus", ApprovedStatus);
                var toDateParam = new SqlParameter("@ToDate", toDate.HasValue ? (object)toDate.Value : DBNull.Value);
                var fromDateParam = new SqlParameter("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value);

                var result = await _dbContext.tbl_PurchaseOrderReprintModelView
                    .FromSqlRaw("stpro_ReprintPurchaseOrder @ProjectId, @SupplierId,@ApprovedStatus, @ToDate, @FromDate",
                        projectIdParam, supplierIdParam, approvedStatusParam, toDateParam, fromDateParam).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getreprintmenu(BasicSearch basicSearch)
        {
            try
            {

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ReprintbyMenu";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = basicSearch.MenuId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string reprintdet = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    reprintdet = reprintdet + dataTable.Rows[i][0].ToString();
                }
                return reprintdet;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }





            /*
                        try
                        {
                            var projectIdParam = new SqlParameter("@ProjectId", ProjectId);
                            var supplierIdParam = new SqlParameter("@SupplierId", SupplierId);
                            var approvedStatusParam = new SqlParameter("@ApprovedStatus", ApprovedStatus);
                            var toDateParam = new SqlParameter("@ToDate", toDate.HasValue ? (object)toDate.Value : DBNull.Value);
                            var fromDateParam = new SqlParameter("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value);

                            var result = await _dbContext.tbl_PurchaseOrderReprintModelView
                                .FromSqlRaw("stpro_ReprintPurchaseOrder @ProjectId, @SupplierId,@ApprovedStatus, @ToDate, @FromDate",
                                    projectIdParam, supplierIdParam, approvedStatusParam, toDateParam, fromDateParam).ToListAsync();

                            return result;
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        */
        }

        public async Task<IEnumerable<Validation>> SetPrintableReportStyle(IEnumerable<PrintableReportCSS> cssText)
        {
            try
            {
                var ReportId = new SqlParameter("@ReportId", SqlDbType.Int) { Value = 0 };
                var jsonObject = new SqlParameter("@Item", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(cssText) };
                var Action = new SqlParameter("@Action", SqlDbType.Int) { Value = 1 };
                var output = await _dbContext.tbl_validation.FromSqlRaw("Stpro_PrintableReportStyle @ReportId, @Item, @Action", ReportId, jsonObject, Action).ToListAsync();
                return output;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PrintableReportCSS>> GetPrintableReportStyle(int id)
        {
            try
            {
                var ReportId = new SqlParameter("@ReportId", SqlDbType.Int) { Value = id };
                var jsonObject = new SqlParameter("@Item", SqlDbType.NVarChar) { Value = "" };
                var Action = new SqlParameter("@Action", SqlDbType.Int) { Value = 5 };
                var output = await _dbContext.tbl_PrintableReportStyle.FromSqlRaw("Stpro_PrintableReportStyle @ReportId, @Item, @Action", ReportId, jsonObject, Action).ToListAsync();
                return output;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetStaticReportData(int BranchId, int ReportId, int RecordId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_StaticReportsInPrintableReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@ReportId", SqlDbType.Int) { Value = ReportId });
                cmd.Parameters.Add(new SqlParameter("@RecordId", SqlDbType.Int) { Value = RecordId });
                if (cmd.Connection.State != ConnectionState.Open) {
                    cmd.Connection.Open();
                }
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string ReportData = "";
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    ReportData = ReportData + dataTable.Rows[i][0].ToString();
                }
                if (ReportData == "")
                    ReportData = "[]";
                return ReportData;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> StaticPrintablePurchaseOrder(int BranchId, int ReportId, int RecordId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_StaticReportsInPurchaseOrder";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@ReportId", SqlDbType.Int) { Value = ReportId });
                cmd.Parameters.Add(new SqlParameter("@RecordId", SqlDbType.Int) { Value = RecordId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string ReportData = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ReportData = ReportData + dataTable.Rows[i][0].ToString();
                }
                if (ReportData == "")
                    ReportData = "[]";
                return ReportData;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
