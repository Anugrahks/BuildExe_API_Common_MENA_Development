using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class UnitRepository:IUnitRepository 
    {
        private readonly MaterialContext _dbContext;
        public UnitRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public async Task Delete(int id,int userid)
        {
            try
            {
                var item = await _dbContext.tbl_Units.FindAsync(id);

                if (item != null)
                {
                    _dbContext.tbl_Units.Remove(item);
                    await _dbContext.SaveChangesAsync();

                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = id;
                    userLogs.UserId = Convert.ToInt16(userid);
                    userLogs.FormName = "UNIT MASTER";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(3);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }

              
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Unit> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Units.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //public IEnumerable<Unit> GetByID(int id,int companyid)
        //{
        //    return _dbContext.tbl_Units.Where(p => p.CompanyId == companyid).Where(p => p.UnitId == id);
        //}

        public async Task<IEnumerable<Unit>> Get(int CompanyId, int Branchid)
        {
            try
            {
                if (Branchid == 0)
                return await _dbContext.tbl_Units.Where(p => p.CompanyId == CompanyId).OrderByDescending(x => x.UnitId).ToListAsync();
            else
                return await _dbContext.tbl_Units.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).OrderByDescending(x => x.UnitId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Unit>> Get(int CompanyId, int Branchid, int UserId)
        {
            try
            {
               return await _dbContext.tbl_Units.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).Where(p=>p.UserId == UserId).OrderByDescending(x => x.UnitId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Unit>> StaticGet(int CompanyId, int Branchid, int UserId)
        {
            try
            {
                return await _dbContext.tbl_Units.Where(p => p.Isdefault == 1).Where(p => p.BranchId == Branchid).Where(p => p.CoefficientStatic == 1).OrderByDescending(x => x.UnitId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> Insert(Unit unit)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(unit));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var units = await _dbContext.tbl_validation.FromSqlRaw("stPro_Units @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return units;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Inserts(Unit unit)
        {
            try
            {
                await _dbContext.AddAsync(unit);
                await _dbContext.SaveChangesAsync();

                if (unit.UnitId > 0)
                {
                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = unit.UnitId;
                    userLogs.UserId = Convert.ToInt16(unit.UserId);
                    userLogs.FormName = "BRAND MASTER";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(1);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }
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

        public async Task<IEnumerable<Validation>> Update(Unit unit)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(unit));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var units = await _dbContext.tbl_validation.FromSqlRaw("stPro_Units @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return units;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Updates(Unit unit)
        {
            try
            {
                _dbContext.Entry(unit).State = EntityState.Modified;
               await _dbContext.SaveChangesAsync();

                if (unit.UnitId > 0)
                {
                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = unit.UnitId;
                    userLogs.UserId = Convert.ToInt16(unit.UserId);
                    userLogs.FormName = "BRAND MASTER";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(2);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckUnitEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

    }
}
