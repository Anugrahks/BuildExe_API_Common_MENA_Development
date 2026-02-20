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

using System.Data.SqlClient;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ProductContext _dbContext;
        public LevelRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Selectmaxlevel = 4,
            SelectAll = 5
        }
        //public void Delete(int id)
        //{
        //    var level = _dbContext.tbl_Level.Find(id);

        //    _dbContext.tbl_Level.Remove(level);
        //    Save();
        //}
        public async Task Delete(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Level @id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<Level> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Level.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Level>> Getforcompany(int Companyid, int branchid)
        {
            try
            {
                return await _dbContext.tbl_Level.Where(x => x.CompanyId == Companyid).Where(x => x.BranchId == branchid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Level>> Get()
        {
            try
            {
                return await _dbContext.tbl_Level.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public void Insert(Level level)
        //{
        //    _dbContext.Add(level);
        //    Save();
        //}
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Level> level)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");

                var team = new SqlParameter("@team", JsonConvert.SerializeObject(level));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Level @id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getmenuwithlevel(int companyid, int branchid)
        {
            try
            {

                var data = await (from a in _dbContext.tbl_Level
                                  join c in _dbContext.tbl_Menu on a.MenuId equals c.MenuId
                                  select new
                                  {
                                      id = a.Id,
                                      menuId = a.MenuId,
                                      menuName = c.MenuName,
                                      formlevel = a.Formlevel,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId

                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).Where(x => x.formlevel > 0).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<Level> level)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");

                var team = new SqlParameter("@team", JsonConvert.SerializeObject(level));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Level @id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public int Selectmaxlevel(int menuid, int companyid, int branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Level";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = menuid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Selectmaxlevel });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = cmd.ExecuteReader();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                int purcasedetails = 0;

                purcasedetails = Convert.ToInt32(dataTable.Rows[0][0].ToString());

                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return 0;
            }

        }

        //public void Update(Level level)
        //{
        //    _dbContext.Entry(level).State = EntityState.Modified;
        //    Save();
        //}
    }
}
