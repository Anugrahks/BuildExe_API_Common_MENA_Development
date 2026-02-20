using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class AccountsReceivableRepository : IAccountsReceivableRepository
    {
        private readonly BasicContext _dbContext;
        public AccountsReceivableRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AccountsReceivable>> GetForReport(BasicSearch basicSearch)
        {
            try
            {
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(basicSearch));

                var _product = await _dbContext.tbl_AccountsReceivable.FromSqlRaw("stpro_AccountsReceivable @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @json", fromdate, todate, CompanyId, BranchId, FinancialYearId, item).ToListAsync();
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
