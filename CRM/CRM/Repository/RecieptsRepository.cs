using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BuildExeServices.Repository;
using Newtonsoft.Json;

using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class RecieptsRepository : IRecieptsRepository
    {
        private readonly ProductContext _dbContext;
        public RecieptsRepository(ProductContext dbContext)
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
            GetOneDetail = 6,
            GetforView = 7
        }

        // type=1 --PARTBILL
        //type=2 --Additional bill
        //type=3 --Percentage Bill
        //type=4 --Stage bill
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Reciept> reciepts)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(reciepts));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Reciepts @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<Reciept> reciepts)
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(reciepts));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Reciepts @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
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

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Reciepts @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Reciept>> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_Reciepts.Where(x => x.Id == Idworkorder).ToListAsync();
                var detaillist = await _dbContext.tbl_RecieptDetails.Where(x => x.RecieptId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception)
            { throw; }
        }


        public async Task<IEnumerable<Reciept>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_Reciepts.ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await  _dbContext.tbl_RecieptDetails.Where(x => x.RecieptId == detail.Id).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Reciept>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
                {
                    var list =await _dbContext.tbl_Reciepts.Where(x => x.CompanyId == companid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_RecieptDetails.Where(x => x.RecieptId == detail.Id).ToListAsync();
                    }
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_Reciepts.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_RecieptDetails.Where(x => x.RecieptId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<RecieptsList>> Getforapproval(int companyId, int branchid, int UserID, int menuId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserID);
                var MenuId = new SqlParameter("@MenuId", menuId);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_RecieptsList.FromSqlRaw("Stpro_RecieptsForApproval @Id,@item,@CompanyId,@BranchId,@userId,@MenuId,@Action", Id, item, CompanyId, BranchId, userId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<RecieptsList>> GetforEdit(int companyId, int branchid, int menuId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var MenuId = new SqlParameter("@MenuId", menuId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_RecieptsList.FromSqlRaw("Stpro_RecieptsForApproval @Id,@item,@CompanyId,@BranchId,@userId,@MenuId,@Action", Id, item, CompanyId, BranchId, userId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<RecieptsList>> GetforEdituser(int companyId, int branchid, int menuId, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var MenuId = new SqlParameter("@MenuId", menuId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_RecieptsList.FromSqlRaw("Stpro_RecieptsForApproval @Id,@item,@CompanyId,@BranchId,@userId,@MenuId,@Action", Id, item, CompanyId, BranchId, userId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<RecieptsList>> GetforView(BillSearch billSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", billSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var MenuId = new SqlParameter("@MenuId", billSearch. MenuId);
                var Action = new SqlParameter("@Action", Actions.GetforView );

                var _product = await _dbContext.tbl_RecieptsList.FromSqlRaw("Stpro_RecieptsForApproval @Id,@item,@CompanyId,@BranchId,@userId,@MenuId,@Action", Id, item, CompanyId, BranchId, userId, MenuId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<RecieptDetail>> Getdetails(int Id)
        {
            try
            {
                var detaillist = await _dbContext.tbl_RecieptDetails.Where(x => x.RecieptId == Id).ToListAsync();
                return detaillist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> getRecieptDetails(int Id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_RecieptsForApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
             
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value =0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetOneDetail  });
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


    }
}
