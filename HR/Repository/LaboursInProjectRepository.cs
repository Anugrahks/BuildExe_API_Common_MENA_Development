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
using System.Data;
using System.Reflection;
using System.Data.Common;

namespace BuildExeHR.Repository
{
    public class LaboursInProjectRepository : ILaboursInProjectRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectEmployee = 4,
            SelectAll = 5
        }

        public LaboursInProjectRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<LaboursInProject> laboursInProjects)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(laboursInProjects));
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LaboursInProject @Id,@ProjectId,@item,@userId,  @Action", Id, ProjectId, item, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int UserId)
        {
            try { 
            var Id = new SqlParameter("@Id", id);
            var ProjectId = new SqlParameter("@ProjectId", "0");
            var item = new SqlParameter("@item", "");
            var userId = new SqlParameter("@userId", UserId);
            var Action = new SqlParameter("@Action", Actions.Delete);

              return  await _dbContext.tbl_validation.FromSqlRaw("Stpro_LaboursInProject @Id,@ProjectId,@item,@userId,  @Action", Id, ProjectId, item, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<LaboursInProject> laboursInProjects)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(laboursInProjects));
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LaboursInProject @Id,@ProjectId,@item,@userId,  @Action", Id, ProjectId, item, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LaboursInProject>> Get()
        {
            try
            {
                var list = await (
                    from lp in _dbContext.tbl_LaboursInProjects
                    join b in _dbContext.tbl_Batch
                        on lp.BatchID equals b.Id into batchJoin
                    from b in batchJoin.DefaultIfEmpty() // LEFT JOIN
                    select new LaboursInProject
                    {
                        Id = lp.Id,
                        ProjectId = lp.ProjectId,
                        BatchID = lp.BatchID,
                        BatchNo = b != null ? b.BatchNo : null,
                        DateAssigned = lp.DateAssigned
                    }
                ).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LaboursInProject>> GetbyID(int Id)
        {
            try
            {
                var list = await _dbContext.tbl_LaboursInProjects.Where(x => x.Id == Id).ToListAsync();
                var detaillist =await _dbContext.tbl_LaboursInProjectDetails.Where(x => x.LaboursInProjectId == Id).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Get(int CompanyId, int BranchId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_LaboursInProjects
                                  join b in _dbContext.tbl_EmployeeCategory on a.EmployeeCategoryId equals b.EmployeeCategoryId
                                  join w in _dbContext.tbl_EmployeeLabourGroup on a.EmployeeLabourGroupId equals w.EmployeeLabourGroupId into ws
                                  from w in ws.DefaultIfEmpty()

                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                                  from e in es.DefaultIfEmpty()
                                  join g in _dbContext.tbl_Division on a.DivisionId equals g.DivisionId into gs
                                  from g in gs.DefaultIfEmpty()
                                  join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                                  from f in fs.DefaultIfEmpty()

                                  select new
                                  {
                                      id = a.Id,
                                      employeeCategoryId = a.EmployeeCategoryId,
                                      employeeCategoryName = b.EmployeeCategoryName,
                                      employeeLabourGroupId = a.EmployeeLabourGroupId,
                                      employeeLabourGroupName = w == null ? " " : w.EmployeeLabourGroupName,
                                      dateAssigned = a.DateAssigned,

                                      projectName = c.ProjectName,
                                      projectId = a.ProjectId,
                                      blockId = a.BlockId,
                                      blockName = d == null ? " " : d.BlockName,
                                      floorId = a.FloorId,
                                      floorName = e == null ? " " : e.FloorName,
                                      unitId = a.UnitId,
                                      unitName = f == null ? " " : f.UnitId,
                                      userId = a.UserId,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      divisionName = g == null ? " " : g.DivisionShortName,
                                      divisionId = a.DivisionId

                                  }).Where(x => x.companyId == CompanyId).Where(x => x.branchId == BranchId).OrderByDescending(x=>x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getuser(int CompanyId, int BranchId, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_LaboursInProjects
                                  join b in _dbContext.tbl_EmployeeCategory on a.EmployeeCategoryId equals b.EmployeeCategoryId
                                  join w in _dbContext.tbl_EmployeeLabourGroup on a.EmployeeLabourGroupId equals w.EmployeeLabourGroupId into ws
                                  from w in ws.DefaultIfEmpty()

                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                                  from e in es.DefaultIfEmpty()
                                  join g in _dbContext.tbl_Division on a.DivisionId equals g.DivisionId into gs
                                  from g in gs.DefaultIfEmpty()
                                  join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                                  from f in fs.DefaultIfEmpty()

                                  select new
                                  {
                                      id = a.Id,
                                      employeeCategoryId = a.EmployeeCategoryId,
                                      employeeCategoryName = b.EmployeeCategoryName,
                                      employeeLabourGroupId = a.EmployeeLabourGroupId,
                                      employeeLabourGroupName = w == null ? " " : w.EmployeeLabourGroupName,
                                      dateAssigned = a.DateAssigned,

                                      projectName = c.ProjectName,
                                      projectId = a.ProjectId,
                                      blockId = a.BlockId,
                                      blockName = d == null ? " " : d.BlockName,
                                      floorId = a.FloorId,
                                      floorName = e == null ? " " : e.FloorName,
                                      unitId = a.UnitId,
                                      unitName = f == null ? " " : f.UnitId,
                                      userId = a.UserId,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      divisionName = g == null ? " " : g.DivisionShortName,
                                      divisionId = a.DivisionId

                                  }).Where(x => x.companyId == CompanyId).Where(x => x.branchId == BranchId).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<EmployeeInProject>> GetEmplyeeInProject(LaboursInProject laboursInProject)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", laboursInProject.ProjectId);
                var unitId = new SqlParameter("@unitId", laboursInProject.UnitId);
                var blockId = new SqlParameter("@blockId", laboursInProject.BlockId);
                var floorId = new SqlParameter("@floorId", laboursInProject.FloorId);
                var CategoryId = new SqlParameter("@categoryId", laboursInProject.EmployeeCategoryId);
                var labourgroupid = new SqlParameter("@labourgroupid", laboursInProject.EmployeeLabourGroupId);
                var CompanyId = new SqlParameter("@CompanyId", laboursInProject.CompanyId);
                var BranchId = new SqlParameter("@BranchId", laboursInProject.BranchId);
                var AssignDate = new SqlParameter("@DateAssigned", laboursInProject.DateAssigned);
                var Action = new SqlParameter("@Action", Actions.SelectEmployee);
                var _product = await _dbContext.tbl_EmployeeInProject.FromSqlRaw("Stpro_LaboursInProject_Details @projectId,@unitId,@blockId,@floorId,@CategoryId,@labourgroupid,@CompanyId,@BranchId, @DateAssigned, @Action", projectId, unitId, blockId, floorId, CategoryId, labourgroupid, CompanyId, BranchId, AssignDate, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> EmployeeWise(HRSearch laboursInProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LaboursInProject_Details";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = laboursInProject.EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@labourgroupid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = laboursInProject.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int) { Value = laboursInProject.BranchId });
                cmd.Parameters.Add(new SqlParameter("@DateAssigned", SqlDbType.DateTime) { Value = laboursInProject.DateAssigned });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string salaryPaymentDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    salaryPaymentDetails = salaryPaymentDetails + dataTable.Rows[i][0].ToString();
                }
                return salaryPaymentDetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        
        public async Task<IEnumerable<Validation>> CheckEditDelete(int projectId, int blockId, int floorId, int unitId )
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var UnitId = new SqlParameter("@UnitId", unitId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckLabourInProjectEditDelete @ProjectId, @BlockId, @FloorId, @UnitId  ", ProjectId, BlockId, FloorId, UnitId).ToListAsync();
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
