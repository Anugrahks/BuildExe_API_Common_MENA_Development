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
    public class ExpenseIncomeRepository : IExpenseIncomeRepository
    {
        private readonly BasicContext _dbContext;
        public ExpenseIncomeRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1,
            selectDetail = 2
        }
        public async Task<IEnumerable<ExpenseDetail>> ExpenseReport(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.Category is null)
                    basicSearch.Category = 0;

                if (basicSearch.WorkName is null)
                    basicSearch.WorkName = 0;

                if (basicSearch.BlockId is null)
                    basicSearch.BlockId = 0;

                if (basicSearch.FloorId is null)
                    basicSearch.FloorId = 0;

                if (basicSearch.UnitId is null)
                    basicSearch.UnitId = 0;

                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var BlockId = new SqlParameter("@BlockId", basicSearch.BlockId);
                var FloorId = new SqlParameter("@FloorId", basicSearch.FloorId);
                var UnitId = new SqlParameter("@UnitId", basicSearch.UnitId);
                var DivisionId = new SqlParameter("@DivisionId", basicSearch.DivisionId);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var category = new SqlParameter("@category", basicSearch.Category);
                var WorkNameId = new SqlParameter("@WorkNameId", basicSearch.WorkName);
                var withDate = new SqlParameter("@withDate", basicSearch.WithStock);
                var Action = new SqlParameter("@Action", Actions.select);
                var EnquiryId = new SqlParameter("@EnquiryId", basicSearch.EnquiryId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);

                var _product = await _dbContext.tbl_Expensedetail.FromSqlRaw("stpro_ExpenseReport @ProjectId, @BlockId,@FloorId,@UnitId,@DivisionId ,@fromdate, @todate, @CompanyId, @BranchId,@category,@WorkNameId, @withDate, @Action, @EnquiryId, @FinancialYearId", ProjectId, BlockId, FloorId, UnitId, DivisionId, fromdate, todate, CompanyId, BranchId, category, WorkNameId, withDate, Action, EnquiryId, FinancialYearId).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Expense>> IncomeReport(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }


                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);

                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var category = new SqlParameter("@category", "0");
                var withDate = new SqlParameter("@withDate", basicSearch.withDate);
                var Action = new SqlParameter("@Action", Actions.select);

                var _product = await _dbContext.tbl_Expense.FromSqlRaw("stpro_IncomeReport @ProjectId,@fromdate, @todate, @CompanyId, @BranchId,@category,@withDate, @Action", ProjectId, fromdate, todate, CompanyId, BranchId, category, withDate, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ExpenseDetail>> ExpenseDetailReport(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.Category is null)
                    basicSearch.Category = 0;

                if (basicSearch.WorkName is null)
                    basicSearch.WorkName = 0;

                if (basicSearch.BlockId is null)
                    basicSearch.BlockId = 0;

                if (basicSearch.FloorId is null)
                    basicSearch.FloorId = 0;

                if (basicSearch.UnitId is null)
                    basicSearch.UnitId = 0;


                if (basicSearch.FinancialYearId is null)
                    basicSearch.FinancialYearId = 0;

                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var BlockId = new SqlParameter("@BlockId", basicSearch.BlockId);
                var FloorId = new SqlParameter("@FloorId", basicSearch.FloorId);
                var UnitId = new SqlParameter("@UnitId", basicSearch.UnitId);
                var DivisionId = new SqlParameter("@DivisionId", basicSearch.DivisionId);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var category = new SqlParameter("@category", basicSearch.Category);
                var WorkNameId = new SqlParameter("@WorkNameId", basicSearch.WorkName);
                var withDate = new SqlParameter("@withDate", basicSearch.WithStock);
                var Action = new SqlParameter("@Action", Actions.selectDetail);
                var EnquiryId = new SqlParameter("@EnquiryId", basicSearch.EnquiryId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);

                var _product = await _dbContext.tbl_Expensedetail.FromSqlRaw("stpro_ExpenseReport @ProjectId, @BlockId,@FloorId,@UnitId,@DivisionId ,@fromdate, @todate, @CompanyId, @BranchId,@category,@WorkNameId, @withDate, @Action, @EnquiryId, @FinancialYearId", ProjectId, BlockId, FloorId, UnitId, DivisionId, fromdate, todate, CompanyId, BranchId, category, WorkNameId, withDate, Action, EnquiryId, FinancialYearId).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ExpenseDetail>> IncomeDetailReport(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }

                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);

                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var category = new SqlParameter("@category", "0");
                var withDate = new SqlParameter("@withDate", basicSearch.withDate);
                var Action = new SqlParameter("@Action", Actions.selectDetail);

                var _product = await _dbContext.tbl_Expensedetail.FromSqlRaw("stpro_IncomeReport @ProjectId,@fromdate, @todate, @CompanyId, @BranchId,@category,@withDate, @Action", ProjectId, fromdate, todate, CompanyId, BranchId, category, withDate, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> cashbalance(int CompanyId, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_StrengthReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> bankbalance(int CompanyId, int Branchid, int FinancialYearid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_StrengthReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        


            public async Task<string> IncomeReportNew(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_IncomeReportCategoryWise";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> bankbalancereciept(int CompanyId, int Branchid, int FinancialYearId, DateTime FromDate , DateTime ToDate)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_StrengthReportReciept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> odbalance(int CompanyId, int Branchid, int FinancialYearid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_StrengthReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
