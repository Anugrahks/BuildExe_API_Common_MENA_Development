using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BuildExeHR.Repository
{
    public class WorkingHoursRepository : IWorkingHoursRepository
    {
        private readonly HRContext _dbContext;
        public WorkingHoursRepository(HRContext dbContext)
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

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkingHours @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<WorkingHours> GetdByID(int iD)
        {
            try
            {
                return await _dbContext.tbl_Workinghours.FindAsync(iD);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WorkingHours>> Get()
        {
            try
            {
                return await _dbContext.tbl_Workinghours.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Get(int companyId, int BranchId)
        {
            try
            {
                var data =await (from a in _dbContext.tbl_Workinghours
                        join b in _dbContext.tbl_EmployeeDesignation  on a.designationid  equals b.Id
                        join c in _dbContext.tbl_EmployeeCategory on b.EmployeeCategoryId  equals c.EmployeeCategoryId 
                        select new
                        {
                            id = a.Id,
                            designationid = a.designationid,
                            employeeDesignationName = b.EmployeeDesignationName ,
                            employeeCategoryId = b.EmployeeCategoryId ,
                            employeeCategoryName = c.EmployeeCategoryName ,
                            time_in = a.time_in,
                            time_out = a.time_out,
                            relaxation = a.relaxation,
                            userId = a.UserId,
                            companyId = a.CompanyId,
                            branchId = a.BranchId,
                            break_hours = a.break_hours,
                            isdefault = a.isdefault,
                            otrelaxation = a.otrelaxation,
                            latePenaltyHalfDay = a.LatePenaltyHalfDay,
                            latePenaltyHours = a.LatePenaltyHours,
                            latePenaltyCustomizations = _dbContext.tbl_LatePenaltyCustomization
                                  .Where(lp => lp.WorkingHoursId == a.Id)
                                  .Select(lp => new
                                  {
                                      lp.Id,
                                      lp.WorkingHoursId,
                                      lp.DesignationId,
                                      lp.PenaltyAfterMinutes,
                                      lp.HourSalary
                                  }).ToList()

                        }).Where(x => x.companyId == companyId).Where(x => x.branchId == BranchId).Where(x => x.isdefault == 0).OrderByDescending(x=>x.id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //return _dbContext.tbl_Workinghours.Where(x => x.CompanyId == companyId).Where(x => x.BranchId == BranchId).ToList();
        }

        public async Task<string> Getuser(int companyId, int BranchId, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_Workinghours
                                  join b in _dbContext.tbl_EmployeeDesignation on a.designationid equals b.Id
                                  join c in _dbContext.tbl_EmployeeCategory on b.EmployeeCategoryId equals c.EmployeeCategoryId
                                  select new
                                  {
                                      id = a.Id,
                                      designationid = a.designationid,
                                      employeeDesignationName = b.EmployeeDesignationName,
                                      employeeCategoryId = b.EmployeeCategoryId,
                                      employeeCategoryName = c.EmployeeCategoryName,
                                      time_in = a.time_in,
                                      time_out = a.time_out,
                                      relaxation = a.relaxation,
                                      userId = a.UserId,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      break_hours = a.break_hours,
                                      isdefault = a.isdefault,
                                      otrelaxation = a.otrelaxation,
                                      latePenaltyHalfDay = a.LatePenaltyHalfDay,
                                      latePenaltyHours = a.LatePenaltyHours,
                                      latePenaltyCustomizations = _dbContext.tbl_LatePenaltyCustomization
                                  .Where(lp => lp.WorkingHoursId == a.Id)
                                  .Select(lp => new
                                  {
                                      lp.Id,
                                      lp.WorkingHoursId,
                                      lp.DesignationId,
                                      lp.PenaltyAfterMinutes,
                                      lp.HourSalary
                                  }).ToList()

                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == BranchId).Where(x=>x.userId == UserId).Where(x=>x.isdefault==0).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //return _dbContext.tbl_Workinghours.Where(x => x.CompanyId == companyId).Where(x => x.BranchId == BranchId).ToList();
        }

        public async Task<IEnumerable<Validation>> Insert(WorkingHours workingHours)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workingHours));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkingHours @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }
        public async Task<IEnumerable<Validation>> Update(WorkingHours workingHours)
        {

            try
            {
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(workingHours));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("stPro_WorkingHours @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckWorkingHoursEditDelete @Id", Id).ToListAsync();
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
