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
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class GodownRepository:IGodownRepository 
    {
        private readonly BasicContext _dbContext;
        public GodownRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5
        }
        public async Task  Delete(int id,int UserId)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
            var GodownId = new SqlParameter("@GodownId", "0");
            var GodownName = new SqlParameter("@GodownName", "0");
            var GodownAddess = new SqlParameter("@GodownAddess", "0");
            var GodownDate = new SqlParameter("@GodownDate", "2020-01-01");
            var CompanyId = new SqlParameter("@CompanyId", UserId);
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Delete);
            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Godown @Id, @GodownId,@GodownName, @GodownAddess, @GodownDate,  @CompanyId, @BranchId, @Action", Id, GodownId, GodownName, GodownAddess, GodownDate, CompanyId, BranchId, Action);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Godown>> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var GodownId = new SqlParameter("@GodownId", "0");
                var GodownName = new SqlParameter("@GodownName", "0");
                var GodownAddess = new SqlParameter("@GodownAddess", "0");
                var GodownDate = new SqlParameter("@GodownDate", "2020-01-01");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var _product = await _dbContext.tbl_GodownMaster.FromSqlRaw("Stpro_Godown @Id, @GodownId,@GodownName, @GodownAddess, @GodownDate,  @CompanyId, @BranchId, @Action", Id, GodownId, GodownName, GodownAddess, GodownDate, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Godown>> Get(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var GodownId = new SqlParameter("@GodownId", "0");
                var GodownName = new SqlParameter("@GodownName", "0");
                var GodownAddess = new SqlParameter("@GodownAddess", "0");
                var GodownDate = new SqlParameter("@GodownDate", "2020-01-01");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_GodownMaster.FromSqlRaw("Stpro_Godown @Id, @GodownId,@GodownName, @GodownAddess, @GodownDate,  @CompanyId, @BranchId, @Action", Id, GodownId, GodownName, GodownAddess, GodownDate, CompanyId, BranchId, Action).ToListAsync();
                return _product;
                //  return _dbContext.tbl_DocumentGroup.ToList();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insert(Godown godown)
        {
            try
            {
                var Id = new SqlParameter("@Id", godown.Id);
                var GodownId = new SqlParameter("@GodownId", godown.GodownId);
                var GodownName = new SqlParameter("@GodownName", godown.GodownName);
                var GodownAddess = new SqlParameter("@GodownAddess", godown.GodownAddess);
                var GodownDate = new SqlParameter("@GodownDate", godown.GodownDate);
                var CompanyId = new SqlParameter("@CompanyId", godown.CompanyId);
                var BranchId = new SqlParameter("@BranchId", godown.BranchId);
                var Action = new SqlParameter("@Action", Actions.Insert);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Godown @Id, @GodownId,@GodownName, @GodownAddess, @GodownDate,  @CompanyId, @BranchId, @Action", Id, GodownId, GodownName, GodownAddess, GodownDate, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

       

        public async Task Update(Godown godown  )
        {
            try
            {
                var Id = new SqlParameter("@Id", godown.Id);
                var GodownId = new SqlParameter("@GodownId", godown.GodownId);
                var GodownName = new SqlParameter("@GodownName", godown.GodownName);
                var GodownAddess = new SqlParameter("@GodownAddess", godown.GodownAddess);
                var GodownDate = new SqlParameter("@GodownDate", godown.GodownDate);
                var CompanyId = new SqlParameter("@CompanyId", godown.CompanyId);
                var BranchId = new SqlParameter("@BranchId", godown.BranchId);
                var Action = new SqlParameter("@Action", Actions.Update);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Godown @Id, @GodownId,@GodownName, @GodownAddess, @GodownDate,  @CompanyId, @BranchId, @Action", Id, GodownId, GodownName, GodownAddess, GodownDate, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
