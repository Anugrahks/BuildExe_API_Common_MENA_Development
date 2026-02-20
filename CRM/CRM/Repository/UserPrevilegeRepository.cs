using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;

using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class UserPrevilegeRepository:IUserPrevilegeRepository 
    {
        private readonly ProductContext _dbContext;

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public UserPrevilegeRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserPrevilege>> Get(int usergroup, int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", usergroup);
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.SelectAll);

                var _product = await _dbContext.tbl_UserPrivilege.FromSqlRaw("Stpro_UserPrevilege @Id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Delete(int usergroup,int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", usergroup);
            var team = new SqlParameter("@team", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var Action = new SqlParameter("@Action", Actions.Delete);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_UserPrevilege @Id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Insert(IEnumerable<UserPrevilege > userPrevileges )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var team = new SqlParameter("@team", JsonConvert.SerializeObject(userPrevileges));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_UserPrevilege @Id,@team, @CompanyId, @BranchId, @Action", Id, team, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
