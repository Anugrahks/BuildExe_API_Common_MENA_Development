using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;

using Microsoft.Data.SqlClient;

using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class TendorSubmittedRepository:ITendorSubmittedRepository 
    {
        private readonly ProductContext _dbContext;

        public TendorSubmittedRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
          
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<Validation>> Insert(TendorSubmitted tendorSubmitted)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", tendorSubmitted.ProjectId );
                
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(tendorSubmitted));
                var Status = new SqlParameter("@Status", tendorSubmitted.Status);
                if (tendorSubmitted.StatusDescription == null)
                    tendorSubmitted.StatusDescription = "";
                var StatusDescription = new SqlParameter("@StatusDescription", tendorSubmitted.StatusDescription);

                var CompanyId = new SqlParameter("@CompanyId", tendorSubmitted.CompanyId);
                var BranchId = new SqlParameter("@BranchId", tendorSubmitted.BranchId);
                var userId = new SqlParameter("@userId", tendorSubmitted.UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorSubmit @Id,@projectId,@item,@Status,@StatusDescription,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, item, Status, StatusDescription, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            // try
            //  {
            //     await _dbContext.AddAsync(tendorSubmitted);
            //    await _dbContext.SaveChangesAsync();
            //  }
            //  catch (Exception)
            // { throw; }
        }
        public async Task<IEnumerable<Validation>> Update(TendorSubmitted tendorSubmitted  )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", tendorSubmitted.ProjectId);

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(tendorSubmitted));
                var Status = new SqlParameter("@Status", tendorSubmitted.Status);
                if (tendorSubmitted.StatusDescription == null)
                    tendorSubmitted.StatusDescription = "";
                var StatusDescription = new SqlParameter("@StatusDescription", tendorSubmitted.StatusDescription);

                var CompanyId = new SqlParameter("@CompanyId", tendorSubmitted.CompanyId);
                var BranchId = new SqlParameter("@BranchId", tendorSubmitted.BranchId);
                var userId = new SqlParameter("@userId", tendorSubmitted.UserId);
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stpro_TendorSubmit @Id,@projectId,@item,@Status,@StatusDescription,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, item, Status, StatusDescription, CompanyId, BranchId, userId, Action).ToListAsync();

                //_dbContext.Entry(tendorSubmitted).State = EntityState.Modified;
                // await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int departmentId)
        {
            try
            {
                var department =await _dbContext.tbl_TendorSubmitted .FindAsync(departmentId);

                _dbContext.tbl_TendorSubmitted.Remove(department);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<TendorSubmitted >> Get()
        {
            try
            {
                return await _dbContext.tbl_TendorSubmitted .ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<TendorSubmitted>> GetByID(int projectid)
        {
            try
            {
                return await _dbContext.tbl_TendorSubmitted.Where(x => x.ProjectId == projectid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
