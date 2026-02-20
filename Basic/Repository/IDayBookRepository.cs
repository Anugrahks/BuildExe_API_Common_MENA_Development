using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IDayBookRepository
    {
       Task  <IEnumerable<DayBook>> GetForReport(BasicSearch basicSearch);
        Task<IEnumerable<FundFlowSummary>> GetForFundFlowReport(BasicSearch basicSearch);
       Task<IEnumerable<DayBookSummary>> GetForSummaryReport(BasicSearch basicSearch);

        Task<IEnumerable<FundFlowSummary>> GetForFundFlowReportSummary(BasicSearch basicSearch);

        Task<IEnumerable<FundFlowSummary>> GetForFundFlowReportCredit(BasicSearch basicSearch);
        Task<string> GetForSummaryandDetailReport(BasicSearch basicSearch);
    }
}
