using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert> >Get(int UserId,DateTime today,int companyid,int BranchId);
        Task<IEnumerable<Alertfew>> Getfew(int UserId, DateTime today, int companyid, int BranchId);
        Task<IEnumerable<AlertCountNew>> Getcount(int UserId, DateTime today, int companyid, int BranchId, int FinancialYearId);
        Task<string> GetcountIndividualCount(int AlertType, int UserId, DateTime today, int companyid, int BranchId, int FinancialYearId);
        
        Task<IEnumerable<AlertCount>> GetcountIonic(int UserId, int companyid, int BranchId);
        Task<string> GettodaysActivity( int userId, DateTime Todate, int companyid, int branchId);
        Task<string> GettodaysActivityAdmin(int userId, int companyid, int branchId, DateTime fromdate, DateTime todate);
        Task<IEnumerable<Alert>> GetwithType(int userId, DateTime today, int companyid, int branchId, int Type);

        Task<string> GetwithTypeThree(int userId, DateTime today, int companyid, int branchId, int Type, int Forms);
        
        Task<string> ActivityStatus(BasicSearch basicSearch);

        Task<string> GetAlertForClosing(int CompanyId, int BranchId, int FinancialYearId);
        Task<string> ActivityStatusSearch(BasicSearch basicSearch);
    }
}
