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
using System.Data.Common;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class ProjectRepository:IProjectRepository 
    {
        private readonly HRContext _dbContext;
        public ProjectRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            selectbyType = 1,
            selectbyEmployee = 2

        }
        public async Task <IEnumerable<Project>> Get(int companyid, int branchid, int type)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var TypeId = new SqlParameter("@TypeId", type);
                var Action = new SqlParameter("@Action", Actions.selectbyType);
                var _product = await _dbContext.tbl_ProjectMaster.FromSqlRaw("Stpro_ProjectForHR @CompanyId,@BranchId,@TypeId,@Action", CompanyId, BranchId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Project>> Get(int Employeeid)
        {
            try
            {

                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var TypeId = new SqlParameter("@TypeId", Employeeid);
                var Action = new SqlParameter("@Action", Actions.selectbyEmployee);
                var _product = await _dbContext.tbl_ProjectMaster.FromSqlRaw("Stpro_ProjectForHR @CompanyId,@BranchId,@TypeId,@Action", CompanyId, BranchId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetProjectsForSiteExpense(int CompanyId, int BranchId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_LaboursInProjects
                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  select new
                                  {
                                      id = a.ProjectId,
                                      projectName = c.ProjectName,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      empCategoryId = a.EmployeeCategoryId

                                  }).Where(x => x.companyId == CompanyId).Where(x => x.branchId == BranchId). Where(x=>x.empCategoryId == 6)
                                  .OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetProjectsForSiteExpenses(int CompanyId, int BranchId, int SiteManager)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetProjectsForSiteExpense";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@SiteManager", SqlDbType.Int) { Value = SiteManager });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string projDetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    projDetails = projDetails + dataTable.Rows[i][0].ToString();
                }
                return projDetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        }
}
