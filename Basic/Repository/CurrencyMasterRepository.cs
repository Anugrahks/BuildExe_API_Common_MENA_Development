using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using iText.Svg.Renderers.Impl;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class CurrencyMasterRepository: ICurrencyMasterRepository
    {
        private readonly BasicContext _dbContext;

        public CurrencyMasterRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            SelectParent = 5,
        }

        #region Data Manipulation

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<CurrencyMaster> currency)
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(currency));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var result = await _dbContext.tbl_validation.FromSqlRaw("stpro_CurrencyMaster @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<CurrencyMaster> currency)
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(currency));
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var result = await _dbContext.tbl_validation.FromSqlRaw("stpro_CurrencyMaster @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int userId)
        {
            try
            {

                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", "{}");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var result = await _dbContext.tbl_validation.FromSqlRaw("stpro_CurrencyMaster @Id, @CompanyId, @BranchId, @json, @FinancialYearId, @UserId, @Action", Id, CompanyId, BranchId, item, FinancialYearId, UserId, Action).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion

        #region Grids & Reports

        public async Task<string> Get(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_CurrencyMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectAll });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string resultList = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    resultList += dataTable.Rows[i][0].ToString();
                }

                if (resultList == "")
                    resultList = "[]";
                return resultList;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetParent(int CompanyId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_CurrencyMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectParent });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string resultList = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    resultList += dataTable.Rows[i][0].ToString();
                }

                if (resultList == "")
                    resultList = "[]";
                return resultList;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion

    }
}
