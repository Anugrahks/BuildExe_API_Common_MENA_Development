using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.Library;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class FloorRepository:IFloorRepository 
    {
        private readonly ProductContext _dbContext;
        public FloorRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }
        public async Task<IEnumerable<Validation>> Delete(int id,int userid)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", userid);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_Floors @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Floor> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Floors.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Floor>> Get(int companyid, int branchid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_Floors.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.FloorId).ToListAsync();
                else
                    return await _dbContext.tbl_Floors.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.FloorId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Floor>> Getuser(int companyid, int branchid, int UserId)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_Floors.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.FloorId).ToListAsync();
                else
                    return await _dbContext.tbl_Floors.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.FloorId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Floor>> Get()
        {
            try
            {
                return  await _dbContext.tbl_Floors.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(Floor floor)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(floor));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_Floors @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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

        public async Task<IEnumerable<Validation>> Update(Floor floor)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(floor));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_Floors @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int projectId, int blockId, int floorId)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@floorId", floorId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckAssignFloorEditDelete @Id, @ProjectId, @BlockId, @FloorId", Id, ProjectId, BlockId, FloorId).ToListAsync();
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
