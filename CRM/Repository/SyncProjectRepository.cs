using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BuildExeServices.Repository
{
    public class SyncProjectRepository : ISyncProjectRepository
    {
        private readonly ProductContext _dbContext;
        public SyncProjectRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            ProjectBlockFloorAsign         = 1,
            ProjectBookingCancellation     = 2,
            ProjectConsultancyDetails      = 3,
            ProjectDivision                = 4,
            ProjectMaster                  = 5,
            ProjectPaymentMode             = 6,
            ProjectStagePlanning           = 7,
            ProjectStagePlanningDetails    = 8,
            ProjectStagePlanningUsers      = 9,
            ProjectStageSettings           = 10,
            ProjectWorkStage               = 11,
            OwnProjectDetails              = 12,
            Company                        = 13,
            Users                          = 14,
            StageActivityDetails           = 15,
            WorkEnquiryStageSetting        = 16,
            WorkEnquiryStageSettingDetails = 17,
            WorkEnquiryStageSettingUsers   = 18


        }                                  


        public async Task<string> ProjectBlockFloorAssign(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectBlockFloorAsign });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectBookingCancellation(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectBookingCancellation });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectConsultancyDetails(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectConsultancyDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectDivision(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectDivision });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectMaster(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectMaster });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectPaymentMode(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectPaymentMode });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectStagePlanning(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectStagePlanning });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> ProjectStagePlanningDetails(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectStagePlanningDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectStagePlanningUsers(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectStagePlanningUsers });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectStageSettings(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectStageSettings });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ProjectWorkStage(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.ProjectWorkStage });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> OwnProjectDetails(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.OwnProjectDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Company(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Company });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Users(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Users });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> StageActivityDetails(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.StageActivityDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> WorkEnquiryStageSetting(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.WorkEnquiryStageSetting });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> WorkEnquiryStageSettingDetails(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.WorkEnquiryStageSettingDetails });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> WorkEnquiryStageSettingUsers(SyncProject syncProject)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SyncProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = syncProject.Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.WorkEnquiryStageSettingUsers });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string SyncProject = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SyncProject = SyncProject + dataTable.Rows[i][0].ToString();
                }

                return SyncProject;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}

