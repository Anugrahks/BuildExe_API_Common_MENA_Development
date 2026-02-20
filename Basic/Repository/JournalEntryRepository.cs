using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;
using System.Reflection;
using System.Data.Common;

namespace BuildExeBasic.Repository
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly BasicContext _dbContext;
        public JournalEntryRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectForReport = 6
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Journal> journalEntries)
        {
            try
            {
                var id = new SqlParameter("@id", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(journalEntries));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<Journal> journalEntries)
        {
            try
            {
                var id = new SqlParameter("@id", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(journalEntries));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int ID, int UserID)
        {
            try
            {
                var id = new SqlParameter("@id", ID);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
       

        public async Task<IEnumerable<JournalList>> GetForEdit(int companyId, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_Journallist.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<JournalList>> GetForEdituser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@id", FinancialYearId);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_Journallist.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, Userid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<JournalList>> GetForApproval(int companyId, int branchid, int UserID,int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@id", FinancialYearId);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
                var _product = await _dbContext.tbl_Journallist.FromSqlRaw("Stpro_Journal @id, @item, @CompanyId, @BranchId, @UserId, @Action", id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getdetails(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Journal";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value =  7 });
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
                return purcasedetails == string.Empty ? "[]" : purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> Report(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Journal";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForReport });
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
                return purcasedetails == string.Empty ? "[]" : purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> Getvouchers(int CompanyId, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_JournalVoucher";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
                return purcasedetails == string.Empty ? "[]" : purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
