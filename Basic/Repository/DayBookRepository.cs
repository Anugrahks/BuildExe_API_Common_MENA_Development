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
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;

namespace BuildExeBasic.Repository
{
    public class DayBookRepository : IDayBookRepository
    {
        private readonly BasicContext _dbContext;
        public DayBookRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
           select =1,
           selectforsummary =2,
           selectforcredit =3
        }
        public async Task<IEnumerable<DayBook>> GetForReport(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId == null)
                {
                    basicSearch.ProjectId = 0;
                }
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.select);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var _product = await _dbContext.tbl_Daybook.FromSqlRaw("stpro_Daybook @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @UserId,@Action,@ProjectId", fromdate, todate, CompanyId, BranchId, FinancialYearId, UserId, Action,ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<DayBookSummary>> GetForSummaryReport(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId == null)
                {
                    basicSearch.ProjectId = 0;
                }
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.selectforsummary);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var _product = await _dbContext.tbl_DaybookSummary.FromSqlRaw("stpro_Daybook @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @UserId,@Action,@ProjectId", fromdate, todate, CompanyId, BranchId, FinancialYearId, UserId, Action,ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetForSummaryandDetailReport(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId == null)
                {
                    basicSearch.ProjectId = 0;
                }
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_Daybook";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        
        public async Task<IEnumerable<FundFlowSummary>> GetForFundFlowReportSummary(BasicSearch basicSearch)
        {
            try
            {
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var Action = new SqlParameter("@Action", Actions.selectforsummary);
                var _product = await _dbContext.tbl_FundFlowSummary.FromSqlRaw("stpro_FundFlowReportNew @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @Action", fromdate, todate, CompanyId, BranchId, FinancialYearId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<FundFlowSummary>> GetForFundFlowReportCredit(BasicSearch basicSearch)
        {
            try
            {
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var Action = new SqlParameter("@Action", Actions.selectforcredit);
                var _product = await _dbContext.tbl_FundFlowSummary.FromSqlRaw("stpro_FundFlowReportNew @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @Action", fromdate, todate, CompanyId, BranchId, FinancialYearId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<FundFlowSummary>> GetForFundFlowReport(BasicSearch basicSearch)
        {
            try
            {
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var Action = new SqlParameter("@Action", Actions.select);
                var _product = await _dbContext.tbl_FundFlowSummary.FromSqlRaw("stpro_FundFlowReportNew @fromdate, @todate, @CompanyId, @BranchId,@FinancialYearId, @Action", fromdate, todate, CompanyId, BranchId, FinancialYearId,Action).ToListAsync();
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
