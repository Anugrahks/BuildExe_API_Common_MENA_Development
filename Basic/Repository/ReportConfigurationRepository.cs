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
using Newtonsoft.Json;
using System.Data.Common;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class ReportConfigurationRepository:IReportConfigurationRepository 
    {
        private readonly BasicContext _dbContext;
        public ReportConfigurationRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforEdit = 4,
            
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ReportConfiguration> reportConfigurations)
        {
              try
            {
            var id = new SqlParameter("@id", "0");
            var MenuId = new SqlParameter("@menuId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(reportConfigurations));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);
            var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ReportConfiguration @id,@menuId, @item, @CompanyId, @BranchId, @UserId, @Action", id, MenuId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            return result;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task Update(IEnumerable<ReportConfiguration> reportConfigurations)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
            var MenuId = new SqlParameter("@menuId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(reportConfigurations));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", "0");
            var Action = new SqlParameter("@Action", Actions.Update );
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_ReportConfiguration @id,@menuId, @item, @CompanyId, @BranchId, @UserId, @Action", id, MenuId, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception)
            { throw; }
        }
        public async Task<IEnumerable<Validation>> Delete(int MenuID, int UserID)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
            var MenuId = new SqlParameter("@menuId", MenuID);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var UserId = new SqlParameter("@UserId", UserID);
            var Action = new SqlParameter("@Action", Actions.Delete);
            var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_ReportConfiguration @id,@menuId, @item, @CompanyId, @BranchId, @UserId, @Action", id, MenuId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            return result;
            }
            catch (Exception)
            { throw; }
        }

        public async Task<IEnumerable<ReportConfiguration>> GetByID(int MenuID, int companyId, int branchId)
        {
            try
            {
                var list = await  _dbContext.tbl_ReportConfiguration.Where(x => x.MenuId  == MenuID).Where(x => x.CompanyId  == companyId).Where(x => x.BranchId  == branchId).ToListAsync();
            foreach (var detail in list)
            {

                var fieldlist = await _dbContext.tbl_ReportConfigurationFieldDetails.Where(x => x.ReportConfigurationId  == detail.Id).ToListAsync();
                var filterlist = await _dbContext.tbl_ReportConfigurationFilterDetails.Where(x => x.ReportConfigurationId  == detail.Id).ToListAsync();
            }
            return list;
            }
            catch (Exception)
            { throw; }
          
        }
        public async Task<string> GetFilterByID(int id, int companyId, int branchId)
        {
            try
            {
                //var data = await (from a in _dbContext.tbl_ReportConfiguration
                //                  join b in _dbContext.tbl_ReportConfigurationFilterDetails on a.Id equals b.ReportConfigurationId
                //                  join c in _dbContext.tbl_ReportFilter on b.ReportFilterId equals c.Id
                //                  select new
                //                  {
                //                      id = a.Id,
                //                      //  configurationFilterDetailsId = b.ConfigurationFilterDetailsId,
                //                      // reportFilterId = b.ReportFilterId,

                //                      menuId = a.MenuId,
                //                      companyId = a.CompanyId,
                //                      branchId = a.BranchId,


                //                      filterFields = c.FilterFields,
                //                      filterType = c.FilterType,
                //                      searchField = c.SearchField



                //                  }).Where(x => x.id == id).Where(x => x.branchId == branchId).Where(x => x.companyId == companyId).ToListAsync();
                //string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                //return jsonString;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_ReportFieldAndFilter";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
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
            {
                throw;
            }
        }
        public async Task<string> GetFieldByID(int id, int companyId, int branchId)
        {
            try
            {
                //var data = await (from a in _dbContext.tbl_ReportConfiguration
                //                  join b in _dbContext.tbl_ReportConfigurationFieldDetails on a.Id equals b.ReportConfigurationId
                //                  join c in _dbContext.tbl_ReportFields on b.ReportFieldId equals c.Id
                //                  select new
                //                  {
                //                      id = a.Id,
                //                      menuId = a.MenuId,
                //                      companyId = a.CompanyId,
                //                      branchId = a.BranchId,
                //                      fieldName = c.FieldName,
                //                      displayName = c.DisplayName,
                //                      order = b.Order
                //                  }).Where(x => x.id == id).Where(x => x.branchId == branchId).Where(x => x.companyId == companyId).OrderBy(s=>s.order).ToListAsync();
                //string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                //return jsonString;
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_ReportFieldAndFilter";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
            {
                throw;
            }
        }
        public async Task<IEnumerable<ReportConfiguration>> Get()
        {
            try
            {
                var purchaselist = await _dbContext.tbl_ReportConfiguration.ToListAsync();
                return purchaselist;
            }
            catch (Exception )
            { throw; }
        }
        public async Task<IEnumerable<Validation>> InsertPrintable(IEnumerable<PrintableTemplate> template)
        {
            try
            {
                var id = new SqlParameter("@Id", "0");
                var MenuId = new SqlParameter("@MenuId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(template));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var result = await _dbContext.tbl_validation.FromSqlRaw("StPro_ReportPrintable @Id,@MenuId, @json, @CompanyId, @BranchId, @UserId, @Action", id, MenuId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return result;

            }
            catch (Exception)
            { throw; }
        }
        public async Task<string> GetPrintableTemplate(int id, int companyId, int branchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_ReportPrintable";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
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
            {
                throw;
            }
        }

        public async Task<string> GetList(int MenuId, int BranchID)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ReportConfiguration";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int) { Value = MenuId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchID });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
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
            {
                throw;
            }
        }

        public async Task<IEnumerable<ListReportName>> ListReportNames()
        {
            try
            {
                var reportNames = await _dbContext.tbl_Menu.ToListAsync();
                return reportNames;
            }
            catch(Exception) { throw; }
        }

    }
}
