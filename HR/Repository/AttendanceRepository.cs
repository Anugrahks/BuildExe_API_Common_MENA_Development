using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace BuildExeHR.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectForedit = 4,
            SelectForApproval = 5,
            Attendancedetail = 6,
            SelectReportJson = 7,
            employeevalidation = 8,
            Totalvalidation = 9,
            selectattendance = 10,
            attendancevalidation = 11,
            biomatrixpunching = 12

        }

        public AttendanceRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<IEnumerable<Validation>> Insert(IEnumerable<Attendance> attendances)
        //{
        //    try
        //    {
        //        var materialId = new SqlParameter("@materialId", "0");
        //        var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
        //        var CompanyId = new SqlParameter("@CompanyId", "0");
        //        var BranchId = new SqlParameter("@BranchId", "0");
        //        var UserId = new SqlParameter("@UserId", "0");
        //        var Action = new SqlParameter("@Action", Actions.Insert);
        //        var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Attendance @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
        //        return purchaseList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}


        public async Task<IEnumerable<Validation>> Insert(IEnumerable<Attendance> attendances)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@Id", "0"),
            new SqlParameter("@json", JsonConvert.SerializeObject(attendances)),
            new SqlParameter("@CompanyId", "0"),
            new SqlParameter("@BranchId", "0"),
            new SqlParameter("@UserID", "0"),
            new SqlParameter("@Action", Actions.Insert)
        };

                var result = await _dbContext.tbl_validation
                    .FromSqlRaw("EXEC [dbo].[Stpro_AttendanceInsert] @Id, @json, @CompanyId, @BranchId, @UserID, @Action", parameters)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> punching(AttendancePunching attendance)
        {
            try
            {
                // ✅ Convert LogTime safely
                DateTime? logTime = null;
                if (!string.IsNullOrWhiteSpace(attendance.LogTime) && DateTime.TryParse(attendance.LogTime, out DateTime parsedLogTime))
                {
                    logTime = parsedLogTime;
                }

                // ✅ Convert DateWorked safely (use 1990-01-01 if invalid)
                DateTime dateWorked = new DateTime(1990, 1, 1);
                if (!string.IsNullOrWhiteSpace(attendance.DateWorked) && DateTime.TryParse(attendance.DateWorked, out DateTime parsedDateWorked))
                {
                    dateWorked = parsedDateWorked;
                }

                // ✅ Now create a new object with the correct types
                var processedAttendance = new
                {
                    attendance.Id,
                    attendance.BranchId,
                    attendance.EmployeeId,
                    attendance.ProjectId,
                    LogTime = logTime, // Converted to DateTime?
                    attendance.LogType,
                    DateWorked = dateWorked, // Converted to DateTime
                    attendance.Pictures,
                    attendance.Location,
                    attendance.WorkCategoryId,
                    attendance.WorkNameId,
                    attendance.Latitude,
                    attendance.Longitude,
                    attendance.Remarks,
                    attendance.TAAmount
                };

                // ✅ Serialize correctly before sending to SQL
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(processedAttendance));
                var materialId = new SqlParameter("@materialId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation
                    .FromSqlRaw("Stpro_AttendancePunching @materialId, @item, @CompanyId, @BranchId, @UserId, @Action",
                        materialId, item, CompanyId, BranchId, UserId, Action)
                    .ToListAsync();

                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(nameof(punching), "punching", ex);
                throw;
            }
        }

        public async Task<string> TADetails(int EmployeeId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendancePunching";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
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


        public async Task<string> TAByMonth(int EmployeeId, int MonthId, int YearId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendancePunching";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = MonthId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = YearId });
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = 0 });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> BiomatrixBulkEntry(IEnumerable<AttendancePunching> PunchingList)
        {
            try
            {

                var jsonObject = new SqlParameter("@item", JsonConvert.SerializeObject(PunchingList));
                var output = await _dbContext.tbl_validation.FromSqlRaw("Stpro_BiomatrixPunching @item", jsonObject).ToListAsync();
                return output;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }





        public async Task<string> NewBiomatrix(AttendancePunching Punching)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_BiomatrixNew";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@item", SqlDbType.NVarChar)
                {
                    Value = JsonConvert.SerializeObject(new
                    {
                        Punching.EmployeeId,
                        Punching.ProjectId,
                        Punching.LogTime,
                        Punching.LogType,
                        Punching.DateWorked,
                        Punching.BranchId
                    })
                });

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                await cmd.ExecuteNonQueryAsync(); // 🔑 no reader

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ValidationAttendance>> attendanceValidation(AttendanceDetail attendanceDetails)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendanceDetails));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", Actions.attendancevalidation);
                var purchaseList = await _dbContext.tbl_validationAttendance.FromSqlRaw("Stpro_Attendance @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task Delete(int Id, int userId)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);
                await _dbContext.Database.ExecuteSqlRawAsync("Stpro_Attendance @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<Validation>> Update(IEnumerable<Attendance> attendances)
        //{
        //    try
        //    {
        //        var materialId = new SqlParameter("@materialId", "0");
        //        var item = new SqlParameter("@item", JsonConvert.SerializeObject(attendances));
        //        var CompanyId = new SqlParameter("@CompanyId", "0");
        //        var BranchId = new SqlParameter("@BranchId", "0");
        //        var UserId = new SqlParameter("@UserId", "0");
        //        var Action = new SqlParameter("@Action", Actions.Update);
        //        var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Attendance @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
        //        return purchaseList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Attendance> attendances)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@Id", "0"),
            new SqlParameter("@json", JsonConvert.SerializeObject(attendances)),
            new SqlParameter("@CompanyId", "0"),
            new SqlParameter("@BranchId", "0"),
            new SqlParameter("@UserID", "0"),
            new SqlParameter("@Action", Actions.Update)
        };

                var result = await _dbContext.tbl_validation
                    .FromSqlRaw("EXEC [dbo].[Stpro_AttendanceInsert] @Id, @json, @CompanyId, @BranchId, @UserID, @Action", parameters)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Attendance>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_AttendanceMaster.Where(x => x.IsReject == 0).Where(x => x.IsDeleted == 0).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_AttendanceDetail.Where(x => x.AttendanceId == detail.Id).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetForEdit(int companyid, int branchid, int Menuid)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Menuid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForedit });
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
            //try
            //{
            //    var materialId = new SqlParameter("@Id", Menuid);
            //var item = new SqlParameter("@item", "");
            //var CompanyId = new SqlParameter("@CompanyId", companyid);
            //var BranchId = new SqlParameter("@BranchId", branchid);
            //var UserId = new SqlParameter("@UserId", "0");
            //var Action = new SqlParameter("@Action", Actions.SelectForedit);

            //var purchaseList =await _dbContext.tbl_AttendanceMasterList.FromSqlRaw("Stpro_Attendance @materialId,@item, @CompanyId, @BranchId,@UserId, @Action", materialId, item, CompanyId, BranchId, UserId, Action).ToListAsync();
            //return purchaseList;
            //}
            //catch (Exception)
            //{ throw; }
        }

        public async Task<string> GetForEdituser(int companyid, int branchid, int Menuid, int UserId, int FinancialYearId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Menuid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForedit });
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

        public async Task<string> GetForApprovals(int companyid, int branchid, int userID, int Menuid, int FinancialYearId)
        {

            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Menuid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userID });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectForApproval });
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

        public async Task<string> GetAttendanceDetail(AttendanceList attendanceList)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AttendanceDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = attendanceList.Id });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.Int) { Value = attendanceList.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = attendanceList.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = attendanceList.UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = attendanceList.BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = attendanceList.FloorId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = attendanceList.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = attendanceList.BranchId });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = attendanceList.EmployeeCategoryId });
                cmd.Parameters.Add(new SqlParameter("@LabourGroupId", SqlDbType.Int) { Value = attendanceList.EmployeeLabourGroupId });
                cmd.Parameters.Add(new SqlParameter("@LabourHeadId", SqlDbType.Int) { Value = attendanceList.LabourHead });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = attendanceList.DateWorked });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Attendancedetail });
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


        public async Task<string> GetPayRollAttendanceDetail(AttendanceList attendanceList)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PayRollAttendanceDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = attendanceList.Id });
                cmd.Parameters.Add(new SqlParameter("@projectId", SqlDbType.Int) { Value = attendanceList.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = attendanceList.DivisionId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = attendanceList.UnitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = attendanceList.BlockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = attendanceList.FloorId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = attendanceList.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = attendanceList.BranchId });
                cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = attendanceList.EmployeeCategoryId });
                cmd.Parameters.Add(new SqlParameter("@LabourGroupId", SqlDbType.Int) { Value = attendanceList.EmployeeLabourGroupId });
                cmd.Parameters.Add(new SqlParameter("@LabourHeadId", SqlDbType.Int) { Value = attendanceList.LabourHead });
                cmd.Parameters.Add(new SqlParameter("@DateWorked", SqlDbType.DateTime) { Value = attendanceList.DateWorked });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Attendancedetail });
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
        public async Task<IEnumerable<Validation>> Getemployeevalidation(int employeeid, DateTime dateworked)
        {
            try
            {
                var Id = new SqlParameter("@Id", employeeid);
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var CategoryId = new SqlParameter("@CategoryId", "0");
                var LabourGroupId = new SqlParameter("@LabourGroupId", "0");
                var LabourHeadId = new SqlParameter("@LabourHeadId", "0");
                var attendaceDate = new SqlParameter("@attendaceDate", dateworked);
                var Action = new SqlParameter("@Action", Actions.employeevalidation);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendanceDetail @Id,@ProjectId,@DivisionId, @UnitId, @BlockId,@FloorId,@CompanyId,@BranchId,@CategoryId,@LabourGroupId,@LabourHeadId,@attendaceDate, @Action", Id, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, CategoryId, LabourGroupId, LabourHeadId, attendaceDate, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Getemployeevalidation(int employeecategory, int labourGroup, int labourhead, DateTime dateworked)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var CategoryId = new SqlParameter("@CategoryId", employeecategory);
                var LabourGroupId = new SqlParameter("@LabourGroupId", labourGroup);
                var LabourHeadId = new SqlParameter("@LabourHeadId", labourhead);
                var attendaceDate = new SqlParameter("@attendaceDate", dateworked);
                var Action = new SqlParameter("@Action", Actions.Totalvalidation);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_AttendanceDetail @Id,@ProjectId,@DivisionId, @UnitId, @BlockId,@FloorId,@CompanyId,@BranchId,@CategoryId,@LabourGroupId,@LabourHeadId,@attendaceDate, @Action", Id, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, CategoryId, LabourGroupId, LabourHeadId, attendaceDate, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> Getjson(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance";
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
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> DateWiseReport(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_DatewiseAttendanceReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = hRSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@Todate", SqlDbType.DateTime) { Value = hRSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = hRSearch.UserId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });

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


        public async Task<string> WeekWiseAttendanceReport(HRSearch hRSearch)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_WeekWiseAttendanceReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = hRSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = hRSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = hRSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });

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

                        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                        if (value is string stringValue && IsLikelyJson(stringValue))
                        {
                            try
                            {
                                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                            }
                            catch
                            {
                                row[columnName] = stringValue; // fallback if invalid JSON
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
                    ContractResolver = new DefaultContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }

        public async Task<string> Get_attendance(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Attendance";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.selectattendance });
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



        public async Task BulkAttendanceEntry(List<Attendance> attendances)
        {
            try
            {
                foreach (var attendance in attendances)
                {
                    await _dbContext.tbl_AttendanceMaster.AddAsync(attendance);
                    if (attendance.AttendanceDetail != null && attendance.AttendanceDetail.Any())
                    {
                        foreach (var attendanceDetail in attendance.AttendanceDetail)
                        {
                            attendanceDetail.AttendanceId = attendance.Id;
                            await _dbContext.tbl_AttendanceDetail.AddAsync(attendanceDetail);
                        }
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public async Task Edit(List<Attendance> attendances)
        {
            try
            {
                foreach (var attendance in attendances)
                {
                    var existingAttendance = await _dbContext.tbl_AttendanceMaster.Include(x => x.AttendanceDetail)
                        .FirstOrDefaultAsync(a => a.Id == attendance.Id);

                    if (existingAttendance != null)
                    {
                        existingAttendance.EmployeeCategoryId = attendance.EmployeeCategoryId;
                        existingAttendance.EmployeeLabourGroupId = attendance.EmployeeLabourGroupId;
                        existingAttendance.LabourHead = attendance.LabourHead;
                        existingAttendance.CompanyId = attendance.CompanyId;
                        existingAttendance.BranchId = attendance.BranchId;
                        existingAttendance.ApprovalStatus = attendance.ApprovalStatus;
                        existingAttendance.ApprovalLevel = attendance.ApprovalLevel;
                        existingAttendance.ApprovedDate = attendance.ApprovedDate;
                        existingAttendance.ApprovalRemarks = attendance.ApprovalRemarks;
                        existingAttendance.IsReject = existingAttendance.IsReject;
                        existingAttendance.Remarks = attendance.Remarks;
                        existingAttendance.IsDeleted = attendance.IsDeleted;
                        existingAttendance.ApprovedBy = attendance.ApprovedBy;
                        existingAttendance.FinancialYearId = attendance.FinancialYearId;
                        existingAttendance.WorkOrderId = attendance.WorkOrderId;

                        if (attendance.AttendanceDetail != null && attendance.AttendanceDetail.Any())
                        {
                            foreach (var updatedDetail in attendance.AttendanceDetail)
                            {
                                var existingDetail = await _dbContext.tbl_AttendanceDetail
                                .FirstOrDefaultAsync(x => x.AttendanceDetailId == updatedDetail.AttendanceDetailId);

                                if (existingDetail != null)
                                {
                                    existingDetail.EmployeeId = updatedDetail.EmployeeId;
                                    existingDetail.ProjectId = updatedDetail.ProjectId;
                                    existingDetail.Amount = updatedDetail.Amount;
                                    existingDetail.Work = updatedDetail.Work;
                                    existingDetail.WorkNameId = updatedDetail.WorkNameId;
                                    existingDetail.UnitId = updatedDetail.UnitId;
                                    existingDetail.BlockId = updatedDetail.BlockId;
                                    existingDetail.FloorId = updatedDetail.FloorId;
                                    existingDetail.DateWorked = updatedDetail.DateWorked;
                                    existingDetail.LoginTime = updatedDetail.LoginTime;
                                    existingDetail.LogoutTime = updatedDetail.LogoutTime;
                                    existingDetail.OverTime = updatedDetail.OverTime;
                                    existingDetail.OverTimeAmount = updatedDetail.OverTimeAmount;
                                    existingDetail.Category = updatedDetail.Category;

                                }
                            }
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetAttendanceDetails(HRSearch AttedanceSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetAttendanceDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(AttedanceSearch) });
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
                if (details == "")
                    details = "[]";
                return details;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetDefaultDate(HRSearch details)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stpro_GetAttendanceDetail";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(details) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string nextdate = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    nextdate = nextdate + dataTable.Rows[i][0].ToString();
                }
                if (nextdate == "")
                    nextdate = "[]";
                return nextdate;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
