using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ProductContext _dbContext;
        public UserGroupRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_UserGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<UserGroup> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_UserGroup.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<UserGroup>> Get(int companyid, int Branchid)
         {

            try
            {
                return await _dbContext.tbl_UserGroup.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == Branchid).OrderByDescending(x => x.UserGroupId).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<UserGroup>> Get()
        {
            try
            {
                return await _dbContext.tbl_UserGroup.Where(c=>c.CompanyId != 0).Where(c => c.BranchId != 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(UserGroup userGroup)
        {
            try
            {
                //await _dbContext.AddAsync(userGroup);
                //await _dbContext.SaveChangesAsync();

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(userGroup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_UserGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
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

        public async Task<IEnumerable<Validation>> Update(UserGroup userGroup)
        {
            try
            {
                //_dbContext.Entry(userGroup).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(userGroup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_UserGroup @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
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
    }
}
