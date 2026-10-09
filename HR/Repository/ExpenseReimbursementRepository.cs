using BuildExeHR.Common;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildExeHR.Repository
{
    public class ExpenseReimbursementRepository : IExpenseReimbursementRepository
    {
        private readonly HRContext _dbContext;

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            SelectForApproval = 9,
            Search = 11
        }

        public ExpenseReimbursementRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }






        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ExpenseReimbursement> items)
        {
            try
            {
                var first = items.FirstOrDefault();

                var idParam = new SqlParameter("@Id", SqlDbType.Int) { Value = 0 };
                var jsonParam = new SqlParameter("@Json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(items) };
                var companyIdParam = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = first?.CompanyId ?? 0 };
                var branchIdParam = new SqlParameter("@BranchId", SqlDbType.Int) { Value = first?.BranchId ?? 0 };
                var monthIdParam = new SqlParameter("@MonthId", SqlDbType.Int) { Value = first?.MonthId ?? 0 };
                var yearIdParam = new SqlParameter("@YearId", SqlDbType.Int) { Value = first?.FinancialYearId ?? 0 };
                var userIdParam = new SqlParameter("@UserId", SqlDbType.Int) { Value = first?.UserId ?? 0 };
                var actionParam = new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Insert };

                return await _dbContext.tbl_validation.FromSqlRaw(
                    "EXEC dbo.Stpro_EmployeeExpenseReimbursement @Id, @Json, @CompanyId, @BranchId, @MonthId, @YearId, @UserId, @Action",
                    idParam, jsonParam, companyIdParam, branchIdParam, monthIdParam, yearIdParam, userIdParam, actionParam
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ExpenseReimbursement> items)
        {
            try
            {
                var first = items.FirstOrDefault();

                var idParam = new SqlParameter("@Id", SqlDbType.Int) { Value = first?.Id ?? 0 };
                var jsonParam = new SqlParameter("@Json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(items) };
                var companyIdParam = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = first?.CompanyId ?? 0 };
                var branchIdParam = new SqlParameter("@BranchId", SqlDbType.Int) { Value = first?.BranchId ?? 0 };
                var monthIdParam = new SqlParameter("@MonthId", SqlDbType.Int) { Value = first?.MonthId ?? 0 };
                var yearIdParam = new SqlParameter("@YearId", SqlDbType.Int) { Value = first?.FinancialYearId ?? 0 };
                var userIdParam = new SqlParameter("@UserId", SqlDbType.Int) { Value = first?.UserId ?? 0 };
                var actionParam = new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Update };

                return await _dbContext.tbl_validation.FromSqlRaw(
                    "EXEC dbo.Stpro_EmployeeExpenseReimbursement @Id, @Json, @CompanyId, @BranchId, @MonthId, @YearId, @UserId, @Action",
                    idParam, jsonParam, companyIdParam, branchIdParam, monthIdParam, yearIdParam, userIdParam, actionParam
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int userId)
        {
            try
            {
                var idParam = new SqlParameter("@Id", id);
                var jsonParam = new SqlParameter("@Json", string.Empty);
                var companyIdParam = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 };
                var branchIdParam = new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 };
                var monthIdParam = new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 };
                var yearIdParam = new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 };
                var userIdParam = new SqlParameter("@UserId", userId);
                var actionParam = new SqlParameter("@Action", Actions.Delete);

                var result = await _dbContext.tbl_validation
                    .FromSqlRaw(
                        "EXEC Stpro_EmployeeExpenseReimbursement @Id, @Json, @CompanyId, @BranchId, @MonthId, @YearId, @UserId, @Action",
                        idParam, jsonParam, companyIdParam, branchIdParam, monthIdParam, yearIdParam, userIdParam, actionParam
                    )
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> GetById(int id, int companyId, int branchId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_EmployeeExpenseReimbursement";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Select });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                var sb = new StringBuilder();
                foreach (DataRow row in dataTable.Rows)
                {
                    sb.Append(row[0].ToString());
                }

                return sb.Length == 0 ? "{}" : sb.ToString();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetAll(int companyId, int branchId, int financialYearId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_EmployeeExpenseReimbursement";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Json", SqlDbType.NVarChar) { Value = string.Empty });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@YearId", SqlDbType.Int) { Value = financialYearId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectAll }); // 5

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                var result = new StringBuilder();
                foreach (DataRow row in dataTable.Rows)
                {
                    result.Append(row[0].ToString());
                }

                return result.Length == 0 ? "[]" : result.ToString();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}