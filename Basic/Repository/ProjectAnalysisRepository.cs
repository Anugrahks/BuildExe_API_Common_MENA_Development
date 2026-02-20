using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using iText.Kernel.Geom;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class ProjectAnalysisRepository:IProjectAnalysisRepository 
    {
        private readonly BasicContext _dbContext;
        public ProjectAnalysisRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            select = 1,
            selectDetail = 2,
            selectDetail_datewise =3,
            selectPrint = 4
        }
        //public async Task <IEnumerable<ProjectAnalysis >> ProjectAnalysisReport(BasicSearch basicSearch)
        //{
        //    try
        //    {
        //        basicSearch.withDate = 1;
        //        if (basicSearch.FromDate is null)
        //        {
        //            basicSearch.withDate = 0;
        //            basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
        //        }
        //        if (basicSearch.ToDate is null)
        //        {
        //            basicSearch.withDate = 0;
        //            basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
        //        }
        //        if (basicSearch.ProjectStatus is null)
        //            basicSearch.ProjectStatus = 0;
        //        var ProjectId = new SqlParameter("@ProjectId", "0");
        //        var ProjectStatus = new SqlParameter("@ProjectStatus", basicSearch.ProjectStatus);
        //        var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
        //        var todate = new SqlParameter("@todate", basicSearch.ToDate);
        //        var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
        //        var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
        //        var withDate = new SqlParameter("@withDate", basicSearch.withDate);
        //        var SiteUser = new SqlParameter("@SiteUser", basicSearch.siteUser);
        //        var SiteUserId = new SqlParameter("@SiteUserId", basicSearch.siteUserId);
        //        var UserId = new SqlParameter("@UserId", basicSearch.userId);
        //        var Type = new SqlParameter("@type", "");
        //        var Department = new SqlParameter("@Department", basicSearch.Department);
        //        var DivisionId = new SqlParameter("@DivisionId", basicSearch.DivisionId);
        //        var WorkNameId = new SqlParameter("@WorkNameId", basicSearch.WorkNameId);
        //        var WorkCategoryId = new SqlParameter("@WorkCategoryId", basicSearch.WorkCategoryId);
        //        var Action = new SqlParameter("@Action", Actions.select);


        //        var _product = await _dbContext.tbl_ProjectAnalysis.FromSqlRaw("stpro_ProjectAnalysisReport @ProjectId,@ProjectStatus,@fromdate, @todate, @CompanyId, @BranchId,@withDate,@SiteUser, @SiteUserId,@UserId ,@type,@Department,@DivisionId,@WorkNameId,@WorkCategoryId, @Action", ProjectId, ProjectStatus, fromdate, todate, CompanyId, BranchId, withDate,SiteUser, SiteUserId, UserId, Type, Department, DivisionId, WorkNameId, WorkCategoryId, Action).ToListAsync();
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<string> ProjectAnalysisReport(BasicSearch basicSearch)
        {
            try
            {
                // Default handling for dates
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ProjectStatus is null)
                    basicSearch.ProjectStatus = 0;

                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.stpro_ProjectAnalysisReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = basicSearch.ProjectStatus });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@withDate", SqlDbType.Int) { Value = basicSearch.withDate });
                cmd.Parameters.Add(new SqlParameter("@SiteUser", SqlDbType.NVarChar) { Value = basicSearch.siteUser });
                cmd.Parameters.Add(new SqlParameter("@SiteUserId", SqlDbType.Int) { Value = basicSearch.siteUserId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.userId });
                cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.Int) { Value = basicSearch.Department });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = basicSearch.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId", SqlDbType.Int) { Value = basicSearch.WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@WorkCategoryId", SqlDbType.Int) { Value = basicSearch.WorkCategoryId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.select });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Handle possible JSON subqueries (FOR JSON PATH results)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }

        public async Task<IEnumerable<ProjectAnalysisDetail>> ProjectAnalysisReportDetail(BasicSearch basicSearch)
        {
            try
            {
                basicSearch.withDate = 1;
                if (basicSearch.FromDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ProjectStatus is null)
                    basicSearch.ProjectStatus = 0;

                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var ProjectStatus = new SqlParameter("@ProjectStatus", basicSearch.ProjectStatus);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var SiteUser = new SqlParameter("@SiteUser", basicSearch.siteUser);
                var SiteUserId = new SqlParameter("@SiteUserId", basicSearch.siteUserId);
                var UserId = new SqlParameter("@UserId", basicSearch.userId);
                var withDate = new SqlParameter("@withDate", basicSearch.withDate);
                var Type = new SqlParameter("@type", "");
                var Department = new SqlParameter("@Department", basicSearch.Department);
                var DivisionId = new SqlParameter("@DivisionId", basicSearch.DivisionId);
                var WorkNameId = new SqlParameter("@WorkNameId", basicSearch.WorkNameId);
                var WorkCategoryId = new SqlParameter("@WorkCategoryId", basicSearch.WorkCategoryId);
                var Json = new SqlParameter("@json", JsonConvert.SerializeObject(basicSearch));
                var Action = new SqlParameter("@Action", Actions.selectDetail);
                
                var _product = await _dbContext.tbl_ProjectAnalysisdetail.FromSqlRaw("stpro_ProjectAnalysisReport @ProjectId,@ProjectStatus,@fromdate, @todate, @CompanyId, @BranchId,@withDate,@SiteUser, @SiteUserId,@UserId ,@type,@Department,@DivisionId,@WorkNameId,@WorkCategoryId,@json, @Action", ProjectId, ProjectStatus, fromdate, todate, CompanyId, BranchId, withDate, SiteUser, SiteUserId, UserId, Type, Department, DivisionId, WorkNameId, WorkCategoryId, Json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectAnalysisDetail_Datewise>> ProjectAnalysisReportDetail_Datewise(BasicSearch basicSearch)
        {
            try
            {
                if(basicSearch.withDate is null)
                {
                    basicSearch.withDate = 0;
                }
                if (basicSearch.FromDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ProjectStatus is null)
                    basicSearch.ProjectStatus = 0;

                var ProjectId = new SqlParameter("@ProjectId", basicSearch.ProjectId);
                var ProjectStatus = new SqlParameter("@ProjectStatus", basicSearch.ProjectStatus);
                var fromdate = new SqlParameter("@fromdate", basicSearch.FromDate);
                var todate = new SqlParameter("@todate", basicSearch.ToDate);
                var CompanyId = new SqlParameter("@CompanyId", basicSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", basicSearch.BranchId);
                var SiteUser = new SqlParameter("@SiteUser", basicSearch.siteUser);
                var SiteUserId = new SqlParameter("@SiteUserId", basicSearch.siteUserId);
                var UserId = new SqlParameter("@UserId", basicSearch.userId);
                var withDate = new SqlParameter("@withDate", basicSearch.withDate);
                var Type = new SqlParameter("@type", basicSearch.Particular);
                var Department = new SqlParameter("@Department", basicSearch.Department);
                var DivisionId = new SqlParameter("@DivisionId", basicSearch.DivisionId);
                var WorkNameId = new SqlParameter("@WorkNameId", basicSearch.WorkNameId);
                var WorkCategoryId = new SqlParameter("@WorkCategoryId", basicSearch.WorkCategoryId);
                var Json = new SqlParameter("@json", "{}");
                var Action = new SqlParameter("@Action", Actions.selectDetail_datewise);

                var _product = await _dbContext.tbl_ProjectAnalysisdetail_Datewise.FromSqlRaw("stpro_ProjectAnalysisReport @ProjectId,@ProjectStatus,@fromdate, @todate, @CompanyId, @BranchId,@withDate,@SiteUser, @SiteUserId,@UserId ,@type,@Department,@DivisionId,@WorkNameId,@WorkCategoryId, @json, @Action", ProjectId, ProjectStatus, fromdate, todate, CompanyId, BranchId, withDate, SiteUser, SiteUserId, UserId, Type, Department, DivisionId, WorkNameId, WorkCategoryId, Json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> ProjectAnalysisReportPrint(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.withDate is null)
                {
                    basicSearch.withDate = 0;
                }
                if (basicSearch.FromDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ProjectStatus is null)
                    basicSearch.ProjectStatus = 0;


                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_ProjectAnalysisReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = basicSearch.ProjectStatus });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@withDate", SqlDbType.Int) { Value = basicSearch.withDate });
                cmd.Parameters.Add(new SqlParameter("@SiteUser", SqlDbType.Int) { Value = basicSearch.siteUser });
                cmd.Parameters.Add(new SqlParameter("@SiteUserId", SqlDbType.Int) { Value = basicSearch.siteUserId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.userId });
                cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.Int) { Value = basicSearch.Department });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = basicSearch.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId", SqlDbType.Int) { Value = basicSearch.WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@WorkCategoryId", SqlDbType.Int) { Value = basicSearch.WorkCategoryId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectPrint });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string projectanalysis = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    projectanalysis = projectanalysis + dataTable.Rows[i][0].ToString();
                }
                if(projectanalysis == "")
                {
                    projectanalysis = "[]";
                }
                return projectanalysis;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> ProjectAnalysisReportPrintDashboard(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.withDate is null)
                {
                    basicSearch.withDate = 0;
                }
                if (basicSearch.FromDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.FromDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ToDate is null)
                {
                    //basicSearch.withDate = 0;
                    basicSearch.ToDate = Convert.ToDateTime("2000-01-01");
                }
                if (basicSearch.ProjectStatus is null)
                    basicSearch.ProjectStatus = 0;


                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "stpro_ProjectAnalysisReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = basicSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = basicSearch.ProjectStatus });
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@withDate", SqlDbType.Int) { Value = basicSearch.withDate });
                cmd.Parameters.Add(new SqlParameter("@SiteUser", SqlDbType.Int) { Value = basicSearch.siteUser });
                cmd.Parameters.Add(new SqlParameter("@SiteUserId", SqlDbType.Int) { Value = basicSearch.siteUserId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.userId });
                cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.Int) { Value = basicSearch.Department });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = basicSearch.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@WorkNameId", SqlDbType.Int) { Value = basicSearch.WorkNameId });
                cmd.Parameters.Add(new SqlParameter("@WorkCategoryId", SqlDbType.Int) { Value = basicSearch.WorkCategoryId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

 
        public async Task<string> GetProjectGraph( int CompanyId, int Branchid, int ProjectId, int PageNumber, int RowsPerPage, int UnitId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_IncomeExpenseGraph";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int) { Value = PageNumber });
                cmd.Parameters.Add(new SqlParameter("@RowsPerPage", SqlDbType.Int) { Value = RowsPerPage });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(new { UnitId }) });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> CashFlowGraph(int CompanyId, int Branchid, int FinancialYearId, int ProjectId,int UnitId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_CashFlowGraph";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(new { UnitId }) });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> DocumentUpload(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_DocumentUploadView";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Form", SqlDbType.NVarChar) { Value = basicSearch.Form });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = basicSearch.Id });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }

}
