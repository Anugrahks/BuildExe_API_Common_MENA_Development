using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
namespace BuildExeServices.Repository
{
    public class ProjectPaymentModeRepository:IProjectPaymentModeRepository 
    {
        private readonly ProductContext _dbContext;
        public ProjectPaymentModeRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            try
            {
                var block = await _dbContext.tbl_ProjectPaymentMode.FindAsync(id);

                 _dbContext.tbl_ProjectPaymentMode.Remove(block);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectPaymentMode>> GetByID(string projectType)
        {
            try
            {
                return await _dbContext.tbl_ProjectPaymentMode.Where(x => x.ProjectTypeId == projectType).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectPaymentMode>> Get()
        {
            try
            {
                return await _dbContext.tbl_ProjectPaymentMode.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insert(ProjectPaymentMode projectPaymentMode  )
        {
            try
            {
                await _dbContext.AddAsync(projectPaymentMode);
                await _dbContext.SaveChangesAsync();
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

        public async Task Update(ProjectPaymentMode projectPaymentMode)
        {
            try
            {
                _dbContext.Entry(projectPaymentMode).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
