using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class MenuRepository:IMenuRepository 
    {
        private readonly ProductContext _dbContext;

        public MenuRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Update(Menu  menu)
        {
            try
            {
                var MenuId = new SqlParameter("@MenuId", menu.MenuId);
                var Active = new SqlParameter("@Active", menu.IsActive);
                var Action = new SqlParameter("@Action", 1);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Module @MenuId,@Active,@Action ", MenuId, Active, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
       // --------------------------------------


        public async Task<IEnumerable<Menu>> GetMenunbymenuid(int menuid)
        {
            try
            {
                return await  _dbContext.tbl_Menu.Where(x => x.MenuId  == menuid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Menu>> GetMenuforPrint()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var Action = new SqlParameter("@Action", 6);

                var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Menu>> GetMenuforReport()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
            var MenuId = new SqlParameter("@MenuId", "0");
            var Action = new SqlParameter("@Action", 5);

            var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Menu>> GetMenuforReprint()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var Action = new SqlParameter("@Action", 7);

                var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Menu>> GetMenuforautomail()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var Action = new SqlParameter("@Action", 8);

                var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Menu>> getall()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
            var MenuId = new SqlParameter("@MenuId", "0");
            var Action = new SqlParameter("@Action", 4);

            var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Menu>> GetMenuforApprovallevel()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
            var MenuId = new SqlParameter("@MenuId", "0");
            var Action = new SqlParameter("@Action", 3);

            var _product = await  _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

       
        public async Task<IEnumerable<Menu>> GetModule()
        {
            try
            {
                var Userid = new SqlParameter("@Userid", "0");
            var MenuId = new SqlParameter("@MenuId", "0");
            var Action = new SqlParameter("@Action", 2);

            var _product =await  _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Menu>> GetMenu(int UsertId)
        {
            try
            {
                var Userid = new SqlParameter("@Userid", UsertId);
            var MenuId = new SqlParameter("@MenuId", "0");
            var Action = new SqlParameter("@Action", 1);

            var _product = await _dbContext.tbl_Menu.FromSqlRaw("Stpro_Menu @UserId,@MenuId,@Action ", Userid, MenuId, Action).ToListAsync ();

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
