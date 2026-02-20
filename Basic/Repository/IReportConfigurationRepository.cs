using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
   public interface IReportConfigurationRepository
    {
        Task<IEnumerable<ReportConfiguration >> Get();
        Task<IEnumerable<ReportConfiguration>> GetByID(int MenuId,int CompanyId,int BranchId);
        Task<string> GetFilterByID(int MenuId, int CompanyId, int BranchId);
        Task<string> GetFieldByID(int MenuId, int CompanyId, int BranchId);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ReportConfiguration> reportConfiguration );
        Task<IEnumerable<Validation>> Delete(int Id, int userID);
        Task Update(IEnumerable<ReportConfiguration> reportConfiguration );
        Task<IEnumerable<Validation>> InsertPrintable(IEnumerable<PrintableTemplate> template);
        Task<string> GetPrintableTemplate(int MenuId, int CompanyId =1, int BranchId = 2);
        Task<IEnumerable<ListReportName>> ListReportNames();
        Task<string> GetList(int MenuId, int BranchID);
    }
}
