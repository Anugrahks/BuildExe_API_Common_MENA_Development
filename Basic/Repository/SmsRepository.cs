using BuildExeBasic.DBContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System;
using BuildExeBasic.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuildExeBasic.Repository
{
    public class SmsRepository : ISmsRepository
    {
        private readonly BasicContext _dbContext;
        public SmsRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            GetSms = 4,
            Status = 3
        }

        public async Task<string> GetSms(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SmsForm";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });

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

        public async Task<IEnumerable<Validation>> Post(Smsmodel smsmodel)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(smsmodel));
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SmsForm @Id, @MenuId, @CompanyId, @BranchId,@json, @Action", Id, MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Put(Smsmodel smsmodel)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(smsmodel));
                var Action = new SqlParameter("@Action", 2);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SmsForm @Id, @MenuId, @CompanyId, @BranchId,@json, @Action", Id, MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> PutStatus(Smsmodel smsmodel)
        {
            try
            {
                var Id = new SqlParameter("@Id", smsmodel.Id);
                var MenuId = new SqlParameter("@MenuId", smsmodel.MenuId);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", smsmodel.IsActive);
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(smsmodel));
                var Action = new SqlParameter("@Action", Actions.Status);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SmsForm @Id, @MenuId, @CompanyId, @BranchId,@json, @Action", Id, MenuId, CompanyId, BranchId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFetch(int Id, int MenuId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WhatsappSmsFetch";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = MenuId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });

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

    }

}