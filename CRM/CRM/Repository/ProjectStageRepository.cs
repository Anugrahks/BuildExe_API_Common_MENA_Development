using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BuildExeServices.Repository;
using Newtonsoft.Json;
using System.Data.SqlClient;

using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ProjectStageRepository:IProjectStageRepository 
    {
        private readonly ProductContext _dbContext;
        public ProjectStageRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            stageUpdate = 6,
            SelectReportJson=7
        }
        public async Task<IEnumerable<Validation>> Delete(int id, int userid)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
            var EntryUserId = new SqlParameter("@EntryUserId", userid);
            var team = new SqlParameter("@team", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectWorkStage @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectStage>> GetByID(int id, int divisionId)
        {
            try
            {
                return await _dbContext.tbl_ProjectWorkStage.Where(x => x.ProjectId == id).Where(x=>x.DivisionId == divisionId).OrderBy(x=>x.OrderId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectStage>> GetforStatusUpdate(int ProjectId,int UnitId, int DivisionId)
        {
            try
            {
                return await _dbContext.tbl_ProjectWorkStage.Where(x => x.ProjectId == ProjectId).Where(x => x.OwnProjectDetailsiId == UnitId).Where(x=>x.DivisionId == DivisionId).OrderBy(x => x.OrderId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectStage>> Get()
        {
            try
            {
                return await _dbContext.tbl_ProjectWorkStage.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectStage> projectStage)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectStage));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectWorkStage @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

       
        public async Task<IEnumerable<Validation>> Update(IEnumerable<ProjectStage> projectStage)
        {
            try
            {
                var Id = new SqlParameter("@id", "0");
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectStage));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.Update );

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectWorkStage @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> UpdateStatus(int financialyearid,IEnumerable<ProjectStage> projectStage)
        {
            try
            {
                var Id = new SqlParameter("@id", financialyearid);
            var EntryUserId = new SqlParameter("@EntryUserId", "0");
            var team = new SqlParameter("@team", JsonConvert.SerializeObject(projectStage));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var Action = new SqlParameter("@Action", Actions.stageUpdate );

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectWorkStage @id,@EntryUserId,@team, @CompanyId, @BranchId, @Action", Id, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getjson(BillSearch billSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectWorkStage";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReportJson });
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
        }

        public async Task<IEnumerable<ProjectStage>> GetForSuperAdmin(int ProjectId, int UnitId, int DivisionId)
        {
            try
            {
                return await _dbContext.tbl_ProjectWorkStage.Where(x => x.ProjectId == ProjectId).Where(x => x.OwnProjectDetailsiId == UnitId).Where(x => x.DivisionId == DivisionId).Where(x => x.StageStatusId == 2).OrderBy(x => x.OrderId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
