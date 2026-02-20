using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class WorkNameRepository : IWorkNameRepository
    {
        private readonly ProductContext _dbContext;
        public WorkNameRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int userid)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", userid);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkName @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<WorkName> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_WorkName.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<WorkName>> Get(int Companyid, int Branchid)
        {
            try
            {
                if (Branchid == 0)
                    return await _dbContext.tbl_WorkName.Where(p => p.CompanyId == Companyid).ToListAsync();
                else
                    return await _dbContext.tbl_WorkName.Where(p => p.CompanyId == Companyid).Where(p => p.BranchId == Branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getbycompany(int Companyid, int Branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_WorkName

                                  select new
                                  {
                                      id = a.Id,                                     
                                      workShortName = a.WorkShortName,
                                      description = a.Description,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      userId = a.UserId,
                                      specId = a.SpecId

                                  }).Where(x => x.companyId == Companyid).Where(x => x.branchId == Branchid).OrderByDescending(x => x.id).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Insert(WorkName workName)
        {
            try
            {
                //await _dbContext.AddAsync(workName);
                //await _dbContext.SaveChangesAsync();
                //if (workName.Id > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = workName.Id;
                //    userLogs.UserId = Convert.ToInt16(workName.UserId);
                //    userLogs.FormName = "WORK Name";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(1);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workName));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkName @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
        public async Task<IEnumerable<Validation>> Update(WorkName workName)
        {
            try
            {
                //_dbContext.Entry(workName).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
                //if (workName.Id > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = workName.Id;
                //    userLogs.UserId = Convert.ToInt16(workName.UserId);
                //    userLogs.FormName = "WORK Name";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(2);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workName));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkName @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WorkName >> Getforforms(int projectId,int unitid,int blockid,int floorId,  int categoryId)
        {
            try
            {
                var json = new SqlParameter("@json", "");
                var Projectid = new SqlParameter("@Projectid", projectId);
                var UnitId = new SqlParameter("@UnitId", unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId ", floorId);
                var CategoryId = new SqlParameter("@CategoryId", categoryId);
                var Action = new SqlParameter("@Action", 6);

                var _product = await _dbContext.tbl_WorkName.FromSqlRaw("Stpro_WorkName @json,@Projectid,@UnitId,@BlockId,@FloorId,@CategoryId,@Action", json, Projectid, UnitId, BlockId, FloorId, CategoryId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int worknameId)
        {
            try
            {
                var Id = new SqlParameter("@Id", worknameId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckWorkNameEditDelete @Id", Id).ToListAsync();
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
