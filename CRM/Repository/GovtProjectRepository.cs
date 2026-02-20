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
    public class GovtProjectRepository : IGovtProjectRepository
    {
        private readonly ProductContext _dbContext;

        public GovtProjectRepository(ProductContext dbContext)
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
        public async Task Insert(GovtProject govtProject)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", govtProject.ProjectId);
                var DateEntered = new SqlParameter("@DateEntered", govtProject.DateEntered);
                var TenderDate = new SqlParameter("@TenderDate", govtProject.TenderDate);
                var TenderType = new SqlParameter("@TenderType", govtProject.TenderType);
                var TenderNumber = new SqlParameter("@TenderNumber", govtProject.TenderNumber);
                var Action = new SqlParameter("@Action", Actions.Insert);
                await _dbContext.Database.ExecuteSqlRawAsync("stpro_GovtProject @id,@ProjectId, @DateEntered, @TenderDate, @TenderType, @TenderNumber,@Action ", id, ProjectId, DateEntered, TenderDate, TenderType, TenderNumber, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(GovtProject govtProject)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", govtProject.ProjectId);
                var DateEntered = new SqlParameter("@DateEntered", govtProject.DateEntered);
                var TenderDate = new SqlParameter("@TenderDate", govtProject.TenderDate);
                var TenderType = new SqlParameter("@TenderType", govtProject.TenderType);
                var TenderNumber = new SqlParameter("@TenderNumber", govtProject.TenderNumber);
                var Action = new SqlParameter("@Action", Actions.Update);
               await _dbContext.Database.ExecuteSqlRawAsync("stpro_GovtProject @id,@ProjectId, @DateEntered, @TenderDate, @TenderType, @TenderNumber,@Action ", id, ProjectId, DateEntered, TenderDate, TenderType, TenderNumber, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int projectId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var DateEntered = new SqlParameter("@DateEntered", "2020-01-01");
                var TenderDate = new SqlParameter("@TenderDate", "2020-01-01");
                var TenderType = new SqlParameter("@TenderType", "0");
                var TenderNumber = new SqlParameter("@TenderNumber", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
               await _dbContext.Database.ExecuteSqlRawAsync("stpro_GovtProject @id,@ProjectId, @DateEntered, @TenderDate, @TenderType, @TenderNumber,@Action ", id, ProjectId, DateEntered, TenderDate, TenderType, TenderNumber, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GovtProject>> Get()
        {
            try
            {
                return await _dbContext.tbl_TenderMaster.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GovtProject>> GetByID(int projectId)
        {
            try
            {
                return await _dbContext.tbl_TenderMaster.Where(p => p.ProjectId == projectId).ToListAsync();
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckGovtProjectEditDelete @Id", Id).ToListAsync();
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
