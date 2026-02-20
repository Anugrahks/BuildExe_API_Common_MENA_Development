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
    public class ForemanWorkOrderRepository : IForemanWorkOrderRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Selectforedit = 4,
            Selectforapprove = 5,
            SelectReportJson=6,
            Selectforview = 7,
            Selectforapproved = 8
        }

        public ForemanWorkOrderRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ForemanWorkOrder> foremanWorkOrders)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(foremanWorkOrders));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
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
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ForemanWorkOrder> foremanWorkOrders)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(foremanWorkOrders));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<ForemanWorkOrder>> Get(int Companyid, int Branchid)
        {
            try
            {
               // List<ForemanWorkOrder> nestedclass = new List<ForemanWorkOrder>();

            if (Branchid != 0)
            {
                var list =await _dbContext.tbl_ForemanWorkOrderMaster.Where(x => x.CompanyId == Companyid).Where(x => x.BranchId == Branchid).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist =await _dbContext.tbl_ForemanWorkOrderDetails.Where(x => x.ForemanWorkOrderId == detail.Id).ToListAsync();

                }

                return list;
            }
            else
            {
                var list =await _dbContext.tbl_ForemanWorkOrderMaster.Where(x => x.CompanyId == Companyid).ToListAsync ();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_ForemanWorkOrderDetails.Where(x => x.ForemanWorkOrderId == detail.Id).ToListAsync();

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
        public async Task<IEnumerable<ForemanWorkOrder>> GetbyID(int Idworkorder)
        {
            try
            {
                // List<ForemanWorkOrder> nestedclass = new List<ForemanWorkOrder>();
                var list = await _dbContext.tbl_ForemanWorkOrderMaster.Where(x => x.Id == Idworkorder).ToListAsync();
            var detaillist = await _dbContext.tbl_ForemanWorkOrderDetails.Where(x => x.ForemanWorkOrderId == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task< IEnumerable<ForemanWorkOrder>> Get(int ProjectId, int UnitId, int BlockId, int FloorId, int foremanID, int DivisionId)
        {
            try
            {
                var list =await _dbContext.tbl_ForemanWorkOrderMaster.Where(x => x.ProjectId == ProjectId).Where(x => x.UnitId  == UnitId ).Where(x => x.BlockId  == BlockId ).Where(x => x.FloorId  == FloorId ).Where(x => x.ForemanId  == foremanID ).Where(x => x.DivisionId == DivisionId).Where(x => x.ApprovalStatus  == 1).Where(x => x.WorkStatus == 1).OrderByDescending(i=>i.Id).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetWorkorder(int Companyid, int BranchID)
        {
            try
            {
                var data =await (from a in _dbContext.tbl_ForemanWorkOrderMaster 
                        join b in _dbContext.tbl_EmployeeMaster on a.ForemanId  equals b.Id
                        join w in _dbContext.tbl_WorkType on a.WorkTypeId equals w.Id
                        join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                        join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                        from d in ds.DefaultIfEmpty()
                        join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                        from e in es.DefaultIfEmpty()
                        join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                        from f in fs.DefaultIfEmpty()

                        select new
                        {
                            id = a.Id,
                            workName = a.WorkName,
                            dateOrdered = a.DateOrdered,
                            foremanId = a.ForemanId ,
                            fullName = b.FullName,
                            workTypeId = a.WorkTypeId,
                            workTypeName = w.WorkTypeName,

                            projectName = c.ProjectName,
                            projectId = a.ProjectId,
                            blockId = a.BlockId,
                            blockName = d == null ? " " : d.BlockName,
                            floorId = a.FloorId,
                            floorName = e == null ? " " : e.FloorName,
                            unitId = a.UnitId,
                            unitName = f == null ? " " : f.UnitId,
                            description = a.Description,
                            companyId = a.CompanyId,
                            branchId = a.BranchId,
                            workStatus = a.WorkStatus
                        }).Where(x => x.companyId == Companyid).Where(x => x.branchId == BranchID).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getdetsils(int Id)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_ForemanWorkOrderDetails 
                        join b in _dbContext.tbl_LabourWorkRate on a.LabourWorkId  equals b.Id
                        select new
                        {
                            foremanWorkOrderDetailsId = a.ForemanWorkOrderDetailsId,
                            foremanWorkOrderId = a.ForemanWorkOrderId,
                            
                            labourWorkId = a.LabourWorkId,
                            labourWorkName = b.LabourWorkName,
                            workRate = a.WorkRate,
                            oTRate = a.OTRate,


                        }).Where(x => x.foremanWorkOrderId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> GetdetsilsforBill(int Id)
        {
            try
            {
                var data =await (from a in _dbContext.tbl_ForemanWorkOrderDetails
                        join b in _dbContext.tbl_LabourWorkRate on a.LabourWorkId equals b.Id
                        select new
                        {
                            foremanWorkOrderDetailsId = a.ForemanWorkOrderDetailsId,
                            foremanWorkOrderId = a.ForemanWorkOrderId,

                            labourWorkId = a.LabourWorkId,
                            labourWorkName = b.LabourWorkName,
                            Wage = a.WorkRate,
                            oTRate = a.OTRate,


                        }).Where(x => x.foremanWorkOrderId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }


        public async Task<IEnumerable<ForemanWorkOrderList>> GetforEdit(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Selectforedit);
            var _product =await _dbContext.tbl_ForemanWorkOrderMasterList .FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ForemanWorkOrderList>> GetforEdituser(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.Selectforedit);
                var _product = await _dbContext.tbl_ForemanWorkOrderMasterList.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<ForemanWorkOrderList>> GetforApproveduser(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.Selectforapproved);
                var _product = await _dbContext.tbl_ForemanWorkOrderMasterList.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task <IEnumerable<ForemanWorkOrderList>> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", Userid);
            var Action = new SqlParameter("@Action", Actions.Selectforapprove );
            var _product =await _dbContext.tbl_ForemanWorkOrderMasterList.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ForemanWorkOrderList>> GetforView(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_ForemanWorkOrderMasterList.FromSqlRaw("Stpro_ForemanWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Getjson(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "dbo.Stpro_ForemanWorkOrder";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
            cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
            cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
            cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReportJson });
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader =await cmd.ExecuteReaderAsync();

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
    }
}
