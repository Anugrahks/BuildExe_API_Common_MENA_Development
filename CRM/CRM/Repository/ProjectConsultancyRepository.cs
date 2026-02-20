using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ProjectConsultancyRepository:IProjectConsultancyRepository 
    {
        private readonly ProductContext _dbContext;
        public ProjectConsultancyRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }
        public async Task Delete(int id,int userId)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
            var EntryUserId = new SqlParameter("@EntryUserId", userId);
            var team = new SqlParameter("@team","");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Delete);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ProjectConsutancy @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectConsultancy>> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_ProjectConsultancyDetails.Where(x => x.ProjectId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetConsultancy(int projectid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_ProjectConsultancyDetails
                                  join b in _dbContext.tbl_ConsultancyWorkMaster on a.Workid equals b.Id
                                  join d in _dbContext.tbl_Units on b.Unit equals d.UnitId into ds from d in ds.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.id,
                                      projectId = a.ProjectId ,
                                     
                                      workid = a.Workid,
                                      workName = b.WorkName,
                                      unit = b.Unit,
                                     
                                      unitLongName = d == null ? String.Empty : d.UnitLongName,
                                      unitRate = a.UnitRate,
                                      qty = a.Qty,
                                      description = a.Description,
                                      remarks = a.Remarks,
                                      startdate = a.startdate,
                                      enddate = a.enddate,
                                      userId=a.UserId

                                  }).Where(x => x.projectId == projectid).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
                
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectConsultancy>> Get()
        {
            try
            {
                return await _dbContext.tbl_ProjectConsultancyDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insert(IEnumerable<ProjectConsultancy> projectConsultancy)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectConsultancy));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

            await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ProjectConsutancy @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task Update(IEnumerable<ProjectConsultancy> projectConsultancy)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectConsultancy));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

           await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ProjectConsutancy @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action);
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckConsultancyEditDelete @Id", Id).ToListAsync();
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
