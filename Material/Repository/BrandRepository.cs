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
    public class BrandRepository:IBrandRepository 
    {
        private readonly MaterialContext _dbContext;
        public BrandRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id,int userid)
        {
            try
            {
                var item = await _dbContext.tbl_MaterialBrand.FindAsync(id);

               
                if (item != null)
                {
                    _dbContext.tbl_MaterialBrand.Remove(item);
                    await _dbContext.SaveChangesAsync();

                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = id;
                    userLogs.UserId = Convert.ToInt16(userid);
                    userLogs.FormName = "BRAND MASTER";
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

        public async Task<Brand>  GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_MaterialBrand.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Brand>> Get(int CompanyId, int Branchid)

        {
            try
            {
                if (Branchid == 0)

                    return await _dbContext.tbl_MaterialBrand.Where(p => p.CompanyId == CompanyId).OrderByDescending(x => x.Id).ToListAsync ();
                else
                    return await _dbContext.tbl_MaterialBrand.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Brand>> Get(int CompanyId, int Branchid, int UserId)

        {
            try
            {
                 return await _dbContext.tbl_MaterialBrand.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).Where(p=>p.UserId ==UserId).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public async Task<IEnumerable<Validation>> Insert(Brand  brand )
        {
            try
            {
                //await _dbContext.AddAsync(brand);
                //await _dbContext.SaveChangesAsync();
                //if (brand.Id > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = brand.Id;
                //    userLogs.UserId = Convert.ToInt16(brand.UserId);
                //    userLogs.FormName = "BRAND MASTER";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(1);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(brand));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var brands = await _dbContext.tbl_validation.FromSqlRaw("stPro_Brand @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return brands;


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

        public async Task<IEnumerable<Validation>> Update(Brand brand)
        {
            try
            {
                // _dbContext.Entry(brand).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();

                // if (brand.Id > 0)
                // {
                //     UserLogs userLogs = new UserLogs();
                //     userLogs.MasterId = brand.Id;
                //     userLogs.UserId = Convert.ToInt16(brand.UserId);
                //     userLogs.FormName = "BRAND MASTER";
                //     userLogs.EntryDate = DateTime.Now;
                //     userLogs.Action = Convert.ToInt32(2);
                //    await _dbContext.AddAsync(userLogs);
                //     await _dbContext.SaveChangesAsync();
                // }

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(brand));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var brands = await _dbContext.tbl_validation.FromSqlRaw("stPro_Brand @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return brands;
  
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckBrandEditDelete @Id", Id).ToListAsync();
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
