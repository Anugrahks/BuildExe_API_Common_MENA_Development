using BuildExeHR.Common;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;
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
namespace BuildExeHR.Repository
{
    public class GroupLabourWagePaymentRepository : IGroupLabourWagePaymentRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Selectforedit = 4,
            SelectforApproval = 5,
            SelectForPayment = 6,
            SelectReportJson = 7,
            Selectforview = 8,
            Label=9
        }
        public GroupLabourWagePaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<GroupLabourWagePayment> groupLabourWagePayments)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(groupLabourWagePayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<GroupLabourWagePayment> labourWagePayments)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(labourWagePayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<GroupLabourWagePayment>> Get(int companyid, int Branchid)
        {
            try
            {
                // List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
                if (Branchid == 0)
                {
                    var list = await _dbContext.tbl_GroupLabourWagePayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_GroupLabourWagePaymentDetails.Where(x => x.GroupLabourWagePaymentId == detail.Id).ToListAsync();
                    }

                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_GroupLabourWagePayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).Where(x => x.BranchId == Branchid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_GroupLabourWagePaymentDetails.Where(x => x.GroupLabourWagePaymentId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<GroupLabourWagePayment>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_GroupLabourWagePayment.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();
                var detaillist = await _dbContext.tbl_GroupLabourWagePaymentDetails.Where(x => x.GroupLabourWagePaymentId == Idworkorder).ToListAsync();
                return list;
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
                var companyid = new SqlParameter("@CompanyId", CompanyId);
                var branchid = new SqlParameter("@BranchId", BranchId);
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", "");
                var sitemanagerId = new SqlParameter("@sitemanagerId", SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", "0");
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", Dateto);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_GroupLabourForPayment @CompanyId, @BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyid, branchid, employeeId,employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
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
                var companyid = new SqlParameter("@CompanyId", labourForPayment.CompanyId);
                var branchid = new SqlParameter("@BranchId", labourForPayment.BranchId);
                var employeeId = new SqlParameter("@employeeId", labourForPayment.EmployeeId);
                var employeeIdList = new SqlParameter("@employeeIdList", labourForPayment.EmployeeIdList);
                var sitemanagerId = new SqlParameter("@sitemanagerId", labourForPayment.SitemanagerId);
                var EmployeeLabourGroupId = new SqlParameter("@EmployeeLabourGroupId", "0");
                var financialyearId = new SqlParameter("@financialyearId", labourForPayment.FinancialYearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", labourForPayment.DateTo);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_GroupLabourForPayment @CompanyId, @BranchId, @employeeId,@employeeIdList,@sitemanagerId,@EmployeeLabourGroupId,@financialyearId,@datefrom,@dateto,@Action", companyid, branchid, employeeId, employeeIdList, sitemanagerId, EmployeeLabourGroupId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
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
                var sitemanagerId = new SqlParameter("@sitemanagerId", SitemanagerId);
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var datefrom = new SqlParameter("@datefrom", "2020-01-01");
                var dateto = new SqlParameter("@dateto", Dateto);
                var Action = new SqlParameter("@Action", Actions.SelectForPayment);
                var _product = await _dbContext.tbl_LabourWageForPayment.FromSqlRaw("stpro_GroupLabourForPaymentPendingBillsClear @CompanyId,@BranchId, @employeeId,@sitemanagerId,@financialyearId,@datefrom,@dateto,@Action", companyId, branchId, employeeId, sitemanagerId, financialyearId, datefrom, dateto, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<GroupLabourWagePaymentList>> GetForEdit(int companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforedit);
                var _product = await _dbContext.tbl_GroupLabourWagePaymentlist.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<GroupLabourWagePaymentList>> GetForEdituser(int companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.Selectforedit);
                var _product = await _dbContext.tbl_GroupLabourWagePaymentlist.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<GroupLabourWagePaymentList>> GetForApproval(int companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforApproval);
                var _product = await _dbContext.tbl_GroupLabourWagePaymentlist.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<GroupLabourWagePaymentList>> GetForView(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_GroupLabourWagePaymentlist.FromSqlRaw("Stpro_GroupLabourWagePayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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
        //    try
        //    {
        //        var data = await (from a in _dbContext.tbl_GroupLabourWagePaymentDetails

        //                          join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
        //                          join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
        //                          from d in ds.DefaultIfEmpty()
        //                          join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
        //                          from e in es.DefaultIfEmpty()
        //                          join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
        //                          from f in fs.DefaultIfEmpty()
        //                          select new
        //                          {
        //                              groupLabourWagePaymentDetailsId = a.GroupLabourWagePaymentDetailsId,
        //                              groupLabourWagePaymentId = a.GroupLabourWagePaymentId,
        //                              dateFrom = a.DateFrom,
        //                              dateTo = a.DateTo,

        //                              projectName = c.ProjectName,
        //                              projectId = a.ProjectId,
        //                              blockId = a.BlockId,
        //                              blockName = d == null ? " " : d.BlockName,
        //                              floorId = a.FloorId,
        //                              floorName = e == null ? " " : e.FloorName,
        //                              unitId = a.UnitId,
        //                              unitName = f == null ? " " : f.UnitId,
        //                              daysWorked = a.DaysWorked,
        //                              overTimeHrs = a.OverTimeHrs,
        //                              wageAmount = a.WageAmount,
        //                              overTimeAmount = a.OverTimeAmount,
        //                              totalWage = a.TotalWage,
        //                              othercharges = a.Othercharges,
        //                              previousBalance = a.PreviousBalance,
        //                              payingAmount = a.PayingAmount,
        //                              advanceAmount = a.AdvanceAmount,
        //                              balanceAmount = a.BalanceAmount

        //                          }).Where(x => x.groupLabourWagePaymentId == Id).ToListAsync();

        //        string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
        //        return jsonString;
        //    }
        //    catch (Exception)
        //    { throw; }
        //}

        public async Task<string> GetDetailsbyid(int Id)
        {
            try
            {
                try
                {
                    DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                    cmd.CommandText = "dbo.Stpro_GroupLabourWagePaymentDetailById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
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
                catch (Exception)
                { throw; }
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

                cmd.CommandText = "dbo.Stpro_GroupLabourWagePayment";
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

        public async Task<IEnumerable<Validation>> ValidationCheck(int EmployeeId, int FinancialyearId, DateTime Dateto)
        {
            try
            {
                var employeeId = new SqlParameter("@employeeId", EmployeeId);
                var financialyearId = new SqlParameter("@financialyearId", FinancialyearId);
                var dateto = new SqlParameter("@dateto", Dateto);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("stpro_CheckAttendanceGroupLabourPayment @employeeId,@financialyearId,@dateto", employeeId, financialyearId, dateto).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetLabel(int CompanyId,int BranchId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GroupLabourWagePayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Label });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
