using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.DBContexts;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace BuildExeHR.Repository
{
    public class EmployeeDesignationRepository:IEmployeeDesignationRepository 
    {
        private readonly HRContext _dbContext;

        public EmployeeDesignationRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            selectvalidation = 1,
            selectDeletevalidation = 2,
            Insert =3,
            Update =4
        }
        public async Task DeleteDesignation(int designationId, int userid)
        {
            try
            {
                var designation = await _dbContext.tbl_EmployeeDesignation.FindAsync(designationId);

              

                if (designation != null)
                {
                    _dbContext.tbl_EmployeeDesignation.Remove(designation);
                    await _dbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<EmployeeDesignation> GetDesignationByID(int designationId)
        {
            try
            {
                return await _dbContext.tbl_EmployeeDesignation.FindAsync(designationId);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDesignation>> GetDesignation(int companyid, int BranchId, int Categoryid)
        {
            try
            {
                if (Categoryid == 0)
            {
                if (BranchId == 0)
                    return await _dbContext.tbl_EmployeeDesignation.Where(x => x.CompanyId == companyid).OrderByDescending(i=>i.Id).ToListAsync ();
                else
                    return await _dbContext.tbl_EmployeeDesignation.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == BranchId).OrderByDescending(i => i.Id).ToListAsync();
            }
            else
            {
                if (BranchId == 0)
                    return await _dbContext.tbl_EmployeeDesignation.Where(x => x.CompanyId == companyid).Where(x => x.EmployeeCategoryId == Categoryid).OrderByDescending(i => i.Id).ToListAsync();
                else
                    return await  _dbContext.tbl_EmployeeDesignation.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == BranchId).Where(x => x.EmployeeCategoryId == Categoryid).OrderByDescending(i => i.Id).ToListAsync();
            }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> Get(int companyid, int BranchId)
        {
            try
            {
                var data =await (from a in _dbContext.tbl_EmployeeDesignation
                        join c in _dbContext.tbl_EmployeeCategory on a.EmployeeCategoryId equals c.EmployeeCategoryId
                        select new
                        {
                            id = a.Id,
                            employeeDesignationName = a.EmployeeDesignationName,
                            description = a.Description,
                            departmentId = a.DepartmentId,
                            employeeCategoryId = a.EmployeeCategoryId,
                            employeeCategoryName = c.EmployeeCategoryName,
                            companyId = a.CompanyId,
                            branchId = a.BranchId,
                            marketing = a.marketing,
                            userId = a.UserId

                        }).Where(x => x.companyId == companyid).Where(x => x.branchId == BranchId).OrderByDescending(x => x.id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> Get(int companyid, int BranchId, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_EmployeeDesignation
                                  join c in _dbContext.tbl_EmployeeCategory on a.EmployeeCategoryId equals c.EmployeeCategoryId
                                  select new
                                  {
                                      id = a.Id,
                                      employeeDesignationName = a.EmployeeDesignationName,
                                      description = a.Description,
                                      departmentId = a.DepartmentId,
                                      employeeCategoryId = a.EmployeeCategoryId,
                                      employeeCategoryName = c.EmployeeCategoryName,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      marketing = a.marketing,
                                      userId = a.UserId

                                  }).Where(x => x.companyId == companyid).Where(x => x.branchId == BranchId).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> InsertDesignation(EmployeeDesignation designation)
        {
            try
            {


                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(designation));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var desig = await _dbContext.tbl_validation.FromSqlRaw("stPro_EmployeeDesignation @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return desig;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> UpdateDesignation(EmployeeDesignation designation)
        {
            try
            {

                var Id = new SqlParameter("@id", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(designation));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var desig = await _dbContext.tbl_validation.FromSqlRaw("stPro_EmployeeDesignation @id,@UserId,@json, @CompanyId, @BranchId, @Action", Id, UserId, json, CompanyId, BranchId, Action).ToListAsync();
                return desig;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> DeleteValidation(int id)
        {
            try
            {

                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@item", "");
                var Action = new SqlParameter("@Action", Actions.selectDeletevalidation);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmployeeDesignationValidation @Id,@CompanyId,@BranchId,@item, @Action", Id, CompanyId, BranchId, item, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckDesignationEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> GetWorkHours(int companyid, int branchid, int Categoryid)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stPro_EmployeeDesignation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = Categoryid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = ""});
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });


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
    }
}
