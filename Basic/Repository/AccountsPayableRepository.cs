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
    public class AccountsPayableRepository : IAccountsPayableRepository
    {
        private readonly BasicContext _dbContext;
        public AccountsPayableRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AccountsPayable>> GetForReport(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId == null)
                {
                    basicSearch.ProjectId = 0;
                }

                if (basicSearch.CategoryIds == null)
                {
                    basicSearch.CategoryIds = "";
                }

                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var Categoryid = new SqlParameter("@CategoryId", basicSearch.CategoryId);
                var Categoryids = new SqlParameter("@CategoryIds", basicSearch.CategoryIds);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(basicSearch));

                if (basicSearch.ProjectWise == 2)
                {
                    var _product = await _dbContext.tbl_AccountsPayable.FromSqlRaw("stpro_AccountsPayableDefault @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @ProjectId, @CategoryId, @CategoryIds, @json", fromdate, todate, CompanyId, BranchId, FinancialYearId, ProjectId, Categoryid, Categoryids, item).ToListAsync();
                    return _product;
                }
                else
                {
                    var _product = await _dbContext.tbl_AccountsPayable.FromSqlRaw("stpro_AccountsPayable @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @ProjectId, @CategoryId, @CategoryIds, @json", fromdate, todate, CompanyId, BranchId, FinancialYearId, ProjectId, Categoryid, Categoryids, item).ToListAsync();
                    return _product;
                }
                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}