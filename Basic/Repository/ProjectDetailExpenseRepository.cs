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
using Newtonsoft.Json.Serialization;


namespace BuildExeBasic.Repository
{
    public class ProjectDetailExpenseRepository : IProjectDetailExpenseRepository
    {
        private readonly BasicContext _dbContext;
        public ProjectDetailExpenseRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1,
            selectDetail = 2
        }
        public async Task<string> ProjectDetailExpenseReport(BasicSearch basicSearch)
        {
            try
            {
                // Normalize defaults
                basicSearch.withDate ??= (basicSearch.FromDate == null || basicSearch.ToDate == null) ? 0 : 1;
                basicSearch.FromDate ??= Convert.ToDateTime("2000-01-01");
                basicSearch.ToDate ??= Convert.ToDateTime("2000-01-01");
                basicSearch.Category ??= 0;
                basicSearch.WorkName ??= 0;
                basicSearch.BlockId ??= 0;
                basicSearch.FloorId ??= 0;
                basicSearch.UnitId ??= 0;
                basicSearch.FinancialYearId ??= 0;
                basicSearch.Offset ??= 1;
                basicSearch.PageSize ??= 5000;

                using var conn = _dbContext.Database.GetDbConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = "stpro_ExpenseReportProjectDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = basicSearch.BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = basicSearch.FloorId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = basicSearch.UnitId });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = basicSearch.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@category", SqlDbType.Int) { Value = basicSearch.Category });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId", SqlDbType.Int) { Value = basicSearch.WorkName });
                cmd.Parameters.Add(new SqlParameter("@CategoryWise", SqlDbType.Int) { Value = basicSearch.CategoryWise });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = basicSearch.Offset });
                cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = basicSearch.PageSize });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.select });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var reportList = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>
                    {
                        ["HeadName"] = reader["HeadName"],
                        ["ExpenseData"] = JsonConvert.DeserializeObject(reader["ExpenseData"]?.ToString() ?? "[]")
                    };

                    reportList.Add(row);
                }

                return JsonConvert.SerializeObject(reportList, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<ProjectExpenseDetail>> ProjectExpenseReport(BasicSearch basicSearch)
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
                var Action = new SqlParameter("@Action", Actions.selectDetail);
                var EnquiryId = new SqlParameter("@EnquiryId", basicSearch.EnquiryId);

                var _product = await _dbContext.tbl_ProjectExpenseDetail.FromSqlRaw("stpro_ProjectDetailExpenseReport @ProjectId, @BlockId,@FloorId,@UnitId,@DivisionId ,@fromdate, @todate, @CompanyId, @BranchId,@category,@WorkNameId, @withDate, @Action, @EnquiryId", ProjectId, BlockId, FloorId, UnitId, DivisionId, fromdate, todate, CompanyId, BranchId, category, WorkNameId, withDate, Action, EnquiryId).ToListAsync();

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
