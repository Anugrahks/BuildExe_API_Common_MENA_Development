using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public class WeeklyBillRepository : IWeeklyBillRepository
    {
        private readonly ProductContext _dbContext;
        public WeeklyBillRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectoneDetails = 6,
            lastBill = 7,
            autono = 8,
            GetSpecDettails = 9,
            ProjectValidation = 15,
            SpectDetailsList = 16
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<WeeklyBill> weeklyBills)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(weeklyBills));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<WeeklyBill> weeklyBills)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(weeklyBills));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Validation(int projectId, int blockId, int floorId, int unitId)
        {
            try
            {
                var Id = new SqlParameter("@ProjectId", projectId);
                var BId = new SqlParameter("@BlockId", blockId);
                var FId = new SqlParameter("@FloorId", floorId);
                var UId = new SqlParameter("@UnitId", unitId);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_WeeklyBillValidation @ProjectId, @BlockId, @FloorId, @UnitId", Id, BId, FId, UId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<WeeklyBill>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_WeeklyBill.Where(x => x.Id == Idworkorder).ToListAsync();
                var detaillist = await _dbContext.tbl_WeeklyBillDetails.Where(x => x.WeeklyBillId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<WeeklyBillList>> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserID);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_WeeklyBillList.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<WeeklyBillList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_WeeklyBillList.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WeeklyBillList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_WeeklyBillList.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<WeeklyBill>> GetLastBill(int projectId, int unitId, int blockId, int floorId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId", unitId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var Action = new SqlParameter("@Action", Actions.lastBill);

                var _product = await _dbContext.tbl_WeeklyBill.FromSqlRaw("Stpro_WeeklyBillDetail @Id,@ProjectId,@UnitId,@BlockId,@FloorId,@Action", Id, ProjectId, UnitId, BlockId, FloorId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId, int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WeeklyBillDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = blockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectoneDetails });
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
        public async Task<IEnumerable<WeeklyBillDetailsList>> GetSpec(int projectId, int unitId, int blockId, int floorId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId", unitId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var Action = new SqlParameter("@Action", Actions.GetSpecDettails);

                var _product = await _dbContext.tbl_WeeklyBillDetailsList.FromSqlRaw("Stpro_WeeklyBillDetail @Id,@ProjectId,@UnitId,@BlockId,@FloorId,@Action", Id, ProjectId, UnitId, BlockId, FloorId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<long> GetAutoNo(int projectId, int unitId, int blockId, int floorId)
        {
            long id = 0;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WeeklyBillDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = blockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });

                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.autono });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    id = Convert.ToInt32(dataTable.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = 0;
            }
            return id;
        }


        public async Task<string> GetWeeklyBillByBillNo(IEnumerable<WeeklyBillByDates> weeklyBillByDates)
        {
            try
            {
                //    var Id = new SqlParameter("@Id", "0");
                //    var item = new SqlParameter("@item", JsonConvert.SerializeObject(weeklyBillByDates));
                //    var CompanyId = new SqlParameter("@CompanyId", "0");
                //    var BranchId = new SqlParameter("@BranchId", "0");
                //    var userId = new SqlParameter("@userId", "0");
                //    var Action = new SqlParameter("@Action", Actions.SpectDetailsList);

                //    var det = await _dbContext.tbl_WeeklyBillDetailsList.FromSqlRaw("Stpro_WeeklyBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                //    return Newtonsoft.Json.JsonConvert.SerializeObject(det, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });


                //}
                //catch (Exception)
                //{ throw; }
                //try
                //{
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WeeklyBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(weeklyBillByDates) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SpectDetailsList });
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
        public async Task<int> GetNextBillNoFrom(IEnumerable<WeeklyBillByDates> weeklyBillByDates)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_WeeklyBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(weeklyBillByDates) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SpectDetailsList });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                return Convert.ToInt32(dataTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return 0;
            }
        }

    }
}
