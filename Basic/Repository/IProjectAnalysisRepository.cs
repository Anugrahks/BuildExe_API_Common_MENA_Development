using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IProjectAnalysisRepository
    {
        Task<string> ProjectAnalysisReport(BasicSearch basicSearch);
        Task<IEnumerable<ProjectAnalysisDetail>> ProjectAnalysisReportDetail(BasicSearch basicSearch);
        Task<IEnumerable<ProjectAnalysisDetail_Datewise>> ProjectAnalysisReportDetail_Datewise(BasicSearch basicSearch);
        Task<string> ProjectAnalysisReportPrint(BasicSearch basicSearch);
        Task<string> GetProjectGraph(int CompanyId, int Branchid, int ProjectId, int PageNumber, int RowsPerPage,int UnitId);

        Task<string> CashFlowGraph(int CompanyId, int Branchid, int FinancialYearId, int ProjectId, int UnitId);
        Task<string> DocumentUpload(BasicSearch basicSearch);

        Task<string> ProjectAnalysisReportPrintDashboard(BasicSearch basicSearch);
    }
}
