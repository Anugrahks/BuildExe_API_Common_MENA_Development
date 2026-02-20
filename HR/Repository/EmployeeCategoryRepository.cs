using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace BuildExeHR.Repository
{
    public class EmployeeCategoryRepository:IEmployeeCategoryRepository 
    {
        private readonly HRContext _dbContext;
        public EmployeeCategoryRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<EmployeeCategory>> Get()
        {
            try
            {
                return await _dbContext.tbl_EmployeeCategory.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeCategory>> GetAdvance()
        {
            try
            {
                var _productList = await _dbContext.tbl_EmployeeCategory.FromSqlRaw("Stpro_EmployeeCategory").ToListAsync();
                return _productList;
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
        public async Task Insert(EmployeeCategory employeeCategory  )
        {
            try
            {
                await _dbContext.AddAsync(employeeCategory);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Employee>> GetbyCategoryPersonal(int Companyid, int Branchid, int EmployeeCategory)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");

                var Action = new SqlParameter("@Action", "9");
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
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
