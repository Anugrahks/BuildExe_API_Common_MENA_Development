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
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace BuildExeBasic.Repository
{
    public class GeneralLedgerRepository:IGeneralLedgerRepository 
    {
        private readonly BasicContext _dbContext;
        public GeneralLedgerRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1
        }

        public async Task<string> LedgerMeerging(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.ClientName ??= string.Empty;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LedgerMergingg";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@AccountHeadId", SqlDbType.NVarChar) { Value = basicSearch.HeadName });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.NVarChar) { Value = basicSearch.EmployeeName });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@VirtualEntry", SqlDbType.Int) { Value = basicSearch.IsVirtual });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@AccountGroupId", SqlDbType.NVarChar) { Value = basicSearch.AccountGroupName });
                cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.NVarChar) { Value = basicSearch.ClientName });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.select });

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
                {
                    purcasedetails = "[]";
                }

                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GeneralLedger>> GeneralLedger(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                    basicSearch.ProjectId = 0;
                if (basicSearch.Retentionpercentage is null)
                    basicSearch.Retentionpercentage = 0;
                if (basicSearch.Label is null)
                    basicSearch.Label = "";

                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var AccountHeadId = new SqlParameter("@AccountHeadId", basicSearch.AccountHeadId);


                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var withopening = new SqlParameter("@withopening", basicSearch.WithOpening);
                var Action = new SqlParameter("@Action", Actions.select);
                var retper = new SqlParameter("@RetPer", basicSearch.Retentionpercentage);
                var label = new SqlParameter("@Label", basicSearch.Label);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(basicSearch));
                var _product = await _dbContext.tbl_GeneralLedger.FromSqlRaw("stpro_GeneralLedger @fromdate, @todate,@AccountHeadId,@ProjectId, @CompanyId, @BranchId,@FinancialYearId, @withopening, @Action,@Label,@RetPer, @json", fromdate, todate, AccountHeadId, ProjectId, CompanyId, BranchId, FinancialYearId, withopening, Action, label, retper, item).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GeneralLedger>> PersonalLedger(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                    basicSearch.ProjectId = 0;

                if (basicSearch.ClientId is null)
                    basicSearch.ClientId = 0;

                if (basicSearch.EmployeeId is null)
                    basicSearch.EmployeeId = 0;

                if (basicSearch.SupplierId is null)
                    basicSearch.SupplierId = 0;

                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var Client = new SqlParameter("@Client", basicSearch.ClientId);
                var Supplier = new SqlParameter("@Supplier", basicSearch.SupplierId);
                var Employee = new SqlParameter("@Employee", basicSearch.EmployeeId);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var DivisionId= new SqlParameter("@DivisionId", basicSearch.DivisionId);
                var withopening = new SqlParameter("@withopening", basicSearch.WithOpening);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(basicSearch));
                var Action = new SqlParameter("@Action", Actions.select);
                var _product = await _dbContext.tbl_GeneralLedger.FromSqlRaw("stpro_PersonalLedger @fromdate, @todate,@Client,@Supplier,@Employee,@ProjectId, @CompanyId, @BranchId,@FinancialYearId,@DivisionId, @withopening, @json, @Action", fromdate, todate, Client, Supplier, Employee, ProjectId, CompanyId, BranchId, FinancialYearId, DivisionId, withopening, item, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GeneralLedger>> GroupSubGroupLedger(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ProjectId is null)
                    basicSearch.ProjectId = 0;

                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var AccountGroupId = new SqlParameter("@AccountGroupId", basicSearch.AccountGroupId);


                var AccountSubGroupId = new SqlParameter("@AccountSubGroupId", basicSearch.AccountSubGroupId);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", basicSearch.FinancialYearId);
                var withopening = new SqlParameter("@withopening", basicSearch.WithOpening);
                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var Action = new SqlParameter("@Action", Actions.select);
                var _product = await _dbContext.tbl_GeneralLedger.FromSqlRaw("stpro_GroupSubGroupLedger @fromdate, @todate,@AccountGroupId,@AccountSubGroupId, @CompanyId, @BranchId,@FinancialYearId, @withopening, @ProjectId, @Action", fromdate, todate, AccountGroupId, AccountSubGroupId, CompanyId, BranchId, FinancialYearId, withopening, ProjectId, Action).ToListAsync();
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