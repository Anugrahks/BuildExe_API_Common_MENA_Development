using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using BuildExeBasic.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeBasic.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using static iText.Svg.SvgConstants;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;




namespace BuildExeBasic.Repository
{
    public class GeneralAlertRepository : IGeneralAlertRepository
    {
        private readonly BasicContext _dbContext;
        public GeneralAlertRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectById = 5,
            Selectdoc = 6



        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<GeneralAlert> generalAlert)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(generalAlert));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var ItemList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GeneralAlerts @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return ItemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }



        public async Task<IEnumerable<Validation>> Update(IEnumerable<GeneralAlert> generalAlert)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(generalAlert));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var ItemList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GeneralAlerts @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return ItemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Validation>> Delete(int Id, int CompanyId, int BranchId)
        {
            try
            {
                var ID = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@item", "");
                var Companyid = new SqlParameter("@CompanyId", CompanyId);
                var Branchid = new SqlParameter("@BranchId", BranchId);
                var UserID = new SqlParameter("@UserId", "");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var itemList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_GeneralAlerts @Id,@item, @CompanyId, @BranchId,@UserId, @Action", ID, item, Companyid, Branchid, UserID, Action).ToListAsync();
                return itemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<GeneralAlert>> Get(int CompanyId, int BranchId, int UserId)
        {
            try
            {
                var ID = new SqlParameter("@Id", "");
                var item = new SqlParameter("@item", "");
                var CompanyID = new SqlParameter("@CompanyId", CompanyId);
                var BranchID = new SqlParameter("@BranchId", BranchId);
                var Userid = new SqlParameter("@UserId", UserId);
                var Action = new SqlParameter("@Action", Actions.Select);
                var ItemList = await _dbContext.tbl_GeneralAlerts.FromSqlRaw("Stpro_GeneralAlerts  @Id,@item,@CompanyId,@BranchId,@UserID,@Action", ID, item, CompanyID, BranchID, Userid, Action).ToListAsync();
                return ItemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<GeneralAlert>> GetById(int Id)
        {
            try
            {
                var ID = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "");
                var Action = new SqlParameter("@Action", Actions.SelectById);
                var ItemList = await _dbContext.tbl_GeneralAlerts.FromSqlRaw("Stpro_GeneralAlerts  @Id,@item,@CompanyId,@BranchId,@UserId,@Action", ID, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return ItemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Generalalertdoc>> Getdoc(int Id)
        {
            try
            {
                var ID = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectdoc);
                var ItemList = await _dbContext.tbl_Generalalertdoc.FromSqlRaw("Stpro_GeneralAlerts @Id,@item,@CompanyId,@BranchId,@UserId,@Action", ID, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return ItemList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}







