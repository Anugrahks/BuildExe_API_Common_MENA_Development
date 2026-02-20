using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class EnquiryModeRepository : IEnquiryModeRepository 
    {
        private readonly ProductContext _dbContext;
        public EnquiryModeRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }
        public async Task<IEnumerable<Validation>> Delete_enquirymode(int enquirymodeId)
        {
            try
            {
                var Id = new SqlParameter("@id", enquirymodeId);
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_EnquiryMode @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public EnquiryMode  GetenquirymodeByID(int enquirymodeId)
        {
            return _dbContext.tbl_EnquiryMode.Find(enquirymodeId);
        }

        public IEnumerable<EnquiryMode> Getenquirymode()
        {
            return _dbContext.tbl_EnquiryMode.ToList();
        }
        public IEnumerable<EnquiryMode> Getenquirymode(int CompanyId, int BranchId)
        {
            return _dbContext.tbl_EnquiryMode.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).OrderByDescending(x => x.Id).ToList();
        }

        public IEnumerable<EnquiryMode> Getenquirymodeuser(int CompanyId, int BranchId, int UserId)
        {
            return _dbContext.tbl_EnquiryMode.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).OrderByDescending(x => x.Id).ToList();
        }

        public async Task<IEnumerable<Validation>> Insertenquirymode(EnquiryMode enquiryMode)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(enquiryMode));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var enMode = await _dbContext.tbl_validation.FromSqlRaw("stPro_EnquiryMode @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return enMode;
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

        public async Task<IEnumerable<Validation>> Update_enquirymode(EnquiryMode enquiryMode)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(enquiryMode));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_EnquiryMode @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
