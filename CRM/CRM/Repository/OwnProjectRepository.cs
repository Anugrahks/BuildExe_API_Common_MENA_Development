using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class OwnProjectRepository:IOwnProjectRepository 
    {
        private readonly ProductContext _dbContext;
        public OwnProjectRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            SelectForBooking=6,
            SelectoneUnit = 7

        }
        public async Task Delete(int id)
        {
            try
            {
                var own = await _dbContext.tbl_OwnProjectDetails.FindAsync(id);

                _dbContext.tbl_OwnProjectDetails.Remove(own);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

        public async Task<IEnumerable<OwnProject>> Get()
        {
            try
            {
                return  await _dbContext.tbl_OwnProjectDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insert(IEnumerable<OwnProject> ownProjects )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(ownProjects));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);
            await _dbContext.Database.ExecuteSqlRawAsync("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action);
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

        public async Task<IEnumerable<OwnProject>> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", 8);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<OwnProject>> GetUnitByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", 9);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<OwnProjectList>> GetForBooking(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectForBooking );
            var _product = await _dbContext.tbl_OwnProjectDetailsList.FromSqlRaw("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<OwnProjectList>> SelectoneUnit(int unitidid)
        {
            try
            {
                var Id = new SqlParameter("@Id", unitidid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectoneUnit);
                var _product = await _dbContext.tbl_OwnProjectDetailsList.FromSqlRaw("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(IEnumerable<OwnProject> ownProjects )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(ownProjects));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

           await _dbContext.Database.ExecuteSqlRawAsync("stpro_OwnProjectMaster @Id,@item,@CompanyId,@BranchId,@Action", Id, item, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #region Unit Filer by Project

        public async Task<IEnumerable<OwnProject>> GetUnitsForGeneralInvoice(int projectId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_GetUnitsForGeneralInvoice @ProjectId", ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<OwnProject>> GetUnitsForStageInvoice(int projectId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_GetUnitsForStageInvoice @ProjectId", ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<OwnProject>> GetUnitsForClientAdvance(int projectId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_GetUnitsForClientAdvance @ProjectId", ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<OwnProject>> GetUnitsForStageReceipt(int projectId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var _product = await _dbContext.tbl_OwnProjectDetails.FromSqlRaw("stpro_GetUnitsForStageReceipt @ProjectId", ProjectId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        #endregion
    }
}
