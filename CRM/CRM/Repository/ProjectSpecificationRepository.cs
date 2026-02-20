using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

using System.Data.Common;
using System.Reflection;
using System.Drawing;

namespace BuildExeServices.Repository
{
    public class ProjectSpecificationRepository : IProjectSpecificationRepository
    {
        private readonly ProductContext _dbContext;
        public ProjectSpecificationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,


            Selectforedit = 4,
            Select = 4,
            SelectAll = 5,
            getforapproval = 6,
            Saveapproval = 7,
            UpdateOneSpec = 8,
            validation = 9,
            getQuotedamt = 10,
            ProjectValidation = 11,
            SelectReport = 12
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectSpecificationMaster> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectSpecification @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> approve(IEnumerable<ProjectSpecificationMaster> specificationMasters)
        {
            try
            {
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var Projectid = new SqlParameter("@Projectid", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Saveapproval);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_RateComparison @item,@Projectid,@unitId,@blockId,@floorId,@userId,@Action", item, Projectid, unitId, blockId, floorId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<ProjectSpecificationMaster> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectSpecification @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> EstimationOrderingUpdate(IEnumerable<EstimationOrdering> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", "21");

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectSpecification @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        
        public async Task UpdateOneProjectSpec(IEnumerable<ProjectSpecificationMaster> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.UpdateOneSpec);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ProjectSpecificationMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int Idworkorder, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectSpecification @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        public async Task<string> GetEdit(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 16 });
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

