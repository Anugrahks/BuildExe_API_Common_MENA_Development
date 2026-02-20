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
    public class SubContractorBillRepository:ISubContractorBillRepository 
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            Selectforapproval = 5,
            PreviousSubBill = 6,
            SelectReportJson=7,
            SelectLastBill = 8,
            Selectforview = 9
        }

        public SubContractorBillRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorBill> subContractorBills )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorBills));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

          var insertStatus = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return insertStatus;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> InsertRate(IEnumerable<SubContractorRateRevision> subContractorBills)
        {
            try
            {
                var WorkOrderId = new SqlParameter("@WorkOrderId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(subContractorBills));
                var SubcontractorId = new SqlParameter("@SubcontractorId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var insertStatus = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubcontractorRevisedRate @WorkOrderId,@json,@SubcontractorId,@Action",  WorkOrderId, json, SubcontractorId, Action).ToListAsync();
                return insertStatus;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int Idworkorder,int userId)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", userId);
            var Action = new SqlParameter("@Action", Actions.Delete);
           await _dbContext.Database.ExecuteSqlRawAsync("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorBill> subContractorBills)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorBills));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);
            var updateStatus = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            return updateStatus;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<SubContractorBill >> Get(int companyid,int branchid)
        {
            try
            { // List<SubContractorBill> nestedclass = new List<SubContractorBill>();
                if (branchid == 0)
            {
                var list =await _dbContext.tbl_SubContractorBillMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId  == companyid).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_SubContractorBillDetails.Where(x => x.SubContractorBillId == detail.Id).ToListAsync();
                }

                return list;
            }
            else
            {
                var list =await _dbContext.tbl_SubContractorBillMaster.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId == companyid).Where(x => x.BranchId  == branchid).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist =await _dbContext.tbl_SubContractorBillDetails.Where(x => x.SubContractorBillId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<SubContractorBill >> GetbyID(int Idworkorder)
        {
            try
            {
                // List<SubContractorBill> nestedclass = new List<SubContractorBill>();
                var list = await _dbContext.tbl_SubContractorBillMaster.Where(x => x.Id == Idworkorder).Where(x => x.IsDeleted == 0).ToListAsync();
            var detaillist =await _dbContext.tbl_SubContractorBillDetails.Where(x => x.SubContractorBillId  == Idworkorder).ToListAsync();
                var detaiaddllist = await _dbContext.tbl_SubContractorAdditionalBillDetails.Where(x => x.SubContractorBillId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorBillList >> GetforEdit(int companyid, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectforEdit);
            var _product =await _dbContext.tbl_SubContractorBillMasterList .FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorBillList>> GetforEdituser(int companyid, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_SubContractorBillMasterList.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<SubContractorBillList>> GetforApproval(int companyid, int branchid, int Userid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyid);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", Userid);
            var Action = new SqlParameter("@Action", Actions.Selectforapproval);
            var _product =await _dbContext.tbl_SubContractorBillMasterList.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorBillList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_SubContractorBillMasterList.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorBill>> GetLastBill(int workorderid)
        {
            try
            {
                var Id = new SqlParameter("@Id", workorderid);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectLastBill);
                var _product = await _dbContext.tbl_SubContractorBillMaster.FromSqlRaw("Stpro_SubContractorWorkBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




        public async Task<string> GetadditionalDetailsbyid(int Id)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_SubContractorAdditionalBillDetails

                                  join c in _dbContext.tbl_LabourWorkRate on a.WorkId equals c.Id
                                  select new
                                  {
                                      subContractorAdditionalBillDetailsId = a.SubContractorAdditionalBillDetailsId,
                                      subContractorBillId = a.SubContractorBillId,
                                      workId = a.WorkId,
                                      labourWorkName = c.LabourWorkName,
                                      quantity = a.Quantity,
                                      rate = a.Rate,
                                      gstPercentage = a.GstPercentage,
                                      igst = a.Igst,
                                      cgst = a.Cgst,
                                      sgst = a.Sgst

                                  }).Where(x => x.subContractorBillId == Id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
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
                var data =await (from a in _dbContext.tbl_SubContractorBillDetails 
                        join b in _dbContext.tbl_SubContractorWorkOrderDetails  on a.WorkOrderDetailsId equals b.SubContractorWorkOrderDetailsId
                        join c in _dbContext.tbl_LabourWorkRate  on b.WorkId  equals c.Id 
                        select new
                        {
                            subContractorBillDetailsId = a.SubContractorBillDetailsId,
                            subContractorBillId = a.SubContractorBillId,
                            workOrderDetailsId = a.WorkOrderDetailsId,
                            currentQuantity = a.CurrentQuantity,
                            negotiatedAmount=a.NegotiatedAmount,
                            workId =b.WorkId,
                            labourWorkName = c.LabourWorkName,
                            quantityOrdered=b.QuantityOrdered,
                            workRate = b.WorkRate,


                        }).Where(x => x.subContractorBillId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        
            public async Task<string> GetSubworkList(int WorkOrderId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = WorkOrderId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
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

        public async Task<string> GetPreviousBillQty(int BillId, int WorkOrderId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = BillId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = WorkOrderId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.PreviousSubBill });
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


        public async Task<string> AccountheadOther(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 12 });
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


        public async Task<string> OtherDeductionsGet(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 13 });
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

        public async Task<string> GetLabel(int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
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


        public async Task<string> GetOtherDeductionPer(int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
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


        public async Task<string> GetPreviousBalance(int WorkOrderId, int SubcontractorId )
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = WorkOrderId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = SubcontractorId });
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





        public async Task<string> Getjson(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd =_dbContext.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "dbo.Stpro_SubContractorWorkBill";
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

        public async Task<string> purchaseadjustment(int Id, int SubcontractorId, int ProjectId, int BlockId, int FloorId, int UnitId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PurchaseAdjustment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@SubcontractorId", SqlDbType.Int) { Value = SubcontractorId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
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


        public async Task<string> subcontractorworkorder(int ProjectId, int BlockId, int FloorId, int UnitId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubcontractorwithActiveWorkOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = FloorId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = UnitId });
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



        public async Task<string> raterevision(int WorkOrderId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubcontractorRevisedRate";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@WorkOrderId", SqlDbType.Int) { Value = WorkOrderId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@SubcontractorId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforEdit });
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


        public async Task<string> raterevision(int WorkOrderId, int WorkId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubcontractorRevisedRate";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@WorkOrderId", SqlDbType.Int) { Value = WorkOrderId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@SubcontractorId", SqlDbType.Int) { Value = WorkId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Selectforapproval });
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

        public async Task<DateTime> GetBillFromDate(int workorderid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GetBillFromDateByWorkOrderId";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@WorkOrderId", SqlDbType.Int) { Value = workorderid });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                DateTime fromDate = DateTime.MinValue;
                if (dataTable.Rows.Count > 0)
                {
                    fromDate = Convert.ToDateTime(dataTable.Rows[0][0].ToString());
                }

                return fromDate;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReportforRateRevision(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubcontractorRevisedRate";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@WorkOrderId", SqlDbType.Int) { Value =hRSearch.CompanyId}); 
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@SubcontractorId", SqlDbType.Int) { Value =hRSearch.BranchId});
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
                {
                    purcasedetails = "[]";
                }

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
