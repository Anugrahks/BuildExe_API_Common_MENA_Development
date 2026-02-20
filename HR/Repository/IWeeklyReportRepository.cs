using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IWeeklyReportRepository
    {
        Task<string> Report(HRSearch weeklyBill);
        Task<IEnumerable<Validation>> Insert(IEnumerable<WeeklyReportApproval> mat);
        Task<IEnumerable<Validation>> Update(IEnumerable<WeeklyReportApproval> mat);

        Task<string> GetPaymentSchedule(int CompanyId, int Branchid, int FinancialYearId);

        Task<string> PaymentScheduleList(int CompanyId, int Branchid,int UserId, int FinancialYearId);

        Task<string> PaymentScheduleApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<string> GetLastDate(int CompanyId, int Branchid, int FinancialYearId);
    }
}
