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
    public class LabourWagePaymentRepository : ILabourWagePaymentRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            SelectForApproval = 5,
            SelectForPayment = 6,
            SelectReportJson = 7,
            Selectforview = 8

        }
        public LabourWagePaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<LabourWagePayment> labourWagePayments)
        {

            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(labourWagePayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<LabourWagePayment> labourWagePayments)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(labourWagePayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
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
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<LabourWagePayment>> Get(int companyid, int Branchid)
        {
            try
            {
                // List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
                if (Branchid == 0)
                {
                    var list = await _dbContext.tbl_LabourWagePayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_LabourWagePaymentDetails.Where(x => x.LabourWagePaymentId == detail.Id).ToListAsync();
                    }
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_LabourWagePayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).Where(x => x.BranchId == Branchid).ToListAsync();

                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_LabourWagePaymentDetails.Where(x => x.LabourWagePaymentId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<LabourWagePayment>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_LabourWagePayment.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();
                var detaillist = await _dbContext.tbl_LabourWagePaymentDetails.Where(x => x.LabourWagePaymentId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LabourWageForPayment>> PendingBillsClear(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto)
        {
            try
            {
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", "");
                var sitemanagerId = new SqlParameter("@sitemanagerId", SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", "0");
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", Dateto);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_LabourForPaymentPendingBillsClear @CompanyId,@BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyId, branchId, employeeId, employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LabourWageForPayment>> GetForPayment(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto)
        {
            try
            {
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", "");
                var sitemanagerId = new SqlParameter("@sitemanagerId", SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", SitemanagerId);
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", Dateto);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_LabourForPayment @CompanyId,@BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyId, branchId, employeeId, employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LabourWageForPayment>> PendingBillsClearList(LabourForPayment labourForPayment)
        {
            try
            {
                var companyId = new SqlParameter("@CompanyId", labourForPayment.CompanyId);
                var branchId = new SqlParameter("@BranchId", labourForPayment.BranchId);
                var employeeId = new SqlParameter("@employeeId", labourForPayment.EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", labourForPayment.EmployeeIdList);
                var sitemanagerId = new SqlParameter("@sitemanagerId", labourForPayment.SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", labourForPayment.EmployeeLabourGroupId);
                var financialyearId = new SqlParameter("@financialyearId", labourForPayment.FinancialYearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", labourForPayment.DateTo);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_LabourForPaymentPendingBillsClear @CompanyId,@BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyId, branchId, employeeId, employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LabourWageForPayment>> GetForPaymentList(LabourForPayment labourForPayment)
        {
            try
            {
                var companyId = new SqlParameter("@CompanyId", labourForPayment.CompanyId);
                var branchId = new SqlParameter("@BranchId", labourForPayment.BranchId);
                var employeeId = new SqlParameter("@employeeId", labourForPayment.EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", labourForPayment.EmployeeIdList);
                var sitemanagerId = new SqlParameter("@sitemanagerId", labourForPayment.SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", labourForPayment.EmployeeLabourGroupId);
                var financialyearId = new SqlParameter("@financialyearId", labourForPayment.FinancialYearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", labourForPayment.DateTo);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_LabourForPayment @CompanyId,@BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyId, branchId, employeeId, employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<LabourWagePaymentList>> GetforEdit(int companyId, int branchId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LabourWagePaymentList.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LabourWagePaymentList>> GetforEdituser(int companyId, int branchId, int UserId, int IsBulk, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", IsBulk);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LabourWagePaymentList.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourWagePaymentList>> Getforapproval(int companyId, int branchId, int Userid, int IsBulk, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", IsBulk);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchId);
                var userId = new SqlParameter("@userId", Userid);
                var Action = new SqlParameter("@Action", Actions.SelectForApproval);
                var _product = await _dbContext.tbl_LabourWagePaymentList.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourWagePaymentList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_LabourWagePaymentList.FromSqlRaw("Stpro_LabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //public async Task<string> GetDetailsbyid(int Id)
        //{
        //    try { 
        //    var data =await (from a in _dbContext.tbl_LabourWagePaymentDetails 

        //                join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
        //                join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
        //                from d in ds.DefaultIfEmpty()
        //                join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
        //                from e in es.DefaultIfEmpty()
        //                join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
        //                from f in fs.DefaultIfEmpty()
        //                select new
        //                {
        //                    LabourWagePaymentDetailsId = a.LabourWagePaymentDetailsId,
        //                    labourWagePaymentId = a.LabourWagePaymentId,
        //                    dateFrom = a.DateFrom,
        //                    dateTo = a.DateTo,

        //                    projectName = c.ProjectName,
        //                    projectId = a.ProjectId,
        //                    blockId = a.BlockId,
        //                    blockName = d == null ? " " : d.BlockName,
        //                    floorId = a.FloorId,
        //                    floorName = e == null ? " " : e.FloorName,
        //                    unitId = a.UnitId,
        //                    unitName = f == null ? " " : f.UnitId,
        //                    daysWorked = a.DaysWorked,
        //                    overTimeHrs = a.OverTimeHrs,
        //                    dailyWageAmount = a.DailyWageAmount,
        //                    overTimeAmount = a.OverTimeAmount,
        //                    totalWage = a.TotalWage,
        //                    othercharges = a.Othercharges,
        //                    previousBalance = a.PreviousBalance,
        //                    payingAmount = a.PayingAmount,
        //                    advanceAmount = a.AdvanceAmount,
        //                    balanceAmount = a.BalanceAmount
        //                }).Where(x => x.labourWagePaymentId == Id).ToListAsync();

        //    string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
        //    return jsonString;
        //    }
        //    catch (Exception)
        //    { throw; }
        //}
        public async Task<string> GetDetailsbyid(int Id, int LabourGroupId)
        {
            try
            {
                try
                {
                    DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                    cmd.CommandText = "dbo.Stpro_LabourWagePaymentDetailById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                    cmd.Parameters.Add(new SqlParameter("@LabourGroupId", SqlDbType.Int) { Value = LabourGroupId });
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

                cmd.CommandText = "dbo.Stpro_LabourWagePayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
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

        public async Task<IEnumerable<Validation>> ValidationCheck(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime Dateto, int UserId, int EmployeeLabourGroupId)
        {
            try
            {
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var dateto = new SqlParameter("@dateto", Dateto);
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var userId = new SqlParameter("@UserId", UserId);
                var action = new SqlParameter("@action", 1);
                var employeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", EmployeeLabourGroupId);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("stpro_CheckAttendanceLabourPayment @CompanyId,@BranchId,@employeeId,@financialyearId,@dateto,@UserId, @EmployeeLabourGroupId, @action", companyId, branchId, employeeId, financialyearId, dateto, userId, employeeLabourGroupId, action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> ValidationChecknew(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime Dateto, int UserId, int EmployeeLabourGroupId)
        {
            try
            {
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var dateto = new SqlParameter("@dateto", Dateto);
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var userId = new SqlParameter("@UserId", UserId);
                var action = new SqlParameter("@action", 2);
                var employeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", EmployeeLabourGroupId);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("stpro_CheckAttendanceLabourPayment @CompanyId,@BranchId,@employeeId,@financialyearId,@dateto,@UserId, @EmployeeLabourGroupId, @action", companyId, branchId, employeeId, financialyearId, dateto, userId, employeeLabourGroupId, action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ListLabour>> GetLabours(int CompanyId, int BranchId, DateTime date)
        {
            try
            {
                var companyId = new SqlParameter("@CompanyId", CompanyId);
                var branchId = new SqlParameter("@BranchId", BranchId);
                var Date = new SqlParameter("@Date", date);
                var entity = await _dbContext.tbl_ListEmployee.FromSqlRaw("stpro_GetLabours  @CompanyId, @BranchId, @Date",
                  companyId, branchId, Date
                ).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
