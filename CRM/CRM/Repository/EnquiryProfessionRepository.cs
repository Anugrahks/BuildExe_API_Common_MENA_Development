using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.Design;
using System.Reflection;
using System.Xml.Linq;
using System.IO;


namespace BuildExeServices.Repository
{
    public class EnquiryProfessionRepository : IEnquiryProfessionRepository
    {
        private readonly ProductContext _dbContext;
        public enum Actions
        {
            PostEnquiryProfessional = 1,
            PutEnquiryProfessional = 2,
            GetDeleteEnquiryProfessional = 3,
            GetEnquiryProfessional = 4,
            GetByIdEnquiryProfessional = 5

        }
        public EnquiryProfessionRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<string> GetEnquiryProfessional(int BranchId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EnquiryProfession";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetEnquiryProfessional });
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

        public async Task<string> GetByIdEnquiryProfessional(int Id)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EnquiryProfession";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetByIdEnquiryProfessional });
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

        public async Task<IEnumerable<Validation>> PostEnquiryProfessional(IEnumerable<EnquiryProfession> enquiryProfession)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var TypeId = new SqlParameter("@TypeId", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(enquiryProfession));
                var Action = new SqlParameter("@Action", Actions.PostEnquiryProfessional);

                var finishedgoodsList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EnquiryProfession @Id,@CompanyId,@BranchId,@FinancialYearId,@UserId,@TypeId,@ProjectId,@json,@Action", Id, CompanyId, BranchId, FinancialYearId, UserId, TypeId, ProjectId, item, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> PutEnquiryProfessional(IEnumerable<EnquiryProfession> enquiryProfession)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var TypeId = new SqlParameter("@TypeId", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(enquiryProfession));
                var Action = new SqlParameter("@Action", Actions.PutEnquiryProfessional);

                var finishedgoodsList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EnquiryProfession @Id,@CompanyId,@BranchId,@FinancialYearId,@UserId,@TypeId,@ProjectId,@json,@Action", Id, CompanyId, BranchId, FinancialYearId, UserId, TypeId, ProjectId, item, Action).ToListAsync();
                return finishedgoodsList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDeleteEnquiryProfessional(int Id)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EnquiryProfession";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetDeleteEnquiryProfessional });
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
