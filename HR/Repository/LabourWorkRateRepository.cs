using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class LabourWorkRateRepository : ILabourWorkRateRepository
    {
        private readonly HRContext _dbContext;
        public LabourWorkRateRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }
        public async Task<IEnumerable<Validation>> Delete(int employeID, int userid)
        {
            try
            {

                var Id = new SqlParameter("@Id", employeID);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Formname = new SqlParameter("@Formname", "LABOUR WORK RATE");
                var Action = new SqlParameter("@Action", "1");
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MasterDeleteValidation @Id,@json, @CompanyId, @BranchId,@UserId,@Formname, @Action", Id, json, CompanyId, BranchId, UserId, Formname, Action).ToListAsync();
                return purchaseList;
                //var labourWorkRate = _dbContext.tbl_LabourWorkRate.Find(employeID);
                //if (labourWorkRate != null)
                //{
                //    _dbContext.tbl_LabourWorkRate.Remove(labourWorkRate);
                //    _dbContext.SaveChanges();

                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = employeID;
                //    userLogs.UserId = Convert.ToInt16(userid);
                //    userLogs.FormName = "LABOUR WORK RATE";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(3);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<LabourWorkRate> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_LabourWorkRate.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourWorkRate>> Get(int companyid, int branchid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_LabourWorkRate.Where(x => x.CompanyId == companyid).ToListAsync();
                else
                    return await _dbContext.tbl_LabourWorkRate.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getworkrate(int companyid, int branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_LabourWorkRate
                                  join c in _dbContext.tbl_Units on a.UnitId equals c.UnitId
                                  select new
                                  {
                                      id = a.Id,
                                      specItemTypeId = a.SpecItemTypeId,
                                      labourWorkName = a.LabourWorkName,
                                      unitId = a.UnitId,
                                      unitShortName = c.UnitShortName,
                                      rate = a.Rate,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      userId = a.UserId


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

        public async Task<string> Getworkratebyuser(int companyid, int branchid, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_LabourWorkRate
                                  join c in _dbContext.tbl_Units on a.UnitId equals c.UnitId
                                  select new
                                  {
                                      id = a.Id,
                                      specItemTypeId = a.SpecItemTypeId,
                                      labourWorkName = a.LabourWorkName,
                                      unitId = a.UnitId,
                                      unitShortName = c.UnitShortName,
                                      rate = a.Rate,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      userId = a.UserId


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

        public async Task<string> Get(int companyid, int branchid, int specitemtypeid)
        {
            try
            {

                var data = await (from a in _dbContext.tbl_LabourWorkRate
                                  join b in _dbContext.tbl_Units on a.UnitId equals b.UnitId
                                  select new
                                  {
                                      id = a.Id,
                                      specItemTypeId = a.SpecItemTypeId,
                                      labourWorkName = a.LabourWorkName,
                                      unitId = a.UnitId,
                                      unitShortName = b.UnitShortName,
                                      rate = a.Rate,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      userId = a.UserId


                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == branchid).Where(x => x.specItemTypeId == specitemtypeid).OrderByDescending(i => i.id).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(LabourWorkRate labourWorkRate)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(labourWorkRate));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_LabourWorkRate @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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

        public async Task<IEnumerable<Validation>> Update(LabourWorkRate labourWorkRate)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(labourWorkRate));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_LabourWorkRate @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckLabourWorkRateEditDelete @Id", Id).ToListAsync();
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
