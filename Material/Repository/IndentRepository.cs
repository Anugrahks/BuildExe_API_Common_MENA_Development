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
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.Design;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class IndentRepository : IIndentRepository
    {
        private readonly MaterialContext _dbContext;
        public IndentRepository(MaterialContext dbContext)
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
            GetReport = 6,
            Selectindentdetails = 6,
            SelectReportJson = 7,
            Selectforview = 8,
            SelectindentWithoutSupplier =9,
            IndentById = 10,
            IndentByCompBranch = 11,
            IndentByProj = 12,
            IndentDetails = 10
        }

        public async Task<IEnumerable<Validation>> Delete(int Id, int UserID)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
                // var item = _dbContext.tbl_IndentMaster.Find(id);
                //
                //_dbContext.tbl_IndentMaster.Remove(item);
                //  Save();
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<Indent>> GetByID(int id)
        {
            try
            {
                //var purchaselist = await _dbContext.tbl_IndentMaster.Where(p => p.Id == id).Where(x => x.IsDeleted == 0).ToListAsync();
                //var purchasedetaillist = await _dbContext.tbl_IndentDetails.Where(x => x.IndentId == id).ToListAsync();
                //return purchaselist;

                var Id = new SqlParameter("@Id", id);
                var proj = new SqlParameter("@ProjectId", "0");
                var unit = new SqlParameter("@UnitId", "0");
                var block = new SqlParameter("@BlockId", "0");
                var floor = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.IndentById);

                var indent = await _dbContext.tbl_IndentMaster.FromSqlRaw("Stpro_IndentMaster @Id,@ProjectId,@UnitId, @BlockId,@FloorId, @CompanyId, @BranchId ,@Action" , Id, proj, unit, block, floor, CompanyId, BranchId, Action).ToListAsync();
                return indent;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<Indent>> Get(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var proj = new SqlParameter("@ProjectId", "0");
                var unit = new SqlParameter("@UnitId", "0");
                var block = new SqlParameter("@BlockId", "0");
                var floor = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Action = new SqlParameter("@Action", Actions.IndentByCompBranch);

                var indent = await _dbContext.tbl_IndentMaster.FromSqlRaw("Stpro_IndentMaster @Id,@ProjectId,@UnitId, @BlockId,@FloorId, @CompanyId, @BranchId ,@Action", Id, proj, unit, block, floor, CompanyId, BranchId, Action).ToListAsync();
                return indent;
            }
            catch (Exception)
            { throw; }
        }
        public async Task<IEnumerable<Indent>> Get(int projectId, int blockid, int floorid, int unitId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var proj = new SqlParameter("@ProjectId", projectId);
                var unit = new SqlParameter("@UnitId", unitId);
                var block = new SqlParameter("@BlockId", blockid);
                var floor = new SqlParameter("@FloorId", floorid);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.IndentByProj);

                var indent = await _dbContext.tbl_IndentMaster.FromSqlRaw("Stpro_IndentMaster @Id,@ProjectId,@UnitId, @BlockId,@FloorId, @CompanyId, @BranchId ,@Action", Id, proj, unit, block, floor, CompanyId, BranchId, Action).ToListAsync();
                return indent;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Indent> indent)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(indent));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();

            }
            catch (Exception)
            { throw; }
            //  _dbContext.Add(indent);
            //  Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Indent> indent)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(indent));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception)
            { throw; }
            // _dbContext.Entry(indent).State = EntityState.Modified;
            // Save();
        }
        public async Task<IEnumerable<IndentList>> GetforApproval(int companyId, int branchid, int UserID, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", UserID);
                var TypeId = new SqlParameter("@TypeId", 1);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }
        public async Task<IEnumerable<IndentList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", "0");
                var TypeId = new SqlParameter("@TypeId", 1);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<IndentList>> GetforEdit(int companyId, int branchid, int userId, int FinancialYearId, int IsAsset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var TypeId = new SqlParameter("@TypeId", 1);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }
        public async Task<IEnumerable<IndentList>> Getforview(MaterialSearch materialSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
                var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");
                var TypeId = new SqlParameter("@TypeId", 1);
                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<string> GetDetailsbyid(int IndentId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_IndentDetails
                                  join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  select new
                                  {
                                      indentDetailsId = a.IndentDetailsId,
                                      indentId = a.IndentId,
                                      materialId = a.MaterialId,
                                      //materialName = b.MaterialName,
                                      materialName = b == null ? String.Empty : b.MaterialName,
                                      unitId = b.UnitId,
                                      materialTypeId =  b.MaterialTypeId,
                                      //unitShortName = c.UnitShortName,
                                      unitShortName = c == null ? String.Empty : c.UnitShortName,
                                      workId = a.WorkId,
                                      quantityRequired = a.QuantityRequired,
                                      requiredDate = a.RequiredDate,
                                      quantityOrdered = a.QuantityOrdered,
                                      purchaseFlag = a.PurchaseFlag,
                                      remarks = a.Remarks,
                                      materialCategoryId = b.MaterialCategoryId,
                                      coefficientFactorValue = a.CoefficientFactorValue,
                                      conversionQuantity = a.ConversionQuantity,
                                      conversionUnitName = a.ConversionUnitName
                                  }).Where(x => x.indentId == IndentId).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<string> GetforReport(MaterialSearch materialSearch)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Indent";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
            catch (Exception)
            { throw; }
            //try
            //{
            //    var materialId = new SqlParameter("@materialId", "0");
            //    var item = new SqlParameter("@item", JsonConvert.SerializeObject(materialSearch));
            //    var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
            //    var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
            //    var UserId = new SqlParameter("@UserId", "0");
            //    var Action = new SqlParameter("@Action", Actions.GetReport);
            //    var indentList = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();

            //    foreach (var indent in indentList)
            //    {
            //        indent.IndentDetailItems = new List<IndentDetailItems>();

            //        var det = await (from a in _dbContext.tbl_IndentDetails
            //                         join b in _dbContext.tbl_MaterialMaster on a.MaterialId equals b.Id into bs
            //                         from b in bs.DefaultIfEmpty()
            //                         select new
            //                         {
            //                             indentDetailsId = a.IndentDetailsId,
            //                             indentId = a.IndentId,
            //                             materialName = b == null ? String.Empty : b.MaterialName,
            //                             quantityRequired = a.QuantityRequired,
            //                             requiredDate = a.RequiredDate
            //                         }).Where(x => x.indentId == indent.Id).ToListAsync();
            //        for (int i = 0; i < det.Count; i++)
            //        {
            //            IndentDetailItems items = new IndentDetailItems();
            //            items.MaterialName = det[i].materialName;
            //            items.QuantityRequired = det[i].quantityRequired;
            //            items.RequiredDate = det[i].requiredDate;

            //            indent.IndentDetailItems.Add(items);
            //        }
            //    }
            //    return indentList;
            //}
            //catch (Exception)
            //{ throw; }

        }

        public async Task<string> Getjson(MaterialSearch hRSearch)
        {
            try
            {


                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Indent";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 1 });
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
                return purcasedetails;
            }
            catch (Exception)
            { throw; }
        }


        public async Task<IEnumerable<IndentDetailsList>> GetDetailsForworkorder(int projectId, 
            int Unitid, int blockid, int floorid, int supplierid, int workCategoryId, int workNameId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId ", Unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId", floorid);
                var EmployeeId = new SqlParameter("@EmployeeId", supplierid);
                var TypeId = new SqlParameter("@TypeId", 1);
                var materialType = new SqlParameter("@materialType", "0");
                var workorderid = new SqlParameter("@workorderid", "0");
                var catId = new SqlParameter("@workCategoryId", workCategoryId);
                var nameId = new SqlParameter("@workNameId", workNameId);
                var Date = new SqlParameter("@date", DateTime.Now);
                var Action = new SqlParameter("@Action", Actions.Selectindentdetails);
                var _product = await _dbContext.tbl_IndentDetailsList.FromSqlRaw("Stpro_Indentdetails @ProjectId, " +
                    "@UnitId, @BlockId,@FloorId,@EmployeeId,@TypeId,@materialType,@workorderid, @workCategoryId, @workNameId,@date,@Action", 
                    ProjectId, UnitId, BlockId, FloorId, EmployeeId, TypeId, materialType, workorderid, catId, nameId, Date, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }


        public async Task<string> GetDetailsForitem(int projectId, int Unitid, int blockid, int floorid, int supplierid, int workCategoryId, int workNameId,DateTime date)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Indentdetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = Unitid });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = blockid });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorid });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = supplierid });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = 3 });
                cmd.Parameters.Add(new SqlParameter("@materialType", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@workorderid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@workCategoryId", SqlDbType.Int) { Value = workCategoryId });
                cmd.Parameters.Add(new SqlParameter("@workNameId", SqlDbType.Int) { Value = workNameId });
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime) { Value = date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.IndentByCompBranch });
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

        public async Task<IEnumerable<IndentDetailsList>> GetWithOutSupplier(int projectId,
            int Unitid, int blockid, int floorid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId ", Unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId", floorid);
                var TypeId = new SqlParameter("@TypeId", 1);
                var Action = new SqlParameter("@Action", Actions.SelectindentWithoutSupplier);
                var _product = await _dbContext.tbl_IndentDetailsList.FromSqlRaw("Stpro_IndentDetWithOutSupplier @ProjectId, @UnitId, @BlockId,@FloorId,@TypeId,@Action",ProjectId, UnitId, BlockId, FloorId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<IndentDetailsList>> GetDetailsForworkorder(int projectId, int Unitid, int blockid, int floorid, int supplierid, int MaterialType)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId ", Unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId", floorid);
                var EmployeeId = new SqlParameter("@EmployeeId", supplierid);
                var TypeId = new SqlParameter("@TypeId", 1);
                var materialType = new SqlParameter("@materialType", MaterialType);
                var workorderid = new SqlParameter("@workorderid", "0");
                var catId = new SqlParameter("@workCategoryId", "0");
                var nameId = new SqlParameter("@workNameId", "0");
                var Date = new SqlParameter("@date", DateTime.Now);
                var Action = new SqlParameter("@Action", Actions.Selectindentdetails);

                var _product = await _dbContext.tbl_IndentDetailsList.FromSqlRaw("Stpro_Indentdetails @ProjectId, " +
                    "@UnitId, @BlockId,@FloorId,@EmployeeId,@TypeId,@materialType ,@workorderid, @workCategoryId, @workNameId,@date,@Action",
                    ProjectId, UnitId, BlockId, FloorId, EmployeeId, TypeId, materialType, workorderid, catId, nameId, Date, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        //public async Task<string> PostIndentDetails(IndentPurchase indentPurchase)
        //{
        //    try
        //    {
        //        DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

        //        cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = "0" });
        //        cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
        //        cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.IndentDetails });
        //        if (cmd.Connection.State != ConnectionState.Open)
        //        {
        //            cmd.Connection.Open();
        //        }

        //        DbDataReader reader = await cmd.ExecuteReaderAsync();

        //        var dataTable = new DataTable();
        //        dataTable.Load(reader);
        //        string res = "";
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            res = res + dataTable.Rows[i][0].ToString();
        //        }
        //        if (res == "")
        //        {
        //            res = "[]";
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<string> PostIndentDetails(IndentPurchase indentPurchase)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SubContractorQuotation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MaterialType", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(indentPurchase) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 10 });

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
