using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ProductContext _dbContext;

        public DepartmentRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<Validation>> Insertdepartment(Department department)
        { 
            try {
                //await _dbContext.AddAsync(department);
                //await _dbContext.SaveChangesAsync();

                //if (department.DepartmentId > 0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = department.DepartmentId;
                //    userLogs.UserId = Convert.ToInt16(department.UserId);
                //    userLogs.FormName = "DEPARTMENT";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(1);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(department));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Department @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return workname;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Updatedepartment(Department department)
        {
            try
            {
                //_dbContext.Entry(department).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
                //if(department .DepartmentId >0)
                //{
                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = department.DepartmentId;
                //    userLogs.UserId = Convert.ToInt16(department.UserId);
                //    userLogs.FormName = "DEPARTMENT";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(2);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}
                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(department));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var workname = await _dbContext.tbl_validation.FromSqlRaw("stPro_Department @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
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

        public async Task<IEnumerable<Validation>> Deletedepartment(int departmentId,int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", departmentId);
                var json = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Formname = new SqlParameter("@Formname", "DEPARTMENT");
                var Action = new SqlParameter("@Action", "1");
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_MasterDeleteValidation @Id,@json, @CompanyId, @BranchId,@UserId,@Formname, @Action", Id, json, CompanyId, BranchId, UserId, Formname, Action).ToListAsync();
                return purchaseList;

                //var department = await  _dbContext.tbl_Departments.FindAsync(departmentId);
                //if (department != null)
                //{
                //     _dbContext.tbl_Departments.Remove(department);
                //    await _dbContext.SaveChangesAsync();

                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = departmentId;
                //    userLogs.UserId = Convert.ToInt16(userid);
                //    userLogs.FormName = "DEPARTMENT";
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

        public async Task<IEnumerable<Department >> Getdepartment()
        {
            try { 
            return await _dbContext.tbl_Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Department>> Getdepartment(int CompanyId, int BranchId)
        {
            try
            {
                if (BranchId == 0)
                    return await _dbContext.tbl_Departments.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.DepartmentId).ToListAsync();
                else
                    return await _dbContext.tbl_Departments.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).OrderByDescending(x => x.DepartmentId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Department>> GetEmployeeDept(int CompanyId, int BranchId)
        {
            try
            {
                if (BranchId == 0)
                    return await _dbContext.tbl_Departments.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.DepartmentId).ToListAsync();
                else
                    return await _dbContext.tbl_Departments.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).Where(x => x.EmployeeDept == 1).OrderByDescending(x => x.DepartmentId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        

        public async Task<Department> GetdepartmentByID(int departmentId)
        {
            try
            {
                return await _dbContext.tbl_Departments.FindAsync(departmentId);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int departmentId)
        {
            try
            {
                var Id = new SqlParameter("@Id", departmentId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckDeptEditDelete @Id", Id).ToListAsync();
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
