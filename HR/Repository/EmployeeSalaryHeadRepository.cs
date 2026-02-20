using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.DBContexts;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class EmployeeSalaryHeadRepository : IEmployeeSalaryHeadRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,

        }
        public EmployeeSalaryHeadRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int employeID, int Userid)
        {
            try
            { //var employee = _dbContext.tbl_EmployeeSalaryHead.Find(employeID);

                //_dbContext.tbl_EmployeeSalaryHead.Remove(employee);
                //Save();
                var Id = new SqlParameter("@Id", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", employeID);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", Userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_EmployeeSalaryHead @Id, @EmployeeId, @item, @CompanyId, @BranchId, @userId, @Action", Id, EmployeeId, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetByID(int ID)
        {
            try
            {
                // return _dbContext.tbl_EmployeeSalaryHead.Where(x => x.EmployeeId  == ID).ToList();
                var data = await (from a in _dbContext.tbl_EmployeeSalaryHead
                                  join b in _dbContext.tbl_SalaryItemHead on a.SalaryItemHeadId equals b.Id
                                  select new
                                  {
                                      employeeId = a.EmployeeId,
                                      salaryItemHeadId = a.SalaryItemHeadId,
                                      headName = b.HeadName,
                                      salaryHeadTypeId = b.SalaryHeadTypeId,
                                      calculateOn = b.CalculateOn,
                                      calculationMode = b.CalculationMode,
                                      remarks = b.Remarks,
                                      varyingHead = b.VaryingHead,
                                      upperLimit = b.UpperLimit,
                                      deductLeave = b.DeductLeave,
                                      effectiveFrom = a.EffectiveFrom,
                                      active = a.Active,
                                      rate = a.Rate,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      employerContributionPer=a.EmployerContributionPer,
                                      employerContributionAmount=a.EmployerContributionAmount,


    }).Where(x => x.employeeId == ID).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int Companyid, int Branchid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_EmployeeSalaryHead
                                  join b in _dbContext.tbl_EmployeeMaster on a.EmployeeId equals b.Id
                                  where a.CompanyId == Companyid && a.BranchId == Branchid
                                  orderby a.Id descending
                                  select new
                                  {
                                      employeeId = a.EmployeeId,
                                      fullName = b.FullName,
                                      effectiveFrom = a.EffectiveFrom,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId
                                  }).ToListAsync();

                var datagroup = data.GroupBy(x => x.employeeId).Select(group => group.First());

                string jsonString = System.Text.Json.JsonSerializer.Serialize(datagroup);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insert(IEnumerable<EmployeeSalaryHead> employeeSalaryHead)
        {
            try
            { //_dbContext.Add(employeeSalaryHead);
              //Save();
                foreach (var member in employeeSalaryHead)
                {
                    member.Active = "Y";
                }


                var Id = new SqlParameter("@Id", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(employeeSalaryHead));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_EmployeeSalaryHead @Id,@EmployeeId,@item,@CompanyId,@BranchId,@userId,@Action", Id, EmployeeId, item, CompanyId, BranchId, userId, Action);

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

        public async Task Update(IEnumerable<EmployeeSalaryHead> employeeSalaryHead)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(employeeSalaryHead));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_EmployeeSalaryHead @Id,@EmployeeId,@item,@CompanyId,@BranchId,@userId,@Action", Id, EmployeeId, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //_dbContext.Entry(employeeSalaryHead).State = EntityState.Modified;
            //Save();
        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int isDelete)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var IsDelete = new SqlParameter("@IsDelete", isDelete);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSalarySettingsEditDelete @Id, @IsDelete", Id, IsDelete).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }
        public async Task<IEnumerable<Validation>> ChecksalaryheadDelete(int id, int employeeid, int isDelete)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var EmployeeId = new SqlParameter("@EmployeeId", employeeid);
                var IsDelete = new SqlParameter("@IsDelete", isDelete);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSalarySettingsEditDelete @Id,@EmployeeId,@IsDelete", Id, EmployeeId, IsDelete).ToListAsync();
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
