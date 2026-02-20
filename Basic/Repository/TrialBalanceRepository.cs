using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class TrialBalanceRepository:ITrialBalanceRepository 
    {
        private readonly BasicContext _dbContext;
        public TrialBalanceRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1
        }
        public async Task<IEnumerable<TrialBalance >> TrialBalance(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                basicSearch.ProjectId = 0;
                if (basicSearch.FromDate == null)
                    basicSearch.FromDate = new DateTime(2001, 1, 1);
                if (basicSearch.ToDate == null)
                    basicSearch.ToDate = new DateTime(2001, 1, 1);
                if (basicSearch.Type is null)
                    basicSearch.Type = 0;
                if (basicSearch.Type is null)
                    basicSearch.Type = 0;

                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
            var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
            var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
            var financialyearId = new SqlParameter("@financialyearId", basicSearch.FinancialYearId);
            var fromDate = new SqlParameter("@fromdate", basicSearch.FromDate);
            var toDate = new SqlParameter("@todate", basicSearch.ToDate);
                var type = new SqlParameter("@Type", basicSearch.Type);
                var _product = await _dbContext.tbl_TrialBalance.FromSqlRaw("stpro_TrialBalance @CompanyId, @BranchId,@ProjectId, @financialyearId,@fromdate,@todate,@Type", CompanyId, BranchId, ProjectId, financialyearId,fromDate,toDate, type).ToListAsync ();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProfitandLoss>> ProfitAndLoss(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                basicSearch.ProjectId = 0;
                if (basicSearch.FromDate == null)
                    basicSearch.FromDate = new DateTime(2001, 1, 1);
                if (basicSearch.ToDate == null)
                    basicSearch.ToDate = new DateTime(2001, 1, 1);

                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
            var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
            var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
            var financialyearId = new SqlParameter("@financialyearId", basicSearch.FinancialYearId);
            var toDate = new SqlParameter("@todate", basicSearch.ToDate);
            var grouping = new SqlParameter("@Grouping", basicSearch.Grouping);
                var _product = await  _dbContext.tbl_ProjectAndLoss.FromSqlRaw("stpro_ProfitAndLoss @CompanyId, @BranchId,@ProjectId,@financialyearId,@todate,@Grouping", CompanyId, BranchId, ProjectId, financialyearId,toDate, grouping).ToListAsync();
                return _product;
                }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<BalanceSheet>> BalanceSheet(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                    basicSearch.ProjectId = 0;
                if (basicSearch.FromDate == null)
                    basicSearch.FromDate = new DateTime(2001, 1, 1);
                if (basicSearch.ToDate == null)
                    basicSearch.ToDate = new DateTime(2001, 1, 1);

                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var financialyearId = new SqlParameter("@financialyearId", basicSearch.FinancialYearId);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var _product = await _dbContext.tbl_BalanceSheet.FromSqlRaw("stpro_BalanceSheet  @CompanyId, @BranchId,@ProjectId, @financialyearId,@fromdate,@todate",  CompanyId, BranchId, ProjectId, financialyearId,fromdate,todate).ToListAsync ();
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
