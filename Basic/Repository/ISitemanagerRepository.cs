using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;

namespace BuildExeBasic.Repository
{
   public interface ISitemanagerRepository
    {
        Task<IEnumerable<Sitemanager>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<SitemanagerList>> GetForEdit(int CompanyId, int Branchid,int TransactionType);
        Task<IEnumerable<SitemanagerList>> GetForEdituser(int CompanyId, int Branchid, int TransactionType, int UserId, int FinancialYearId);
        Task<IEnumerable<SitemanagerList>> GetForapproval(int CompanyId, int Branchid, int TransactionType,int userID, int FinancialYearId);
        Task<IEnumerable<Sitemanager>> GetByID(int Id);
        decimal SitemanagerBalance(int sitemanagerId, int FinancialYearID);
        Task<IEnumerable<Validation>> Insert(Sitemanager sitemanager);
        Task<IEnumerable<Validation>> Delete(int Id,int userId);
        Task<IEnumerable<Validation>> Update(Sitemanager sitemanager);
        Task<string> SitemanagerBalance(int sitemanagerId, int CompanyId, int BranchId, int FinancialYearID);
        Task<string> SitemanagerBalance_Final(int sitemanagerId, int CompanyId, int BranchId, int FinancialYearIDt);
        Task<string> GetLedger(BasicSearch basicSearch);
        Task<string> GetReport(BasicSearch basicSearch);
        Task<string> GetAdvanceLedger(BasicSearch basicSearch);
        Task<string> GetLoanLedger(BasicSearch basicSearch);

        Task<IEnumerable<Validation>> InsertSiteExpense(IEnumerable<SiteExpense> specificationMasters);
        Task<IEnumerable<Validation>> UpdateSiteExpense(IEnumerable<SiteExpense> specificationMasters);
        Task<IEnumerable<Validation>> DeleteSiteExpense(int ID, int userId);
        Task<string> getusersiteexpense(int CompanyId, int Branchid, int UserId, int FinancialYearId);

        Task<string> getforApprovalsiteexpense(int CompanyId, int Branchid, int userid, int FinancialYearId);

        Task<string> Getbyidsiteexpense(int id);
    }
}
