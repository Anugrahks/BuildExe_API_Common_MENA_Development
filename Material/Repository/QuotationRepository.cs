using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Insert_Rate = 4,
            Update_Rate = 5,
            Delete_Rate = 6,

            Selectforedit = 7,
            Selectforapproval = 8,
            Selectbyproject = 9,
            SelectforRateedit = 10,
            SelectMaxOrderNo = 11,
            approve = 12,
            selectallsuppliers = 13,
            selectallsupplierswithitem = 14,
            selectallComparisonStatemetn = 15,
            selectitemwithSupplier = 16,
            selectsupplierTot = 17,
            selectapproveddata = 18,
            SelectRateforapproval = 20,

        }

        public QuotationRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Quotation> quotations)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(quotations));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int UserID)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Quotation> Quotation)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(Quotation));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Quotation>> GetbyID(int Id)
        {
            try
            {
                var purchaselist = await _dbContext.tbl_QuotationMaster.Where(x => x.Id == Id).Where(x => x.IsDeleted == 0).ToListAsync();
                var purchasedetaillist = await _dbContext.tbl_QuotationDetails.Where(x => x.QuotationId == Id).ToListAsync();
                return purchaselist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert_QuotationRate(IEnumerable<QuotationRate> quotations)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(quotations));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert_Rate);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete_QuotationRate(int Id, int UserID)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete_Rate);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update_QuotationRate(IEnumerable<QuotationRate> Quotation)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(Quotation));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update_Rate);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetDetailsbyid(int Quotationid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_QuotationDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  join e in _dbContext.tbl_MaterialBrand on a.BrandId equals e.Id into es
                                  from e in es.DefaultIfEmpty()
                                  select new
                                  {
                                      quotationDetailId = a.QuotationDetailId,
                                      quotationId = a.QuotationId,
                                      materialId = a.MaterialId,
                                      IndentId = a.IndentId,
                                      materialID= b.MaterialID,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      unitId = b.UnitId,
                                      materialTypeId = b.MaterialTypeId,
                                      brandId = a.BrandId,
                                      materialBrandName = e == null ? String.Empty : e.MaterialBrandName,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      quantity = a.Quantity,


                                  }).Where(x => x.quotationId == Quotationid).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetSupplierDetails(int Quotationid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_QuotationSupplier
                                  join b in _dbContext.tbl_QuotationMaster on a.QuotationId equals b.Id
                                  join c in _dbContext.tbl_Suppliers on a.SupplierId equals c.Id into cs
                                  from c in cs.DefaultIfEmpty()

                                  select new
                                  {

                                      quotationId = a.QuotationId,
                                      supplierId = a.SupplierId,
                                      supplierName = c == null ? String.Empty : c.SupplierName,


                                  }).Where(x => x.quotationId == Quotationid).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetRateDetailsbyid(int Quotationid)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_QuotationRate
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  join e in _dbContext.tbl_MaterialBrand on a.BrandId equals e.Id into es
                                  from e in es.DefaultIfEmpty()
                                  join d in _dbContext.tbl_Suppliers on a.SupplierId equals d.Id into ds
                                  from d in ds.DefaultIfEmpty()

                                  select new
                                  {
                                      quotationRateId = a.QuotationRateId,
                                      quotationId = a.QuotationId,
                                      supplierId = a.SupplierId,
                                      materialId = a.MaterialId,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      brandId = a.BrandId,
                                      materialBrandName = e == null ? String.Empty : e.MaterialBrandName,
                                      unitId = b.UnitId,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      quantity = a.Quantity,
                                      supplierName = d == null ? String.Empty : d.SupplierName,
                                      rate = a.Rate,
                                      amount = a.Amount,
                                      preference = a.Preference

                                  }).Where(x => x.quotationId == Quotationid).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<QuotationList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforedit);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<QuotationList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Selectforedit);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<QuotationList>> Getforapproval(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<QuotationList>> Getfor_Edit_Rate(int companyId, int branchid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforRateedit);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<QuotationList>> Getfor_Edit_Rate(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.SelectforRateedit);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<QuotationList>> Getfor_CompasisonStatement(int companyId, int branchid)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.selectallComparisonStatemetn);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<QuotationList>> Getfor_CompasisonStatement(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.selectallComparisonStatemetn);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<QuotationList>> GetforApprovalCompasisonStatement(int companyId, int branchid, int userid, int FinancialYearId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", FinancialYearId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.SelectRateforapproval);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<QuotationList>> GetBy_Project(int ProjectID)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", ProjectID);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectbyproject);
                var purchaseList = await _dbContext.tbl_Quotationlist.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<int> GetMaxquotationo(int CompanyId, int Branchid)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectMaxOrderNo });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }

        public async Task<int> GetMaxquotationo(int CompanyId, int Branchid, int FinancialYearId)
        {

            int orderID = 0;

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = FinancialYearId });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 22 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                orderID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                return orderID;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                return orderID;
            }
        }


        public async Task<string> Getsuppliers(int PRojectid, int MaterialId, int BrandID)
        {



            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = PRojectid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = MaterialId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BrandID });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectallsuppliers });
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

        public async Task<string> GetsupplierswithItem(int QuotationID)
        {



            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = QuotationID });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectallsupplierswithitem });
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

        public async Task<string> GetItemwithSupplier(int QuotationID)
        {



            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = QuotationID });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectitemwithSupplier });
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
        public async Task<string> GetSupplierTotal(int QuotationID)
        {



            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = QuotationID });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = 0 });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectsupplierTot });
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

        public async Task<IEnumerable<Validation>> Approve_QuotationRate(IEnumerable<Quotation> Quotation)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(Quotation));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var SupplierId = new SqlParameter("@SupplierId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.approve);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Quotation @materialId,@item, @CompanyId, @BranchId,@SupplierId,@UserId, @Action", materialId, item, CompanyId, BranchId, SupplierId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetApprovedData(int CompanyId, int BranchId, int ProjectId, int MaterialId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Quotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = MaterialId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@supplierId", SqlDbType.Int) { Value = ProjectId });

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectapproveddata });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string itemdetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    itemdetails = itemdetails + dataTable.Rows[i][0].ToString();
                }

                return itemdetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetQuotationApprovedDetails(int projectId, int materialId, int brandId, int companyId, int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_QuotationApprovedDet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@MaterialId", SqlDbType.Int) { Value = materialId });
                cmd.Parameters.Add(new SqlParameter("@BrandId", SqlDbType.Int) { Value = brandId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string quotationDetails = string.Empty;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    quotationDetails = quotationDetails + dataTable.Rows[i][0].ToString();
                }

                if (string.IsNullOrEmpty(quotationDetails))
                {
                    quotationDetails = "[]";
                }

                return quotationDetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

    }
}
