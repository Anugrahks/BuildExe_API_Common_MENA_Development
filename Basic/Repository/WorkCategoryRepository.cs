using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class WorkCategoryRepository :IWorkCategoryRepository 
    {
        private readonly BasicContext _dbContext;
        public WorkCategoryRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id,int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Formname = new SqlParameter("@Formname", "Work Category");
                var Action = new SqlParameter("@Action", "1");
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MasterDeleteValidation @Id,@json, @CompanyId, @BranchId,@UserId,@Formname, @Action", Id, json, CompanyId, BranchId, UserId, Formname, Action).ToListAsync();
                return purchaseList;
                //var floor = _dbContext.tbl_WorkCategoryMaster.Find(id);

                //if (floor != null)
                //{
                //    _dbContext.tbl_WorkCategoryMaster.Remove(floor);
                //    await _dbContext.SaveChangesAsync();


                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = id;
                //    userLogs.UserId = Convert.ToInt16(UserId);
                //    userLogs.FormName = "Work Category";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(3);

                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<WorkCategory>  GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_WorkCategoryMaster.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WorkCategory >> Get(int companyId, int branchid)
        {
            try
            {
                if (branchid == 0)

                return await _dbContext.tbl_WorkCategoryMaster.Where(p => p.CompanyId == companyId).OrderByDescending(i=>i.Id).ToListAsync();
            else
                return await _dbContext.tbl_WorkCategoryMaster.Where(p => p.CompanyId == companyId).Where(p => p.BranchId == branchid).OrderByDescending(i => i.Id).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(WorkCategory workCategory  )
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workCategory));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkCategory @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public async Task<IEnumerable<Validation>> Update(WorkCategory workCategory  )
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workCategory));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkCategory @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckWorkCategoryEditDelete @Id", Id).ToListAsync();
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
