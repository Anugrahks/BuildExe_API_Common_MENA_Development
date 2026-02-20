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
using System.Data.Common;
using System.Data;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class EmployeeListRepository : IEmployeeListRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            SelectReport = 6,
            SelectbyLabourGroup = 7,
                SelectSMbyProj = 8,
                SelectAllByProjectId =10,
            GetByProjectId = 11,

        }

        public EmployeeListRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task <IEnumerable<EmployeeListPersonalLedger>> Get(int companid, int branchid, int categoryId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", "");
                var CategoryId = new SqlParameter("@categoryId", categoryId);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var _product = await _dbContext.tbl_EmployeeMasterlistPersonal.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync ();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeList>> GetAdvance(int companid, int branchid, int categoryId, int ProjectId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", "");
                var CategoryId = new SqlParameter("@categoryId", categoryId);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", ProjectId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeList>> Getsiteuser(int companid, int branchid, int categoryId, int sitemanager, int sitemanagerid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", "");
                var CategoryId = new SqlParameter("@categoryId", categoryId);
                var DepartmentId = new SqlParameter("@DepartmentId", sitemanager);
                var DesignationId = new SqlParameter("@DesignationId", sitemanagerid);
                var Action = new SqlParameter("@Action", 12);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeList>> Getsiteuser(int companid, int branchid)
        {
            try
            {
                var companyParam = new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = companid
                };

                var branchParam = new SqlParameter("@BranchId", SqlDbType.Int)
                {
                    Value = branchid
                };

                var result = await _dbContext.tbl_EmployeeMasterlist
                    .FromSqlRaw(
                        "EXEC Stpro_EmployeeNotinBatch @CompanyId, @BranchId",
                        companyParam,
                        branchParam
                    )
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeList>> GetEmpByProject(int companid, int branchid, int categoryId, int projectId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", "");
                var CategoryId = new SqlParameter("@categoryId", categoryId);
                var DepartmentId = new SqlParameter("@DepartmentId", projectId);
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var Action = new SqlParameter("@Action", Actions.GetByProjectId);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<EmployeeList>> GetEmpByProject(int companid, int branchid, int categoryId, int projectId, int sitemanager, int sitemanagerid)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", sitemanager);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", "");
                var CategoryId = new SqlParameter("@categoryId", categoryId);
                var DepartmentId = new SqlParameter("@DepartmentId", projectId);
                var DesignationId = new SqlParameter("@DesignationId", sitemanagerid);
                var Action = new SqlParameter("@Action", 13);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<EmployeeList>> GetByProj(int companid, int branchid, int projectId, int unitId, int blockId, int floorId)
        {
            try
            {

                //var list = await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companid).
                //    Where(x=>x.BranchId==branchid).Where(x=>x ToListAsync();
                //return list;
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", projectId);
                var CategoryId = new SqlParameter("@categoryId", unitId);
                var DepartmentId = new SqlParameter("@DepartmentId", blockId);
                var DesignationId = new SqlParameter("@DesignationId", floorId);
                var Action = new SqlParameter("@Action", Actions.SelectAllByProjectId);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<EmployeeList>> GetByProj(int companid, int branchid, int projectId, int unitId, int blockId, int floorId, int sitemanager, int sitemanagerid)
        {
            try
            {

                //var list = await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companid).
                //    Where(x=>x.BranchId==branchid).Where(x=>x ToListAsync();
                //return list;
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", projectId);
                var CategoryId = new SqlParameter("@categoryId", unitId);
                var DepartmentId = new SqlParameter("@DepartmentId", blockId);
                var DesignationId = new SqlParameter("@DesignationId", floorId);
                var Sitemanager = new SqlParameter("@Sitemanager", blockId);
                var SitemanagerId = new SqlParameter("@SitemanagerId", floorId);
                var Action = new SqlParameter("@Action", 1);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeListsiteUser @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId,@Sitemanager,@SitemanagerId,  @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Sitemanager, SitemanagerId , Action).ToListAsync();
                return _product;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeList>> Get(int companid, int branchid, int projectId, int unitId, int blockId, int floorId)
        {
            try
            {

                //var list = await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companid).
                //    Where(x=>x.BranchId==branchid).Where(x=>x ToListAsync();
                //return list;
                var CompanyId = new SqlParameter("@CompanyId", companid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var item = new SqlParameter("@item", projectId);
                var CategoryId = new SqlParameter("@categoryId", unitId);
                var DepartmentId = new SqlParameter("@DepartmentId", blockId);
                var DesignationId = new SqlParameter("@DesignationId", floorId);
                var Action = new SqlParameter("@Action", Actions.SelectSMbyProj);
                var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
                return _product;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<EmployeeList>> Get(int departmentId, int desinationId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var item = new SqlParameter("@item", "");
            var CategoryId = new SqlParameter("@categoryId", "0");
            var DepartmentId = new SqlParameter("@DepartmentId", departmentId);
            var DesignationId = new SqlParameter("@DesignationId", desinationId);
            var Action = new SqlParameter("@Action", Actions.Select);
            var _product = await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetReport(HRSearch hRSearch )
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmployeeList";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("Designationid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purcasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purcasedetails = purcasedetails + dataTable.Rows[i][0].ToString();
                }
                if (purcasedetails == "")
                    purcasedetails = "[]";
                return purcasedetails;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            //try
            //{
            //    var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId );
            //var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId );
            //var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
            //var CategoryId = new SqlParameter("@categoryId", "0");
            //var DepartmentId = new SqlParameter("@DepartmentId", "0");
            //var DesignationId = new SqlParameter("@DesignationId", "0");
            //var Action = new SqlParameter("@Action", Actions.SelectReport);
            //var _product =await _dbContext.tbl_EmployeeReport .FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
            //return _product;
            //}
            //catch (Exception)
            //{ throw; }
        }
        public async Task<IEnumerable<EmployeeList>> GetByLabourGroup(int companid, int branchid, int LabourGroupId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", companid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var item = new SqlParameter("@item", "");
            var CategoryId = new SqlParameter("@categoryId", "0");
            var DepartmentId = new SqlParameter("@DepartmentId", "0");
            var DesignationId = new SqlParameter("@DesignationId", LabourGroupId);
            var Action = new SqlParameter("@Action", Actions.SelectbyLabourGroup);
            var _product =await _dbContext.tbl_EmployeeMasterlist.FromSqlRaw("Stpro_EmployeeList @CompanyId,@BranchId,@item,@CategoryId,@DepartmentId,@DesignationId, @Action", CompanyId, BranchId, item, CategoryId, DepartmentId, DesignationId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ListEmployeeByCategory>> GetEmployeeListById(int employeeCategoryId, int EmployeeLabourGroupId, int CompanyId, int BranchId)
        {
            try
            {
                var EmployeeCategoryId = new SqlParameter("@EmployeeCategoryId", employeeCategoryId);
                var employeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", EmployeeLabourGroupId);
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var employeeList = await _dbContext.tbl_ListEmployeeByCategory.FromSqlRaw("stpro_GetEmployeeListByCategoryId @EmployeeCategoryId,@EmployeeLabourGroupId , @CompanyId, @BranchId",
                EmployeeCategoryId, employeeLabourGroupId, companyId, branchId).ToListAsync();
                return employeeList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ListEmployeeByCategory>> Getemployeebylabourheadid(int id)
        {
            try
            {
                var EmployeeCategoryId = new SqlParameter("@EmployeeCategoryId", "0");
                var employeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", "0");
                var companyId = new SqlParameter("@CompanyId", id);
                var branchId = new SqlParameter("@BranchId", "0");
                var employeeList = await _dbContext.tbl_ListEmployeeByCategory.FromSqlRaw("stpro_GetEmployeeListByCategoryId @EmployeeCategoryId,@EmployeeLabourGroupId , @CompanyId, @BranchId",
                EmployeeCategoryId, employeeLabourGroupId, companyId, branchId).ToListAsync();
                return employeeList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> NumberofDaysInMonth(int CompanyId, int BranchId,int DesignationId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ValidationsinPayroll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date) { Value = DateTime.Now });
                cmd.Parameters.Add(new SqlParameter("@employeeid", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@leaveid", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int) { Value = DesignationId });
                cmd.Parameters.Add(new SqlParameter("@durationId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string details = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    details = details + dataTable.Rows[i][0].ToString();
                }
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }

}

