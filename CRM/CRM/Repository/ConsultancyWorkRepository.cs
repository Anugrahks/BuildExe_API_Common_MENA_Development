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
    public class ConsultancyWorkRepository:IConsultancyWorkRepository 
    {
        private readonly ProductContext _dbContext;
        public ConsultancyWorkRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
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

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_ConsultancyWork @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<ConsultancyWork>  GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_ConsultancyWorkMaster .FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ConsultancyWork>> Get()
        {
            try
            {
                return await _dbContext.tbl_ConsultancyWorkMaster.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task< IEnumerable<ConsultancyWork>> Get(int companyid, int branchid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_ConsultancyWorkMaster.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.Id ).ToListAsync();
                else
                    return await _dbContext.tbl_ConsultancyWorkMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getdetails(int companyid, int branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_ConsultancyWorkMaster
                                  join c in _dbContext.tbl_Units on a.Unit equals c.UnitId into cs from c in cs.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      workName = a.WorkName,
                                      unit = a.Unit,
                                      // unitLongName = c.UnitLongName,
                                      unitLongName = c == null ? String.Empty : c.UnitLongName,
                                      unitRate = a.UnitRate,
                                      remarks = a.Remarks,
                                      sac_Code = a.Sac_Code,
                                     // sac_Code = (a == null ? String.Empty : a.Sac_Code),
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId


                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getdetailsuser(int companyid, int branchid, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_ConsultancyWorkMaster
                                  join c in _dbContext.tbl_Units on a.Unit equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      workName = a.WorkName,
                                      unit = a.Unit,
                                      // unitLongName = c.UnitLongName,
                                      unitLongName = c == null ? String.Empty : c.UnitLongName,
                                      unitRate = a.UnitRate,
                                      remarks = a.Remarks,
                                      sac_Code = a.Sac_Code,
                                      // sac_Code = (a == null ? String.Empty : a.Sac_Code),
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId


                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Insert(ConsultancyWork consultancyWork)
        {
            try
            {
                //await  _dbContext.AddAsync (consultancyWork);
                // await _dbContext.SaveChangesAsync();
                // if (consultancyWork.Id > 0)
                // {
                //     UserLogs userLogs = new UserLogs();
                //     userLogs.MasterId = consultancyWork.Id;
                //     userLogs.UserId = Convert.ToInt16(consultancyWork.UserId);
                //     userLogs.FormName = "CONSULTANCY WORK";
                //     userLogs.EntryDate = DateTime.Now;
                //     userLogs.Action = Convert.ToInt32(1);
                //     await _dbContext.AddAsync(userLogs);
                //     await  _dbContext.SaveChangesAsync();
                // }
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(consultancyWork));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_ConsultancyWork @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
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

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Update(ConsultancyWork consultancyWork)
        {
            try
            {
                //_dbContext.Entry(consultancyWork).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync ();
                //if (consultancyWork.Id > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = consultancyWork.Id;
                //    userLogs.UserId = Convert.ToInt16(consultancyWork.UserId);
                //    userLogs.FormName = "CONSULTANCY WORK";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(2);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(consultancyWork));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_ConsultancyWork @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
