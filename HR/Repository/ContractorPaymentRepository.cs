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
    public class ContractorPaymentRepository : IContractorPaymentRepository
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
            Selectforview = 7
        }
        public ContractorPaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ContractorPayment> contractorPayments)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(contractorPayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ContractorPayment> contractorPayments)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(contractorPayments));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ContractorPayment>> Get(int companyid, int Branchid)
        {
            try
            {
                // List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
                if (Branchid == 0)
                {
                    var list = await _dbContext.tbl_ContractorPayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_ContractorPaymentDetails.Where(x => x.ContractorPaymentId == detail.Id).ToListAsync();
                    }

                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_ContractorPayment.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).Where(x => x.BranchId == Branchid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_ContractorPaymentDetails.Where(x => x.ContractorPaymentId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<ContractorPayment>> GetbyID(int Idworkorder)
        {
            try
            {
                // List<SubContractorWorkOrder> nestedclass = new List<SubContractorWorkOrder>();
                var list = await _dbContext.tbl_ContractorPayment.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();
                var detaillist = await _dbContext.tbl_ContractorPaymentDetails.Where(x => x.ContractorPaymentId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ContractorPaymentList>> GetforEdit(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_ContractorPaymentList.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ContractorPaymentList>> GetforEdituser(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_ContractorPaymentList.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ContractorPaymentList>> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", Userid);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
                var _product = await _dbContext.tbl_ContractorPaymentList.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ContractorPaymentList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_ContractorPaymentList.FromSqlRaw("Stpro_ContractorPayment @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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
                var data = await (from a in _dbContext.tbl_ContractorPaymentDetails
                                  join b in _dbContext.tbl_ContractorWorkOrderMaster on a.BillId equals b.Id
                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  join g in _dbContext.tbl_Division on b.DivisionId equals g.DivisionId
                                  join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                                  from e in es.DefaultIfEmpty()
                                  join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                                  from f in fs.DefaultIfEmpty()
                                  select new
                                  {
                                      contractorPaymentDetailsId = a.ContractorPaymentDetailsId,
                                      contractorPaymentId = a.ContractorPaymentId,
                                      billId = a.BillId,
                                      dateOrdered = b.DateOrdered,
                                      workOrderNo = b.WorkOrderNo,
                                      billAmount = b.BillAmount,
                                      projectName = c.ProjectName,
                                      DivisionShortname=g.DivisionShortName,
                                      projectId = a.ProjectId,
                                      DivisionId=b.DivisionId,
                                      blockId = a.BlockId,
                                      blockName = d == null ? " " : d.BlockName,
                                      floorId = a.FloorId,
                                      floorName = e == null ? " " : e.FloorName,
                                      unitId = a.UnitId,
                                      unitName = f == null ? " " : f.UnitId,
                                      paymentAmount = a.PaymentAmount,
                                      discountAmount = a.DiscountAmount,
                                      advanceAmount = a.AdvanceAmount

                                  }).Where(x => x.contractorPaymentId == Id).ToListAsync();

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

                cmd.CommandText = "dbo.Stpro_ContractorPayment";
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
    }
}
