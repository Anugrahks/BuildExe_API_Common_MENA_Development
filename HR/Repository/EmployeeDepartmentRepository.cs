using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Repository 
{
    public class EmployeeDepartmentRepository:IEmployeeDepartmentRepository 
    {
        private readonly HRContext _dbContext;
        public EmployeeDepartmentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Deletedepartment(int employeeDepartmentID,int userid)
        {
            try
            {
                var employeeDepartment = await _dbContext.tbl_EmployeeDepartment.FindAsync(employeeDepartmentID);
                if (employeeDepartment != null)
                {
                    _dbContext.tbl_EmployeeDepartment.Remove(employeeDepartment);
                    await _dbContext.SaveChangesAsync();

                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = employeeDepartmentID;
                    userLogs.UserId = Convert.ToInt16(userid);
                    userLogs.FormName = "EMPLOYEE DEPARTMENT";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(3);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<EmployeeDepartment> GetdepartmentByID(int employeeDepartmentID)
        {
            try
            {
                return  await _dbContext.tbl_EmployeeDepartment.FindAsync(employeeDepartmentID);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDepartment>> Getdepartment(int companyid, int Branchid)
        {
            try
            {
                if (Branchid == 0)
                return await _dbContext.tbl_EmployeeDepartment.Where(x => x.CompanyId == companyid).ToListAsync();
            else
                return await _dbContext.tbl_EmployeeDepartment.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == Branchid).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Insertdepartment(EmployeeDepartment employeeDepartment)
        {
            try
            {
                await _dbContext.AddAsync(employeeDepartment);
                await _dbContext.SaveChangesAsync();
                if (employeeDepartment.Id > 0)
                {
                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = employeeDepartment.Id;
                    userLogs.UserId = Convert.ToInt16(employeeDepartment.UserId);
                    userLogs.FormName = "EMPLOYEE DEPARTMENT";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(1);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }
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

        public async Task Updatedepartment(EmployeeDepartment employeeDepartment)
        {
            try
            {
                _dbContext.Entry(employeeDepartment).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                if (employeeDepartment.Id > 0)
                {
                    UserLogs userLogs = new UserLogs();
                    userLogs.MasterId = employeeDepartment.Id;
                    userLogs.UserId = Convert.ToInt16(employeeDepartment.UserId);
                    userLogs.FormName = "EMPLOYEE DEPARTMENT";
                    userLogs.EntryDate = DateTime.Now;
                    userLogs.Action = Convert.ToInt32(2);
                    await _dbContext.AddAsync(userLogs);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