        public async Task<string> GetSpecDetailsById(string Ids)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = Ids });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
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


        public async Task<string> GetEstimationId(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specificationFilters) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });
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



        public async Task<string> EstimationApprovalPost(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specificationFilters) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 18 });
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

        public async Task<string> EstimationApproval(int ProjectId, int DivisionId, int BranchId, int UserId, int EnquiryId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;



                var json = JsonConvert.SerializeObject(new { EnquiryId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = DivisionId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 17 });
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

        public async Task<string> EstimationOrdering(int ProjectId, int DivisionId, int BranchId, int UserId, int EnquiryId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;



                var json = JsonConvert.SerializeObject(new { EnquiryId });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = DivisionId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 19 });
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




        public async Task<string> GetEstimationIdList(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specificationFilters) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 22 });
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


        public async Task<string> EstimationOrderingForPrint(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = specificationFilters.EstimationIds });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 24 });
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


        public async Task<string> GetEstimationSubId(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specificationFilters) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 23 });
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
        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        public async Task Delete(int projectid, int UnitId, int Blockid, int floorid, int userid)
        {
            try
            {
                var item = new SqlParameter("@item", "");
                var Projectid = new SqlParameter("@Projectid", projectid);
                var unitId = new SqlParameter("@unitId", UnitId);
                var blockId = new SqlParameter("@blockId", Blockid);
                var floorId = new SqlParameter("@floorId", floorid);
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_RateComparison @item,@Projectid,@unitId,@blockId,@floorId,@userId,@Action", item, Projectid, unitId, blockId, floorId, userId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Validation(int projectid, int UnitId, int Blockid, int floorid, string estimationid)
        {
            try
            {
                var item = new SqlParameter("@item", estimationid);
                var Projectid = new SqlParameter("@Projectid", projectid);
                var unitId = new SqlParameter("@unitId", UnitId);
                var blockId = new SqlParameter("@blockId", Blockid);
                var floorId = new SqlParameter("@floorId", floorid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.validation);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_RateComparison @item,@Projectid,@unitId,@blockId,@floorId,@userId,@Action", item, Projectid, unitId, blockId, floorId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectSpecificationMaster>> GetbyID(int Idworkorder)
        {
            try
            {

                var list = await _dbContext.tbl_ProjectSpecificationMaster.Where(x => x.Id == Idworkorder).ToListAsync();
                var detaillist = await _dbContext.tbl_ProjectSpecificationDetails.Where(x => x.ProjSpecificationId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getbyproject(SpecificationFilters specificationFilters)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EstimationGet";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = specificationFilters.SubId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = specificationFilters.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = specificationFilters.BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = specificationFilters.FloorId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = specificationFilters.UnitId });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = specificationFilters.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@EstimationId", SqlDbType.Int) { Value = specificationFilters.EstimationId });
                cmd.Parameters.Add(new SqlParameter("@EnquiryId", SqlDbType.Int) { Value = specificationFilters.EnquiryId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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

        public async Task<IEnumerable<ProjectSpecificationMaster>> GetForApproval(int projectid, int UnitId, int Blockid, int floorid, int userid)
        {
            try
            {

                var item = new SqlParameter("@item", "");
                var Projectid = new SqlParameter("@Projectid", projectid);
                var unitId = new SqlParameter("@unitId", UnitId);
                var blockId = new SqlParameter("@blockId", Blockid);
                var floorId = new SqlParameter("@floorId", floorid);
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.getforapproval);

                var list = await _dbContext.tbl_ProjectSpecificationMaster.FromSqlRaw("Stpro_RateComparison @item,@Projectid,@unitId,@blockId,@floorId,@userId,@Action", item, Projectid, unitId, blockId, floorId, userId, Action).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetbyprojectSpec(int Companyid, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Selectforedit });
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

        public async Task<string> GetbyprojectComp(int proj, int unit, int block, int floor)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = proj });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = unit });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = block });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = floor });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 14 });
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





        public async Task<string> GetbyprojectSpecGrid(int Companyid, int Branchid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 15 });
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

        public async Task<string> GetbyprojectSpecGriduser(int Companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 15 });
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

        public async Task<string> getforApproval(int Companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = Companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 15 });
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

        public async Task<decimal> GetQuotedAmt(int projectId)
        {
            decimal id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ProjectSpecification";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.getQuotedamt });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);


                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    id = Convert.ToDecimal(dataTable.Rows[i][0].ToString());
                }
                return id;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = 0;
            }
            return id;
        }



        public async Task<string> GetbyprojectList(int projectid, int UnitId, int Blockid, int floorid, int divisionid)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_GetSpecificationListByProject";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectid });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.NVarChar) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = Blockid });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorid });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = divisionid });
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
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }



        public async Task<IEnumerable<ProjectSpecificationMaster>> GetforPartRate(int projectId)
        {
            try
            {
                var list = await _dbContext.tbl_ProjectSpecificationMaster.Where(x => x.ProjectId == projectId).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_ProjectSpecificationDetails.Where(x => x.ProjSpecificationId == detail.Id).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<ProjectSpecificationMaster>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_ProjectSpecificationMaster.ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_ProjectSpecificationDetails.Where(x => x.ProjSpecificationId == detail.Id).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectSpecificationMaster>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
                {
                    var list = await _dbContext.tbl_ProjectSpecificationMaster.Where(x => x.CompanyId == companid).OrderByDescending(o => o.Id).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_ProjectSpecificationDetails.Where(x => x.ProjSpecificationId == detail.Id).ToListAsync();
                    }
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_ProjectSpecificationMaster.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).OrderByDescending(o => o.Id).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_ProjectSpecificationDetails.Where(x => x.ProjSpecificationId == detail.Id).ToListAsync();
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int unit, int block, int floor, int DivisionId, int EnquiryId, int EstimationId)
        {
            try
            {
                var Id = new SqlParameter("@PId", id);
                var UId = new SqlParameter("@UId", unit);
                var BId = new SqlParameter("@BId", block);
                var FId = new SqlParameter("@FId", floor);
                var DId = new SqlParameter("@DId", DivisionId);
                var EId = new SqlParameter("@EId", EnquiryId);
                var EstId = new SqlParameter("@EstId", EstimationId);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckProjectSpecEditDelete @PId, @UId, @BId, @FId, @DId, @EId, @EstId", Id, UId, BId, FId,DId,EId,EstId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> CheckProject(int projectid, int UnitId, int Blockid, int floorid)
        {
            try
            {
                var Id = new SqlParameter("@Id", projectid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", UnitId);
                var BranchId = new SqlParameter("@BranchId", Blockid);
                var userId = new SqlParameter("@userId", floorid);
                var Action = new SqlParameter("@Action", Actions.ProjectValidation);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ProjectSpecification @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReport(SpecSearch specSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EstimationPrint";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = specSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = specSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string specificationdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    specificationdetails = specificationdetails + dataTable.Rows[i][0].ToString();
                }
                return specificationdetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> EstimationReport(SpecSearch specSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EstimationReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = specSearch.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = specSearch.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@EstimationIds", SqlDbType.NVarChar) { Value = specSearch.EstimationIds });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(specSearch) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = specSearch.Action });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string specificationdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    specificationdetails = specificationdetails + dataTable.Rows[i][0].ToString();
                }
                return specificationdetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

    }
}
