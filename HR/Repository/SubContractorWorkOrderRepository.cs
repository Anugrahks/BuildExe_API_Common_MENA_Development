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
    public class SubContractorWorkOrderRepository:ISubContractorWorkOrderRepository 
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            SelectReportJson = 6,
            Selectforview = 7,
            Selectforapproved = 8
        }

        public SubContractorWorkOrderRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorWorkOrder> subContractorWorkOrders)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorWorkOrders));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Idworkorder,int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorWorkOrder> subContractorWorkOrders )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorWorkOrders));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorWorkOrder>> Get(int companyid, int Branchid)
        {
            try
            {// List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
                if (Branchid == 0)
            {
                var list =await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId  == companyid).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist =await _dbContext.tbl_SubContractorWorkOrderDetails.Where(x => x.SubContractorWorkOrderId == detail.Id).ToListAsync();
                }

                return list;
            }
            else
            {
                var list = await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).Where(x => x.BranchId  == Branchid ).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist =await _dbContext.tbl_SubContractorWorkOrderDetails.Where(x => x.SubContractorWorkOrderId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<SubContractorWorkOrder >> GetbyID(int Idworkorder)
        {
            try
            {
               // List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
            var list =await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();
            var detaillist = await _dbContext.tbl_SubContractorWorkOrderDetails.Where(x => x.SubContractorWorkOrderId  == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorWorkOrder>> Getbyproject(int ProjectId, int UnitId, int BlockId, int FloorId,int SubContractorId,int DivisionId)
        {
            try
            {
                var list =await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.ProjectId  == ProjectId).Where(x => x.UnitId  == UnitId ).Where(x => x.BlockId  == BlockId).Where(x => x.FloorId  == FloorId).Where(x => x.SubContractorId  == SubContractorId ).Where(x => x.DivisionId == DivisionId).Where(x => x.ApprovalStatus  == 1).Where(x => x.IsDeleted == 0).Where(x => x.WorkOrderStatus == 0).OrderByDescending(i=>i.Id).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorWorkOrder>> GetDetails(int ProjectId, int UnitId, int BlockId, 
            int FloorId, int SubContractorId, int workcategoryId, int workNameId, int DivisionId)
        {
            try
            {

                var projectId = new SqlParameter("@ProjectId", ProjectId);
                var unitId = new SqlParameter("@UnitId", UnitId);
                var blockId = new SqlParameter("@BlockId", BlockId);
                var floorId = new SqlParameter("@FloorId", FloorId);
                var subContractorId = new SqlParameter("@SubContractorId", SubContractorId);
                var workcategory = new SqlParameter("@WorkCategoryId", workcategoryId);
                var workName = new SqlParameter("@WorkNameId", workNameId);
                var divisionId = new SqlParameter("@DivisionId", DivisionId);
                var _product = await _dbContext.tbl_SubContractorWorkOrderMaster.FromSqlRaw("Stpro_SubContractorWorkOrderMasterDet " +
                    "@ProjectId, @UnitId, @BlockId, @FloorId, @SubContractorId,@WorkCategoryId,@WorkNameId,@DivisionId", projectId, unitId, blockId, floorId, subContractorId, workcategory, workName, divisionId).ToListAsync();
                return _product;
                //var list = await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.ProjectId == ProjectId).
                //    Where(x => x.UnitId == UnitId).Where(x => x.BlockId == BlockId).
                //    Where(x => x.FloorId == FloorId).Where(x => x.SubContractorId == SubContractorId).
                //    Where(x => x.ApprovalStatus == 1).Where(x => x.IsDeleted == 0).
                //    Where(x => x.WorkOrderStatus == 0).Where(x => x.WorkNameId == 0 || x.WorkNameId == workNameId || x.WorkNameId == null).
                //    Where(x => x.Category == 0 || x.Category == workcategoryId || x.Category == null)
                //    .OrderByDescending(i => i.Id).ToListAsync();
                //return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }


        public async Task<IEnumerable<SubContractorWorkOrder>> GetDetails(int ProjectId, int UnitId, int BlockId,
    int FloorId, int SubContractorId)
        {
            try
            {

                var projectId = new SqlParameter("@ProjectId", ProjectId);
                var unitId = new SqlParameter("@UnitId", UnitId);
                var blockId = new SqlParameter("@BlockId", BlockId);
                var floorId = new SqlParameter("@FloorId", FloorId);
                var subContractorId = new SqlParameter("@SubContractorId", SubContractorId);
                var _product = await _dbContext.tbl_SubContractorWorkOrderMaster.FromSqlRaw("Stpro_SubContractorRateRevisionMasterDet " +
                    "@ProjectId, @UnitId, @BlockId, @FloorId, @SubContractorId", projectId, unitId, blockId, floorId, subContractorId).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }


        public async Task<IEnumerable<SubContractorWorkOrder>> Getbyproject_Completed(int ProjectId, int UnitId, int BlockId, int FloorId, int SubContractorId)
        {
            try
            {
                var list = await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.ProjectId == ProjectId).Where(x => x.UnitId == UnitId).Where(x => x.BlockId == BlockId).Where(x => x.FloorId == FloorId).Where(x => x.SubContractorId == SubContractorId).Where(x => x.ApprovalStatus == 1).Where(x => x.IsDeleted == 0).Where(x => x.WorkOrderStatus == 1).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorWorkOrder>> GetbyattendanceType(int ProjectId, int UnitId, int BlockId, int FloorId, int SubContractorId,int AttendanceType)
        {
            try
            {
                var list = await _dbContext.tbl_SubContractorWorkOrderMaster.Where(x => x.ProjectId == ProjectId).Where(x => x.UnitId == UnitId).Where(x => x.BlockId == BlockId).Where(x => x.FloorId == FloorId).Where(x => x.SubContractorId == SubContractorId).Where(x => x.ApprovalStatus == 1).Where(x => x.IsDeleted == 0).Where(x => x.Attendancetype == AttendanceType).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorWorkOrderList>> GetforEdit(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product =await _dbContext.tbl_SubContractorWorkOrderMasterList.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<SubContractorWorkOrderList>> GetforEdituser(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_SubContractorWorkOrderMasterList.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<SubContractorWorkOrderList>> getapproved(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.Selectforapproved);
                var _product = await _dbContext.tbl_SubContractorWorkOrderMasterList.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        
        public async Task<IEnumerable<SubContractorWorkOrderList >> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", Userid);
            var Action = new SqlParameter("@Action", Actions.Selectforapproval);
            var _product =await _dbContext.tbl_SubContractorWorkOrderMasterList.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorWorkOrderList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_SubContractorWorkOrderMasterList.FromSqlRaw("Stpro_SubContractorWorkOrder @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDetailsbyid(int Id)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_SubContractorWorkOrderDetails
                        join b in _dbContext.tbl_LabourWorkRate on a.WorkId equals b.Id into bjoin
                        from b in bjoin.DefaultIfEmpty()
                        join c in _dbContext.tbl_Units on a.UnitId equals c.UnitId into cjoin
                        from c in cjoin.DefaultIfEmpty()
                        select new
                        {
                            subContractorWorkOrderDetailsId = a.SubContractorWorkOrderDetailsId,
                            subContractorWorkOrderId = a.SubContractorWorkOrderId,
                            indentId = a.IndentId,
                            workId = a.WorkId,
                            labourWorkName = b.LabourWorkName,
                            quantityOrdered = a.QuantityOrdered,
                            workRate = a.WorkRate,
                            unitId=a.UnitId,
                            unitShortName=c.UnitShortName


                        }).Where(x => x.subContractorWorkOrderId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
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

            cmd.CommandText = "dbo.Stpro_SubContractorWorkOrder";
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
    }
}
