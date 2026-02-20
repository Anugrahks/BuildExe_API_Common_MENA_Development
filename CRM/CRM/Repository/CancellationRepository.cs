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
    public class CancellationRepository : ICancellationRepository
    {
        private readonly ProductContext _dbContext;
        public CancellationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Indent =1,
            PurchaseOrder =2,
            Insert =3,
            Project =4
        }

        public async Task<string> Indent(int ProjectId, int UnitId, int BlockId ,int Division, int FloorId , int Type)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Cancellation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@Division", SqlDbType.Int) { Value = Division });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = Type });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Indent });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Project(int CompanyId, int BranchId, int UserId, int SiteUser, int TypeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Cancellation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Division", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = SiteUser });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = TypeId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Project });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        
        public async Task<string> PurchaseOrder(int ProjectId, int UnitId, int BlockId, int Division, int FloorId, int Type)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Cancellation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@Division", SqlDbType.Int) { Value = Division });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = Type });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.PurchaseOrder });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Cancellation> cancellations)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var Division = new SqlParameter("@Division", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var Type = new SqlParameter("@Type", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(cancellations));
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Cancellation @ProjectId,@UnitId,@BlockId,@Division,@FloorId,@Type,@json,@Action", ProjectId, UnitId, BlockId, Division, FloorId, Type, json, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Put(AdvanceSetting advanceSetting)
        {
            try
            {
                var FinancialYearId = new SqlParameter("@FinancialYearId", advanceSetting.FinancialYearId);
                var LabourAdvanceProject = new SqlParameter("@LabourAdvanceProject", advanceSetting.LabourAdvanceProject);
                var GroupLabourAdvanceProject = new SqlParameter("@GroupLabourAdvanceProject", advanceSetting.GroupLabourAdvanceProject);
                var ForemanAdvanceProject = new SqlParameter("@ForemanAdvanceProject", advanceSetting.ForemanAdvanceProject);
                var SupplierAdvanceProject = new SqlParameter("@SupplierAdvanceProject", advanceSetting.SupplierAdvanceProject);
                var Action = new SqlParameter("@Action", Actions.Indent);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AdvanceSetting @FinancialYearId,@LabourAdvanceProject,@GroupLabourAdvanceProject,@ForemanAdvanceProject,@SupplierAdvanceProject,@Action", FinancialYearId, LabourAdvanceProject, GroupLabourAdvanceProject, ForemanAdvanceProject, SupplierAdvanceProject, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> AdvanceSetting(int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdvanceSetting";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@LabourAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@GroupLabourAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ForemanAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SupplierAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.PurchaseOrder });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> AdvanceSettingFinancialYear(int BranchId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdvanceSetting";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@LabourAdvanceProject", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@GroupLabourAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ForemanAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SupplierAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Insert });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> ApprovalView(int BranchId, int FinancialYearId, int MenuId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdvanceSetting";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@LabourAdvanceProject", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@GroupLabourAdvanceProject", SqlDbType.Int) { Value = MenuId });
                cmd.Parameters.Add(new SqlParameter("@ForemanAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@SupplierAdvanceProject", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
