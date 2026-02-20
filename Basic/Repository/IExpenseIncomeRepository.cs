using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IExpenseIncomeRepository
    {
        Task<IEnumerable<ExpenseDetail>> ExpenseReport(BasicSearch basicSearch);
        Task<IEnumerable<ExpenseDetail>> ExpenseDetailReport(BasicSearch basicSearch);
        Task<IEnumerable<Expense>> IncomeReport(BasicSearch basicSearch);


        Task<string> IncomeReportNew(BasicSearch basicSearch);

        
        Task<IEnumerable<ExpenseDetail>> IncomeDetailReport(BasicSearch basicSearch);
        Task<string> cashbalance(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> bankbalance(int CompanyId, int Branchid, int FinancialYearId);
        Task<string> bankbalancereciept(int CompanyId, int Branchid, int FinancialYearId, DateTime FromDate , DateTime ToDate);
        Task<string> odbalance(int CompanyId, int Branchid, int FinancialYearId);


    }
}
