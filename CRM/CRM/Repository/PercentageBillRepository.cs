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
    public class PercentageBillRepository:IPercentageBillRepository 
    {
        private readonly ProductContext _dbContext;
        public PercentageBillRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,

            SelectReport = 4
        }
        public async Task Insert(IEnumerable<PercentageBill> percentageBills )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(percentageBills));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_PercentageBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(IEnumerable<PercentageBill> percentageBills)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(percentageBills));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_PercentageBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int Idworkorder, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", userid);
            var Action = new SqlParameter("@Action", Actions.Delete);

           await _dbContext.Database.ExecuteSqlRawAsync("Stpro_PercentageBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PercentageBill>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_PercentageBill.Where(x => x.Id == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PercentageBill>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_PercentageBill.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PercentageBill>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
            {
                var list = await _dbContext.tbl_PercentageBill.Where(x => x.CompanyId == companid).ToListAsync();
              
                return list;
            }
            else
            {
                var list = await _dbContext.tbl_PercentageBill.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).ToListAsync ();
               
                return list;
            }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<PercentageBillReport>> GetReport(BillSearch billSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
            var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId);
            var BranchId = new SqlParameter("@BranchId", billSearch.BranchId);
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectReport);

            var _product =await _dbContext.tbl_PercentageBillReport .FromSqlRaw("Stpro_PercentageBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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
