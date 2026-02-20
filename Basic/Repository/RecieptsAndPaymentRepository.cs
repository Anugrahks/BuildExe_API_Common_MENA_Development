using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class RecieptsAndPaymentRepository:IRecieptsAndPaymentRepository 
    {
        private readonly BasicContext _dbContext;
        public RecieptsAndPaymentRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1
        }
        public async Task <IEnumerable<DayBook>> RecieptsAndPayment(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                    basicSearch.ProjectId = 0;

                basicSearch.IsAllRnP ??= 0; // null replacer


                var json = basicSearch == null ? "{}" : JsonConvert.SerializeObject(basicSearch);

                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var projectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var Json = new SqlParameter("@json", json);
                var Action = new SqlParameter("@Action", Actions.select);
                var _product = await _dbContext.tbl_Daybook.FromSqlRaw("stpro_RecieptsAndPayment @fromdate, @todate, @CompanyId, @BranchId, @FinancialYearId,@ProjectId, @json, @Action", fromdate, todate, CompanyId, BranchId, FinancialYearId, projectId, Json, Action).ToListAsync();
                return _product;


                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> RecieptsAndPaymentReport(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_RecieptsReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = basicSearch.Action });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        
        public async Task<string> GetStartDateEndDateForProject(int ProjectId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_DefaultDates";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@SitemanagerId", SqlDbType.Int) { Value = 0 });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
