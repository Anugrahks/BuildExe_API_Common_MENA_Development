using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;

using BuildExeServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ProjectBlockFloorAssignRepository : IProjectBlockFloorAssignRepository
    {
        private readonly ProductContext _dbContext;
        public ProjectBlockFloorAssignRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int Blockid, int UserID)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                var EntryUserId = new SqlParameter("@EntryUserId", UserID);
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", Blockid);
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBlockFloorAssign>> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Project_BlockFloorAssign.Where(x => x.ProjectId == id).ToListAsync();
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
        public async Task<IEnumerable<ProjectBlockFloorAssign>> Get()
        {
            try
            {
                return await _dbContext.tbl_Project_BlockFloorAssign.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectBlockFloorAssign> projectBlockFloorAssign)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectBlockFloorAssign));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
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

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ProjectBlockFloorAssign> projectBlockFloorAssign)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectBlockFloorAssign));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectBlockFloorAssignList>> get(int Companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var Action = new SqlParameter("@Action", Actions.Select);
                var purchaseList = await _dbContext.tbl_Project_BlockFloorAssignList.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBlockFloorAssignList>> getuser(int Companyid, int Branchid, int UserId)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.Select);
                var purchaseList = await _dbContext.tbl_Project_BlockFloorAssignList.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action,@UserId", Id, EntryUserId, team, CompanyId, BranchId, Action, userId).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectBlockFloorAssign>> getFloor(int projectid, int blockid)
        {
            try
            {
                var Id = new SqlParameter("@id", projectid);
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", blockid);
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var purchaseList = await _dbContext.tbl_Project_BlockFloorAssign.FromSqlRaw("Stpro_ProjectBlockFloorAssign @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
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
                var data = await (from a in _dbContext.tbl_Project_BlockFloorAssign
                                  join b in _dbContext.tbl_Block on a.BlockId equals b.BlockId
                                  join c in _dbContext.tbl_Floors on a.FloorId equals c.FloorId
                                  join d in _dbContext.tbl_ProjectMaster on a.ProjectId equals d.id
                                  select new
                                  {
                                      id = a.id,
                                      isActive = a.IsActive,
                                      projectId = a.ProjectId,
                                      projectName = d.ProjectName,
                                      blockId = a.BlockId,
                                      blockName = b.BlockName,
                                      floorId = a.FloorId,
                                      floorName = c.FloorName,
                                      unitId = a.UnitId,
                                      userId = a.UserId,
                                      companyId = d.CompanyId,
                                      branchId = d.BranchId
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

        public async Task<string> Getbycompanyuser(int Companyid, int Branchid, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_Project_BlockFloorAssign
                                  join b in _dbContext.tbl_Block on a.BlockId equals b.BlockId
                                  join c in _dbContext.tbl_Floors on a.FloorId equals c.FloorId
                                  join d in _dbContext.tbl_ProjectMaster on a.ProjectId equals d.id
                                  select new
                                  {
                                      id = a.id,
                                      isActive = a.IsActive,
                                      projectId = a.ProjectId,
                                      projectName = d.ProjectName,
                                      blockId = a.BlockId,
                                      blockName = b.BlockName,
                                      floorId = a.FloorId,
                                      floorName = c.FloorName,
                                      unitId = a.UnitId,
                                      userId = a.UserId,
                                      companyId = d.CompanyId,
                                      branchId = d.BranchId
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
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int type)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var Type = new SqlParameter("@Type", type);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckAssignRelatedEditDelete @Id, @Type", Id, Type).ToListAsync();
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
