using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class SupplierPaymentRepository : ISupplierPaymentRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            SelectforApproval = 5,
            SelectforReport = 6,
            DebitEdit = 7,
            DebitNoteReport = 8

        }

        public SupplierPaymentRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<SupplierPayment> supplierPayment)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(supplierPayment));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int id, int userId)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<SupplierPayment> supplierPayment)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(supplierPayment));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierPayment>> Get(int CompanyId, int Branchid)
        {
            try
            {
                if (Branchid == 0)
                {
                    var purchaselist = await _dbContext.tbl_SupplierPaymentMaster.Where(p => p.CompanyId == CompanyId).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_SupplierPaymentDetails.Where(x => x.SupplierPaymentId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
                else
                {
                    var purchaselist = await _dbContext.tbl_SupplierPaymentMaster.Where(p => p.CompanyId == CompanyId).Where(p => p.BranchId == Branchid).ToListAsync();
                    foreach (var purdetail in purchaselist)
                    {
                        var purchasedetaillist = await _dbContext.tbl_SupplierPaymentDetails.Where(x => x.SupplierPaymentId == purdetail.Id).ToListAsync();
                    }
                    return purchaselist;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<SupplierPayment>> GetbyID(int Id)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_SupplierPaymentMaster.Where(x => x.Id == Id).ToListAsync();
                var purchasedetaillist = await _dbContext.tbl_SupplierPaymentDetails.Where(x => x.SupplierPaymentId == Id).ToListAsync();
                return purchaselist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<SupplierPaymentList>> GetForEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_SupplierPaymentMasterList.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierPaymentList>> GetForEdituser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_SupplierPaymentMasterList.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierPaymentList>> GetForApproval(int companyId, int branchid, int userId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.SelectforApproval);
                var _product = await _dbContext.tbl_SupplierPaymentMasterList.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
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
                var data = await (from a in _dbContext.tbl_SupplierPaymentDetails

                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                                  from e in es.DefaultIfEmpty()
                                  join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                                  from f in fs.DefaultIfEmpty()
                                  select new
                                  {
                                      supplierPaymentDetailsId = a.SupplierPaymentDetailsId,
                                      supplierPaymentId = a.SupplierPaymentId,
                                      purchaseId = a.PurchaseId,
                                      paymentAmount = a.PaymentAmount,
                                      discountAmount = a.DiscountAmount,
                                      advanceAmount = a.AdvanceAmount,
                                      isOpening = a.IsOpening,

                                      projectName = c.ProjectName,
                                      projectId = a.ProjectId,
                                      blockId = a.BlockId,
                                      blockName = d == null ? " " : d.BlockName,
                                      floorId = a.FloorId,
                                      floorName = e == null ? " " : e.FloorName,
                                      unitId = a.UnitId,
                                      unitName = f == null ? " " : f.UnitId


                                  }).Where(x => x.supplierPaymentId == Id).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetforReport(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SupplierPayment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforReport });
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
            //var Id = new SqlParameter("@Id", "0");
            //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
            //var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId );
            //var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId );
            //var UserId = new SqlParameter("@UserId", "0");
            //var Action = new SqlParameter("@Action", Actions.GetReport);
            //var _product = _dbContext.tbl_SupplierPaymentMasterList.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action);
            //return _product;
        }

        public async Task<IEnumerable<DebitNoteList>> getDebit(int SupplierId, int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@Id", "0");
                var companyId = new SqlParameter("@CompanyId", "0");
                var branchId = new SqlParameter("@BranchId", "0");
                var supplierId = new SqlParameter("@SupplierId", SupplierId);
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.Update);
                var _product = await _dbContext.tbl_DebitNoteList.FromSqlRaw("stpro_DebitNote @Id, @CompanyId, @BranchId, @SupplierId, @FinancialYearId, @json,@Action", id, companyId, branchId, supplierId, financialYearId, json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Save(IEnumerable<DebitNote> debitNote)
        {
            try

            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(debitNote));
                var Action = new SqlParameter("@Action", Actions.Insert);
                var result = await _dbContext.tbl_validation.FromSqlRaw("stpro_DebitNote @Id, @CompanyId, @BranchId, @SupplierId, @FinancialYearId, @json, @Action", Id, CompanyId, BranchId, SupplierId, FinancialYearId, json, Action).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<DebitNoteList>> getDebitEdit(int SupplierId, int FinancialYearId, int SupplierPaymentId)
        {
            try
            {
                var id = new SqlParameter("@Id", SupplierPaymentId);
                var companyId = new SqlParameter("@CompanyId", "0");
                var branchId = new SqlParameter("@BranchId", "0");
                var supplierId = new SqlParameter("@SupplierId", SupplierId);
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var json = new SqlParameter("@json", "");
                var Action = new SqlParameter("@Action", Actions.DebitEdit);
                var _product = await _dbContext.tbl_DebitNoteList.FromSqlRaw("stpro_DebitNote @Id, @CompanyId, @BranchId, @SupplierId, @FinancialYearId, @json, @Action", id, companyId, branchId, supplierId, financialYearId, json, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }

        public async Task<string> GetforDebitNoteReport(MaterialSearch materialSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_DebitNoteReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.DebitNoteReport });
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
            //var Id = new SqlParameter("@Id", "0");
            //var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
            //var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId );
            //var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId );
            //var UserId = new SqlParameter("@UserId", "0");
            //var Action = new SqlParameter("@Action", Actions.GetReport);
            //var _product = _dbContext.tbl_SupplierPaymentMasterList.FromSqlRaw("Stpro_SupplierPayment @Id,@item, @CompanyId, @BranchId,@UserId, @Action", Id, item, CompanyId, BranchId, UserId, Action);
            //return _product;
        }

    }
}
