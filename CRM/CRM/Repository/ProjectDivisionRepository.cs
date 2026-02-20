using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ProjectDivisionRepository : IProjectDivisionRepository
    {
        private readonly ProductContext _dbContext;
        public ProjectDivisionRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            project = 1,
            Block = 2,
            Floor = 3,
            Unit = 4,
            Select = 5,
            UnitByProj = 6
        }
        public async Task<IEnumerable<ProjectDivision>> Getproject(int Companyid, int Branchid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", "");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var Action = new SqlParameter("@Action", Actions.project);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivision @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<long> Gettype(int projectid)
        {
            long id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_IsProject_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectid });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Direction = ParameterDirection.Output });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                await cmd.ExecuteNonQueryAsync();
                id = (long)cmd.Parameters["@id"].Value;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = 0;
            }
            return id;



        }
        public async Task<IEnumerable<ProjectDivision>> GetBlock(int Projectid)
        {
            try
            {

                //List<ProjectDivision> projDivisions = new List<ProjectDivision>();
                //var det = await (from a in _dbContext.tbl_Project_BlockFloorAssign
                //                 join b in _dbContext.tbl_Block on a.BlockId equals b.BlockId
                //                 select new 
                //                 {
                //                     blockId = a.BlockId,
                //                     id = 0,
                //                     blockName = b.BlockName,
                //                     projectId = a.ProjectId,
                //                     projectName = "0",
                //                     floorId =0,
                //                     floorName = "0",
                //                     unitId = 0,
                //                     unitName ="0",
                //                     active = a.IsActive
                //                 }).Where(x => x.projectId == Projectid).ToListAsync();
                //foreach(var b in det.Distinct())
                //{
                //    if (b.active == 1)
                //    {
                //        ProjectDivision projDiv = new ProjectDivision();
                //        projDiv.id = b.id;
                //        projDiv.UnitId = b.unitId;
                //        projDiv.FloorId = b.floorId;
                //        projDiv.BlockId = b.blockId;
                //        projDiv.ProjectId = b.projectId;
                //        projDiv.UnitName = b.unitName;
                //        projDiv.FloorName = b.floorName;
                //        projDiv.BlockName = b.blockName;
                //        projDiv.ProjectName = b.projectName;
                //        projDivisions.Add(projDiv);
                //    }
                //}
                //return projDivisions;

                //Incorrect data getting while using below code. so introduced linq for the same

                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", 2);
                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivision @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectDivision>> GetFloor(int Projectid, int Blockid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", Blockid);
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Floor);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivision @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectDivision>> GetUnit(int Projectid, int Blockid, int Floorid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", Blockid);
                var FloorId = new SqlParameter("@FloorId", Floorid);
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Unit);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivision @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        #region Project, Block, Floor Unit
        public async Task<IEnumerable<ProjectDivision>> GetProj(int Companyid, int Branchid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", "");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var Action = new SqlParameter("@Action", Actions.project);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivisionMain @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectDivision>> GetBlockByProj(int Projectid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", 2);
                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivisionMain @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectDivision>> GetFloorByProjBlock(int Projectid, int Blockid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", Blockid);
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Floor);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivisionMain @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectDivision>> GetUnitByProjBlockFloor(int Projectid, int Blockid, int Floorid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", Blockid);
                var FloorId = new SqlParameter("@FloorId", Floorid);
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Unit);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivisionMain @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ProjectDivision>> GetUnitByProj(int Projectid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", Projectid);
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "");
                var UnitId = new SqlParameter("@UnitId", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.UnitByProj);

                var _product = await _dbContext.tbl_ProjectDivision.FromSqlRaw("Stpro_ProjectDivisionMain @ProjectId,@BlockId,@FloorId,@UnitId, @CompanyId, @BranchId, @Action", ProjectId, BlockId, FloorId, UnitId, CompanyId, BranchId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion
    }
}
