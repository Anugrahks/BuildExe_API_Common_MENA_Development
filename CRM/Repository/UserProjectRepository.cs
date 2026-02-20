using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BuildExeServices.Repository;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly ProductContext _dbContext;
        public UserProjectRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
           
        }
        public async Task Insert(IEnumerable<UserProject> userProjects)
        {
            try
            {
                var userId = new SqlParameter("@id", "0");
                var projects = new SqlParameter("@projects", JsonConvert.SerializeObject(userProjects));
                var Action = new SqlParameter("@Action", Actions.Insert);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_UserProject @id,@projects,  @Action", userId, projects, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Delete(int userid)
        {
            try
            {
                var userId = new SqlParameter("@Userid", userid);
            var projects = new SqlParameter("@projects", "");

            var Action = new SqlParameter("@Action", Actions.Delete);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_UserProject @Userid,@projects,  @Action", userId, projects, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(IEnumerable<UserProject> userProjects)
        {
            try
            {
                var userId = new SqlParameter("@id", "0");
                var projects = new SqlParameter("@projects", JsonConvert.SerializeObject(userProjects));

                var Action = new SqlParameter("@Action", Actions.Update);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_UserProject  @id,@projects,  @Action", userId, projects, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<UserProject>> Get()
        {
            try
            {

                var userId = new SqlParameter("@id", "0");
            var projects = new SqlParameter("@projects", "");

            var Action = new SqlParameter("@Action", Actions.SelectAll);
            var _product = await _dbContext.tbl_UserProject.FromSqlRaw("Stpro_UserProject  @id,@projects,  @Action", userId, projects, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<UserProject>> GetByID(int Userid)
        {
            try
            {
                var userId = new SqlParameter("@id", Userid);
                var projects = new SqlParameter("@projects", "");

                var Action = new SqlParameter("@Action", Actions.Select);
                var _product =await _dbContext.tbl_UserProject.FromSqlRaw("Stpro_UserProject  @id,@projects, @Action", userId, projects, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
