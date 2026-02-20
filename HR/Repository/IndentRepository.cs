using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.DBContexts;
using BuildExeHR.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
namespace BuildExeHR.Repository
{
    public class IndentRepository:IIndentRepository 
    {
        private readonly HRContext _dbContext;
        public IndentRepository(HRContext dbContext)
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
            Selectindentdetails = 6,
                SelectReportJson=7,
            SelectforView = 8
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
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception)
            { throw; }
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
           return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception)
            { throw; }

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
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Indent @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception)
            { throw; }

        }
        public async Task<IEnumerable<IndentList>> GetforApproval(int companyId, int branchid, int UserID, int FinancialYearId)
        {
            try
            {
                int IsAsset = 0;
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var UserId = new SqlParameter("@UserId", UserID);
            var TypeId = new SqlParameter("@TypeId", 2);
            var Action = new SqlParameter("@Action", Actions.Selectforapproval);

            var _product =await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
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
            var TypeId = new SqlParameter("@TypeId", 2);
            var Action = new SqlParameter("@Action", Actions.SelectforEdit);

            var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
            return _product;
            }
            catch (Exception)
            { throw; }
        }


        public async Task<IEnumerable<IndentList>> GetforEdituser(int companyId, int branchid, int userId, int FinancialYearId)
        {
            try
            {
                int IsAsset = 0;
                var json = JsonConvert.SerializeObject(new { IsAsset });
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", json);
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var UserId = new SqlParameter("@UserId", userId);
                var TypeId = new SqlParameter("@TypeId", 2);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_IndentMasterList.FromSqlRaw("Stpro_IndentForApproval @Id,@item, @CompanyId, @BranchId,@UserId,@TypeId ,@Action", Id, item, CompanyId, BranchId, UserId, TypeId, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<IndentList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var UserId = new SqlParameter("@UserId", "0");
                var TypeId = new SqlParameter("@TypeId", 2);
                var Action = new SqlParameter("@Action", Actions.SelectforView);

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
                                  join b in _dbContext.tbl_LabourWorkRate on a.WorkId equals b.Id
                                  join c in _dbContext.tbl_Units on b.UnitId equals c.UnitId into cs
                                  from c in cs.DefaultIfEmpty()
                                  select new
                                  {
                                      indentDetailsId = a.IndentDetailsId,
                                      indentId = a.IndentId,
                                      workId = a.WorkId,
                                      labourWorkName = b.LabourWorkName,
                                      unitId = b.UnitId,
                                      //unitShortName = c.UnitShortName,
                                      unitShortName = c == null ? " " : c.UnitShortName,

                                      quantityRequired = a.QuantityRequired,
                                      requiredDate = a.RequiredDate,
                                      quantityOrdered = a.QuantityOrdered,
                                      purchaseFlag = a.PurchaseFlag,


                                  }).Where(x => x.indentId == IndentId).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception)
            { throw; }
        }
        public async Task< IEnumerable<IndentDetailsList >> GetDetailsForworkorder(int projectId, int Unitid, int blockid, int floorid, int subcontractoId,int Workorderid)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId ", Unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId", floorid);
                var EmployeeId = new SqlParameter("@EmployeeId", subcontractoId);
                var TypeId = new SqlParameter("@TypeId", 2);
                var materialType = new SqlParameter("@materialType", "0");
                var workorderid = new SqlParameter("@workorderid", Workorderid);

                var Action = new SqlParameter("@Action", Actions.Selectindentdetails);

                var _product = await _dbContext.tbl_IndentDetailsList.FromSqlRaw("Stpro_IndentdetailsList @ProjectId, @UnitId, @BlockId,@FloorId,@EmployeeId,@TypeId,@materialType,@workorderid ,@Action", ProjectId, UnitId, BlockId, FloorId, EmployeeId, TypeId, materialType, workorderid, Action).ToListAsync();
                return _product;
                // return null;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<IndentDetailsList>> GetDetails(int projectId, int Unitid, int blockid, 
            int floorid, int subcontractoId, int workCategoryId, int workNameId)
        {
            try
            {
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId ", Unitid);
                var BlockId = new SqlParameter("@BlockId", blockid);
                var FloorId = new SqlParameter("@FloorId", floorid);
                var EmployeeId = new SqlParameter("@EmployeeId", subcontractoId);
                var TypeId = new SqlParameter("@TypeId", 2);
                var materialType = new SqlParameter("@materialType", "0");
                var workorderid = new SqlParameter("@workorderid", "0");
                var catId = new SqlParameter("@workCategoryId", workCategoryId);
                var nameId = new SqlParameter("@workNameId", workNameId);
                var date = new SqlParameter("@date", DateTime.Now);
                var Action = new SqlParameter("@Action", Actions.Selectindentdetails);
                var _product = await _dbContext.tbl_IndentDetailsList.FromSqlRaw("Stpro_Indentdetails " +
                    "@ProjectId,@UnitId,@BlockId,@FloorId,@EmployeeId,@TypeId,@materialType,@workorderid, " +
                    "@workCategoryId, @workNameId,@date,@Action", ProjectId, UnitId, BlockId, FloorId, EmployeeId, TypeId, materialType, workorderid, catId, nameId, date, Action).ToListAsync();
                return _product;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<string> Getjson(HRSearch hRSearch )
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
            catch (Exception)
            { throw; }
        }

    }
}
