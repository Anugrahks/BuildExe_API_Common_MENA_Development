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
    public class MaterialCategoryRepository:IMaterialCategoryRepository 
    {
        private readonly MaterialContext _dbContext;
        public MaterialCategoryRepository(MaterialContext dbContext)
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
        public async Task Delete(int id,int  userid)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
        "DELETE FROM tbl_MaterialCategory WHERE Id = {0}", id);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<MaterialCategory> GetByID(int id)
        {
            try
            {
                return await  _dbContext.tbl_MaterialCategory.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialCategory>> Get(int CompanyId,int Branchid)
        {
            try
            {
                return await _dbContext.tbl_MaterialCategory.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<MaterialCategory>> Get(int CompanyId, int Branchid, int UserId)
        {
            try
            {
                    return await _dbContext.tbl_MaterialCategory.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).Where(p=>p.UserId == UserId).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(MaterialCategory materialCategory)
        {
            try
            {
                //await _dbContext.AddAsync(materialCategory);
                //await _dbContext.SaveChangesAsync();
                //if (materialCategory.Id > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = materialCategory.Id;
                //    userLogs.UserId = Convert.ToInt16(materialCategory.UserId);
                //    userLogs.FormName = "MATERIAL CATEGORY MASTER";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(1);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(materialCategory));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var units = await _dbContext.tbl_validation.FromSqlRaw("stPro_Category @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return units;
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

        public async Task<IEnumerable<Validation>> Update(MaterialCategory materialCategory)
        {
            try
            {
                // _dbContext.Entry(materialCategory).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
                // if (materialCategory.Id > 0)
                // {
                //     UserLogs userLogs = new UserLogs();
                //     userLogs.MasterId = materialCategory.Id;
                //     userLogs.UserId = Convert.ToInt16(materialCategory.UserId);
                //     userLogs.FormName = "MATERIAL CATEGORY MASTER";
                //     userLogs.EntryDate = DateTime.Now;
                //     userLogs.Action = Convert.ToInt32(2);
                //     await _dbContext.AddAsync(userLogs);
                //     await _dbContext.SaveChangesAsync();
                // }

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(materialCategory));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var units = await _dbContext.tbl_validation.FromSqlRaw("stPro_Category @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return units;
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckMaterialCategoryEditDelete @Id", Id).ToListAsync();
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
