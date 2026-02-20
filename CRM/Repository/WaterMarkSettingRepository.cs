
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using System.ComponentModel.Design;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class WaterMarkSettingRepository : IWaterMarkSettingRepository
    {
        private readonly ProductContext _dbContext;

        public WaterMarkSettingRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            selectvalidation = 1,
            SelectionUpdate = 2,
            ActiveHeaders = 3,
            HeaderNameValidation = 4,
            Insert = 5,
            Update = 6,
            Delete = 7
        }
        public async Task<IEnumerable<Validation>> Delete(int id)
        {
            try
            {
                //await _dbContext.AddAsync(userGroup);
                //await _dbContext.SaveChangesAsync();

                var Id = new SqlParameter("@id", id);
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SettingWaterMark @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public IEnumerable<WaterMarkSetting> GetByCompanyAndBranch(int companyId, int branchId)
        {
            var reportHeaderSettings = _dbContext.tbl_WaterMarkSettings.Where(x => x.CompanyId == companyId || x.CompanyId == 0).Where(x => x.BranchId == branchId || x.BranchId == 0);
            return reportHeaderSettings.ToList();
        }

        public WaterMarkSetting GetByID(int id)
        {
            var reportHeaderSettings = _dbContext.tbl_WaterMarkSettings.Where(x => x.Id == id).FirstOrDefault();
            return reportHeaderSettings;
        }

        public IEnumerable<WaterMarkSetting> GetHeader()
        {
            var reportHeaderSettings = _dbContext.tbl_WaterMarkSettings;
            return reportHeaderSettings.ToList();
        }

        public async Task<IEnumerable<Validation>> Insert(WaterMarkSetting reportHeaderSettings)
        {
            try
            {
                //await _dbContext.AddAsync(userGroup);
                //await _dbContext.SaveChangesAsync();

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(reportHeaderSettings));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SettingWaterMark @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Update(WaterMarkSetting reportHeaderSettings)
        {
            try
            {
                //await _dbContext.AddAsync(userGroup);
                //await _dbContext.SaveChangesAsync();

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(reportHeaderSettings));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SettingWaterMark @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> HeaderUpdate(int companyid, int branchID, int id, string status)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchID);
                var Type = new SqlParameter("@Status", status);
                var HeaderName = new SqlParameter("@HeaderName", "");
                var Action = new SqlParameter("@Action", Actions.SelectionUpdate);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_WaterMarkSettings @Id, @CompanyId,@BranchId, @Status, @HeaderName, @Action", Id, CompanyId, BranchId, Type, HeaderName, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WaterMarkSetting>> HeaderStatusByType(int companyid, int branchID, int id, int type, string status)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchID);
                var Type = new SqlParameter("@Status", status);
                var HeaderName = new SqlParameter("@HeaderName", "");
                var Action = new SqlParameter("@Action", Actions.ActiveHeaders);
                var _product = await _dbContext.tbl_WaterMarkSettings.FromSqlRaw("Stpro_WaterMarkSettings @Id, @CompanyId,@BranchId, @Status, @HeaderName, @Action", Id, CompanyId, BranchId, Type, HeaderName, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> HeaderNameValidation(int companyid, int branchID, int id, string headerName)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchID);
                var Type = new SqlParameter("@Status", "");
                var HeaderName = new SqlParameter("@HeaderName", headerName);
                var Action = new SqlParameter("@Action", Actions.HeaderNameValidation);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_WaterMarkSettings @Id, @CompanyId,@BranchId, @Status, @HeaderName, @Action", Id, CompanyId, BranchId, Type, HeaderName, Action).ToListAsync();
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
