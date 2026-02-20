using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.Common;
using System.Reflection;
using System.ComponentModel.Design;

namespace BuildExeServices.Repository
{
    public class TendorNegotiationRepository : ITendorNegotiationRepository
    {
        private readonly ProductContext _dbContext;

        public TendorNegotiationRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task Insert(TendorNegotiation tendorNegotiation)
        {
            try
            {
                await _dbContext.AddAsync(tendorNegotiation);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Update(TendorNegotiation tendorNegotiation)
        {
            try
            {
                _dbContext.Entry(tendorNegotiation).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int departmentId)
        {
            try
            {
                var department = await _dbContext.tbl_TendorNegotiation.FindAsync(departmentId);

                _dbContext.tbl_TendorNegotiation.Remove(department);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<TendorNegotiation>> Get()
        {
            try
            {

                return await _dbContext.tbl_TendorNegotiation.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<TendorNegotiation>> GetByID(int projectid)
        {
            try
            {
                //    return  await _dbContext.tbl_TendorNegotiation.Where(x => x.ProjectId == projectid).ToListAsync();
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", projectid);

                var item = new SqlParameter("@item", "");

                var Action = new SqlParameter("@Action", "5");

                var purchaseList = await _dbContext.tbl_TendorNegotiation.FromSqlRaw("stpro_ProjectSpecificationNegotiation @Id,@projectId,@item, @Action", Id, ProjectId, item, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Insert(IEnumerable<SpecificationNegotiation> specificationNegotiation)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@projectId", "0");

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationNegotiation));

                var Action = new SqlParameter("@Action", "1");

                await _dbContext.Database.ExecuteSqlRawAsync("stpro_ProjectSpecificationNegotiation @Id,@projectId,@item,@Action", Id, ProjectId, item, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetprojectSpec_Negotiated(int ProjectID)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_ProjectSpecificationNegotiation";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@projectid", SqlDbType.Int) { Value = ProjectID });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
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
