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

namespace BuildExeBasic.Repository
{
    public class HeaderRepository:IHeaderRepository
    {
        private readonly BasicContext _dbContext;
        public HeaderRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
          
        }
        public async Task Insert(IEnumerable<Header> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Header @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(IEnumerable<Header> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Header @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Delete(int companyid,int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Header @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Header>> Get(int companyid, int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_Header.FromSqlRaw("Stpro_Header @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action).ToListAsync(); 
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //-------------------------------------------------------------------------------

        public async Task InsertContent(IEnumerable<Content> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Content @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task UpdateContent(IEnumerable<Content> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Content @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task DeleteContent(int companyid, int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Content @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Content>> GetContent(int companyid, int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_Content.FromSqlRaw("Stpro_Content @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task InsertFooter(IEnumerable<Footer> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Footer @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task UpdateFooter(IEnumerable<Footer> headers)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(headers));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Footer @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task DeleteFooter(int companyid, int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Footer @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Footer>> GetFooter(int companyid, int Branchid, int menuid)
        {
            try
            {
                var Menuid = new SqlParameter("@Menuid", menuid);

                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_Footer.FromSqlRaw("Stpro_Footer @Menuid, @item, @CompanyId, @BranchId, @UserId, @Action", Menuid, item, CompanyId, BranchId, UserId, Action).ToListAsync();
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
