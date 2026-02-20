using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace BuildExeBasic.Repository
{
    public interface IPrintableReportConfigurationRepository
    {
        Task<PrintableReportConfiguration> GetByID(int Id);
        Task<string> GetByMenuID(int MenuId, int CompanyId, int BranchId);
        Task<IEnumerable<Validation>> Insert(PrintableReportConfiguration reportConfiguration);
        Task<IEnumerable<Validation>> Delete(int Id, int userID);
        Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationList();

        Task<string> GetPrintableReportConfigurationlistcompanybranch(int CompanyId, int BranchId);

        Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListById(int MenuId);

        Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListBycompanybranchId(int MenuId, int CompanyId, int BranchId);
        Task UpdatePrintableReportConfiguration(int Id, int MenuId, string TemplateName, string TemplateStructure, string WatermarkText, string PageSize);
        Task DeletePrintableReportConfiguration(int id);
        Task<PrintableReportImageConfiguration> GetPrintableReportConfigurationImage(int PrintableReportConfigurationId, int ImageType);
        Task PrintableReportConfigurationUpload(PrintableReportConfigurationImage configurations);

        Task<List<PurchaseOrderReprintModelView>> PurchaseOrderReprint(int ProjectId, int SupplierId, int ApprovedStatus, DateTime? toDate, DateTime? fromDate);

        Task<string> Getreprintmenu(BasicSearch basicSearch);

        Task<IEnumerable<PrintableReportConfigurationList>> GetPrintableReportConfigurationListBycompanybranchIddynamic(int MenuId, int CompanyId, int BranchId);

        Task<IEnumerable<Validation>> SetPrintableReportStyle(IEnumerable<PrintableReportCSS> cssText);
        Task<IEnumerable<PrintableReportCSS>> GetPrintableReportStyle(int reportid);
        Task<string> GetStaticReportData(int BranchId, int ReportId, int RecordId);


        Task<string> StaticPrintablePurchaseOrder(int BranchId, int ReportId, int RecordId);

    }
}
