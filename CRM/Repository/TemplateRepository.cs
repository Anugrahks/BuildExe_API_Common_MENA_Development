using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ProductContext _dbContext;
        public TemplateRepository(ProductContext dbContext)
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
            SelectDetailItemList = 6
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Template> templates)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(templates));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_TemplateMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<Template> templates)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(templates));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_TemplateMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int Idworkorder, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_TemplateMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Template>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_TemplateMaster.Where(x => x.Id == Idworkorder).ToListAsync();
            var detaillist = await _dbContext.tbl_TemplateDetails.Where(x => x.TemplateId == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Template>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_TemplateMaster.Where(x => x.IsDeleted == 0).ToListAsync();
            foreach (var detail in list)
            {
                var detaillist = await _dbContext.tbl_TemplateDetails.Where(x => x.TemplateId == detail.Id).ToListAsync();
            }
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Template>> GetbyWorkname(int Id)
        {
            try
            {
                var list = await _dbContext.tbl_TemplateMaster.Where(x => x.IsDeleted == 0).Where(x => x.WorkNameId == Id).ToListAsync();
               
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Template>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
            {
                var list = await _dbContext.tbl_TemplateMaster.Where(x => x.CompanyId == companid).Where(x => x.IsDeleted == 0).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_TemplateDetails.Where(x => x.TemplateId == detail.Id).ToListAsync();
                }
                return list;
            }
            else
            {
                var list = await _dbContext.tbl_TemplateMaster.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).Where(x => x.IsDeleted == 0).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_TemplateDetails.Where(x => x.TemplateId == detail.Id).ToListAsync();
                }
                return list;
            }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> getTemplateforEdit(int companyid, int branchid)

        {
            try
            {
                var data = await (from a in _dbContext.tbl_TemplateMaster
                                  join b in _dbContext.tbl_WorkName on a.WorkNameId equals b.Id   into bs from b in bs.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      templateName = a.TemplateName,
                                      templateNo = a.TemplateNo,
                                      workNameId = a.WorkNameId,
                                      //workShortName = b.WorkShortName,
                                      workShortName = b == null ? String.Empty : b.WorkShortName,
                                      // workTypeName = c.WorkTypeName,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      isDeleted = a.IsDeleted


                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).Where(x => x.isDeleted == 0).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> getTemplateforEdituser(int companyid, int branchid, int UserId, int FinancialYearId)

        {
            try
            {
                var data = await (from a in _dbContext.tbl_TemplateMaster
                                  join b in _dbContext.tbl_WorkName on a.WorkNameId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      templateName = a.TemplateName,
                                      templateNo = a.TemplateNo,
                                      workNameId = a.WorkNameId,
                                      //workShortName = b.WorkShortName,
                                      workShortName = b == null ? String.Empty : b.WorkShortName,
                                      // workTypeName = c.WorkTypeName,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      isDeleted = a.IsDeleted


                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).Where(x => x.isDeleted == 0).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> getTemplate(int workid, int WorkNameId)

        {
            try
            {
                var data = await (from a in _dbContext.tbl_TemplateMaster
                                  join b in _dbContext.tbl_WorkName on a.WorkNameId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      templateName = a.TemplateName,
                                      templateNo = a.TemplateNo,
                                      workNameId = a.WorkNameId,
                                    //  workShortName = b.WorkShortName,
                                      workShortName = b == null ? String.Empty : b.WorkShortName,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      isDeleted = a.IsDeleted


                                  }).Where(x => x.workNameId == WorkNameId).Where(x => x.isDeleted == 0).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<TemplateDetailList>> GetTemplateDetails(IEnumerable<Template> templates)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(templates));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectDetailItemList);

            var _product = await _dbContext.tbl_TemplateDetailsList.FromSqlRaw("Stpro_TemplateMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckTemplateEditDelete @Id", Id).ToListAsync();
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
