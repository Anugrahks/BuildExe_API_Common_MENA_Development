using BuildExeHR.DBContexts;
using BuildExeHR.Library;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeHR.Repository
{
    public class EmployeeMasterRepository : IEmployeeMasterRepository
    {
        private readonly HRContext _dbContext;
        public EmployeeMasterRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public enum Actions
        {
            selectvalidation = 1,
            selectDeletevalidation = 2,
            SelectForAttendance = 6,
            SelectbyCategory = 7,
            SelectbyCategoryandlaborgroup = 8,
            PostValidation = 9,
            SelectForSalaryPayment = 10,
            SelectForCheckIn = 11
        }

        public async Task Delete(int employeID, int userid)
        {
            try
            {
                var employee = await _dbContext.tbl_EmployeeMaster.FindAsync(employeID);

                if (employee != null)
                {
                    //_dbContext.tbl_EmployeeMaster.Remove(employee);
                    employee.Status = "DELETED";
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetByID(int employeeID)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_EmployeeMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = employeeID });
                cmd.Parameters.Add(new SqlParameter("@item", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 6 });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();

                Dictionary<string, object> row = null;

                if (await reader.ReadAsync())   // Read only ONE row
                {
                    row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (FOR JSON PATH cases)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue;
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }
                }

                // If no data found return empty JSON
                if (row == null)
                    return "{}";

                return JsonConvert.SerializeObject(row, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> StatusRejoining(int companyid, int branchid)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_EmployeeMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@item", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 7 });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // Try to parse JSON columns (FOR JSON PATH cases)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue;
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        

        public async Task<IEnumerable<EmployeeMaster>> Get(int companyid, int branchid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.Id).ToListAsync();
                else
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeMaster>> Getuser(int companyid, int branchid, int Userid)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.Id).ToListAsync();
                else
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<EmployeeMaster>> Getuser(int companyid, int branchid, int Userid, int Financialyearid)
        //{
        //    try
        //    {
        //        var Id = new SqlParameter("@Id", Financialyearid);
        //        var item = new SqlParameter("@item", "");
        //        var CompanyId = new SqlParameter("@CompanyId", companyid);
        //        var BranchId = new SqlParameter("@BranchId", branchid);
        //        var UserId = new SqlParameter("@UserId", "0");
        //        var Action = new SqlParameter("@Action", 5);

        //        var _product = await _dbContext.tbl_EmployeeMaster.FromSqlRaw("Stpro_EmployeeMaster @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();

        //        foreach (var detail in _product)
        //        {
        //            detail.Password = Encription.DecryptString(detail.Password);
        //        }
        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}


        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }



        public async Task<string> Getuser(int companyid, int branchid, int Userid, int Financialyearid)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_EmployeeMaster";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Financialyearid });
                cmd.Parameters.Add(new SqlParameter("@item", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = Userid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                        // ✅ DECRYPT PASSWORD COLUMN
                        if (columnName.Equals("Password", StringComparison.OrdinalIgnoreCase) && value != null)
                        {
                            value = Encription.DecryptString(value.ToString());
                        }

                        // Try to parse JSON columns (FOR JSON PATH cases)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue;
                            }
                        }
                        else
                        {
                            row[columnName] = value;
                        }
                    }

                    result.Add(row);
                }

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<EmployeeMaster>> GetByStatus(int companyid, int branchid, string status)
        {
            try
            {
                if (branchid == 0)
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).OrderByDescending(x => x.Id).Where(x => x.Status == status).ToListAsync();
                else
                    return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).Where(x => x.Status == status).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeMaster>> GetByStatusandCategory(int companyid, int branchid, int category, string status)
        {
            try
            {

                return await _dbContext.tbl_EmployeeMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId == branchid).Where(x => x.EmployeeCategoryId == category).Where(x => x.Status == status).OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> getvalidation(EmployeeMaster employeeMaster)
        {
            try
            {

                var Id = new SqlParameter("@Id", "0");
                var CompanyId = new SqlParameter("@CompanyId", employeeMaster.CompanyId);
                var BranchId = new SqlParameter("@BranchId", employeeMaster.BranchId);
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(employeeMaster));
                var Action = new SqlParameter("@Action", Actions.selectvalidation);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmployeeValidation @Id,@CompanyId,@BranchId,@item, @Action", Id, CompanyId, BranchId, item, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Deletevalidation(int id)
        {
            try
            {

                var Id = new SqlParameter("@Id", id);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var item = new SqlParameter("@item", "");
                var Action = new SqlParameter("@Action", Actions.selectDeletevalidation);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmployeeValidation @Id,@CompanyId,@BranchId,@item, @Action", Id, CompanyId, BranchId, item, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Insert(EmployeeMaster employeeMaster)
        {
            try
            {
                if (employeeMaster.Password != null) {
                    employeeMaster.Password = Encription.EncryptString(employeeMaster.Password);
                }

                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(employeeMaster));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 1);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmployeeMaster @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(EmployeeMaster employeeMaster)
        {
            try
            {
                if (employeeMaster.Password != null)
                {
                    employeeMaster.Password = Encription.EncryptString(employeeMaster.Password);
                }
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(employeeMaster));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 2);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_EmployeeMaster @Id,@item,@CompanyId,@BranchId,@UserId,@Action", Id, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetbyCategory(int Companyid, int Branchid, int EmployeeCategory)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");

                var Action = new SqlParameter("@Action", Actions.SelectbyCategory);
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetbyCategoryandLabourgroup(int Companyid, int Branchid, int EmployeeCategory, int labourGroup)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", labourGroup);
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectbyCategoryandlaborgroup);
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetForAttendance(int ProjectId, int UnitId, int BlockId, int FloorId, int EmployeeCategory)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", ProjectId);
                var unitId = new SqlParameter("@unitId", UnitId);
                var blockId = new SqlParameter("@blockId", BlockId);
                var floorId = new SqlParameter("@floorId", FloorId);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");
                var Action = new SqlParameter("@Action", Actions.SelectForAttendance);
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public int GenerateNextEmpNo(int CompanyId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_GenerateNextEmpNo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                int empNo = (Int32)cmd.ExecuteScalar();
                return empNo;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public int ValidationForPunching(int EmployeeId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PunchingValidation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = EmployeeId });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                int empNo = (Int32)cmd.ExecuteScalar();
                return empNo;
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckEmployeeEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex) { throw; }

        }

        public async Task<string> EmployeeWithProject(int projectId, int categoryId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmployeeWithProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> EmployeeforProject(int employeeId, int categoryId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmployeeWithProject";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = employeeId });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string purchasedetails = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    purchasedetails = purchasedetails + dataTable.Rows[i][0].ToString();
                }
                return purchasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<EmployeeDetail> GetEmployeeById(int EmployeeId)
        {
            try
            {
                var employeeId = new SqlParameter("@EmployeeId", EmployeeId);

                var employee = await _dbContext.tbl_EmployeeDetail.FromSqlRaw("stpro_GetEmployee @EmployeeId ", employeeId)
                    .ToListAsync();


                return employee.FirstOrDefault();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> PostValidation(EmployeeMaster employeeMaster)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_EmployeeMasterValidation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = employeeMaster.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = employeeMaster.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeMaster.EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Decimal) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.PostValidation });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string validation = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    validation = validation + dataTable.Rows[i][0].ToString();
                }

                return validation;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<string> BirthDayReminder(int BranchId , int UserId , DateTime Date)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_BirthdayReminder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string validation = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    validation = validation + dataTable.Rows[i][0].ToString();
                }

                return validation;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetForSalaryPymt(int Companyid, int Branchid, int EmployeeCategory)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");

                var Action = new SqlParameter("@Action", Actions.SelectForSalaryPayment);
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Employee>> GetForCheckIn(int Companyid, int Branchid, int EmployeeCategory)
        {
            try
            {
                var projectId = new SqlParameter("@projectId", "0");
                var unitId = new SqlParameter("@unitId", "0");
                var blockId = new SqlParameter("@blockId", "0");
                var floorId = new SqlParameter("@floorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var CategoryId = new SqlParameter("@categoryId", EmployeeCategory);
                var DepartmentId = new SqlParameter("@DepartmentId", "0");
                var DesignationId = new SqlParameter("@DesignationId", "0");
                var DateWorked = new SqlParameter("@DateWorked", "2020-01-01");

                var Action = new SqlParameter("@Action", Actions.SelectForCheckIn);
                var _product = await _dbContext.tbl_Employee.FromSqlRaw("Stpro_EmployeeForAttendance @projectId,@unitId,@blockId,@floorId,@CompanyId,@BranchId,@CategoryId,@DepartmentId,@DesignationId,@DateWorked, @Action", projectId, unitId, blockId, floorId, CompanyId, BranchId, CategoryId, DepartmentId, DesignationId, DateWorked, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
