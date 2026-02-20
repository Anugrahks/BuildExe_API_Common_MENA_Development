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
using System.Reflection;
using System.Data.Common;

namespace BuildExeServices.Repository
{
    public class RateEvaluationRepository : IRateEvaluationRepository
    {
        private readonly ProductContext _dbContext;
        public RateEvaluationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Selectitemdetails = 4,
            Selectcharges = 5,
            Validation = 6
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<ProjectSpecificationMaster> rateEvaluations)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var unitid = new SqlParameter("@unitid", "0");
                var blockid = new SqlParameter("@blockid", "0");
                var floorid = new SqlParameter("@floorid", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(rateEvaluations));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_RateEvaluation @Id,@ProjectId,@unitid,@blockid,@floorid,@item,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, unitid, blockid, floorid, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return result;
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
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
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

        public async Task<IEnumerable<Validation>> GetVal(int projectId, int UnitId, int BlockId, int Floorid, int DivisionId, int EnquiryId)
        {
            try
            {
                var Id = new SqlParameter("@Id", DivisionId);
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var unitid = new SqlParameter("@unitid", UnitId);
                var blockid = new SqlParameter("@blockid", BlockId);
                var floorid = new SqlParameter("@floorid", Floorid);
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(""));
                var CompanyId = new SqlParameter("@CompanyId", EnquiryId);
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Validation);
                var secondAction = new SqlParameter("@secondAction", 0);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_RateEvaluation @Id,@ProjectId,@unitid,@blockid,@floorid,@item,@CompanyId,@BranchId,@userId,@Action", Id, ProjectId, unitid, blockid, floorid, item, CompanyId, BranchId, userId, Action).ToListAsync();


            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return null;
            }
        }
    }
}
