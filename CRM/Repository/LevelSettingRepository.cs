using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class LevelSettingRepository : ILevelSettingRepository
    {
        private readonly ProductContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            
        }

        public LevelSettingRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int companyid, int branchid, int UserId)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
            var EntryUserId = new SqlParameter("@EntryUserId", UserId);
            var team = new SqlParameter("@team", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LevelSetting @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LevelSetting>> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_LevelSetting.Where(x => x.MenuId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LevelSetting>> GetByID(int id, int companyid, int branchid)
        {
            try
            {
                return await _dbContext.tbl_LevelSetting.Where(x => x.MenuId == id).Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LevelSetting>> Get()
        {
            try
            {
                return await _dbContext.tbl_LevelSetting.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<LevelSetting> level)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(level));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LevelSetting @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> Update(IEnumerable<LevelSetting> level)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(level));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LevelSetting @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public string Getjson(int companyid, int branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "dbo.Stpro_LevelSetting";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@EntryUserId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
            
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Select });
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = cmd.ExecuteReader();

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
