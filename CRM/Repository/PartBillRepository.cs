using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using BuildExeServices.Repository;
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

namespace BuildExeServices.Repository
{
    public class PartBillRepository : IPartBillRepository
    {
        private readonly ProductContext _dbContext;
        public PartBillRepository(ProductContext dbContext)
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
            SelectdaetailsBasedonProject = 6,
            SelectforReport = 7,
            lastBill = 8,
            Selectforview = 9,
            gethistory = 10
        }

        public async Task<IEnumerable<Validation>> Insert(PartBillMaster partBillMaster)
        {
            var parameters = new[]
            {
        new SqlParameter("@Id", SqlDbType.Int) { Value = 0 },

        new SqlParameter("@json", SqlDbType.NVarChar, -1)
        {
            Value = JsonConvert.SerializeObject(partBillMaster)
        },

        new SqlParameter("@CompanyId", SqlDbType.Int)
        {
            Value = partBillMaster.CompanyId
        },

        new SqlParameter("@BranchId", SqlDbType.Int)
        {
            Value = partBillMaster.BranchId
        },

        new SqlParameter("@Userid", SqlDbType.Int)
        {
            Value = partBillMaster.UserId
        },

        new SqlParameter("@Action", SqlDbType.Int)
        {
            Value = 1
        }
    };

            //return await _dbContext.tbl_validation
            //    .FromSqlRaw("EXEC Stpro_PartBill @Id,@json,@CompanyId,@BranchId,@Userid,@Action", parameters)
            //    .ToListAsync();
            return await _dbContext.tbl_validation
    .FromSqlRaw(
        "EXEC Stpro_PartBill @Id = @Id, @json = @json, @CompanyId = @CompanyId, @BranchId = @BranchId, @Userid = @Userid, @Action = @Action",
        parameters)
    .ToListAsync();
        }
        //    public async Task<IEnumerable<Validation>> Insert(PartBillMaster partBillMaster)
        //    {
        //        var Id = new SqlParameter("@Id", "0");
        //        //var item = new SqlParameter("@item", JsonConvert.SerializeObject(partBillMaster));
        //        var item = new SqlParameter("@json", SqlDbType.NVarChar, -1)
        //        {
        //            Value = JsonConvert.SerializeObject(partBillMaster)
        //        };
        //        var CompanyId = new SqlParameter("@CompanyId", "0");
        //        var BranchId = new SqlParameter("@BranchId", "0");
        //        var userId = new SqlParameter("@userId", "0");
        //        var Action = new SqlParameter("@Action", Actions.Insert);

        //        //return await _dbContext.tbl_validation
        //        //    .FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action",
        //        //    Id, item, CompanyId, BranchId, userId, Action)
        //        //    .ToListAsync();
        //        return await _dbContext.tbl_validation
        //.FromSqlRaw("EXEC Stpro_PartBill @Id,@json,@CompanyId,@BranchId,@Userid,@Action",
        //Id, item, CompanyId, BranchId, userId, Action)
        //.ToListAsync();
        //    }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<PartBillMaster> partBillMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                // var item = new SqlParameter("@item", JsonConvert.SerializeObject(partBillMasters));
                var item = new SqlParameter("@json", SqlDbType.NVarChar, -1)
                {
                    Value = JsonConvert.SerializeObject(partBillMasters)
                };
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                //var updateStatus = await _dbContext.tbl_validation.FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                var updateStatus = await _dbContext.tbl_validation.FromSqlRaw("EXEC Stpro_PartBill  @Id = @Id, @json = @json, @CompanyId = @CompanyId, @BranchId = @BranchId, @Userid = @Userid, @Action = @Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return updateStatus;
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

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PartBillMaster>> GetbyID(int Idworkorder)
        {
            try
            {

                var list = await _dbContext.tbl_PartBillMaster.Where(x => x.Id == Idworkorder).ToListAsync();
                var detaillist = await _dbContext.tbl_PartBillDetails.Where(x => x.PartBillMasterId == Idworkorder).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<PartBillMaster>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_PartBillMaster.ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_PartBillDetails.Where(x => x.PartBillMasterId == detail.Id).ToListAsync();
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PartBillMaster>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
                {
                    var list = await _dbContext.tbl_PartBillMaster.Where(x => x.CompanyId == companid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_PartBillDetails.Where(x => x.PartBillMasterId == detail.Id).ToListAsync();
                    }
                    return list;
                }
                else
                {
                    var list = await _dbContext.tbl_PartBillMaster.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).ToListAsync();
                    foreach (var detail in list)
                    {
                        var detaillist = await _dbContext.tbl_PartBillDetails.Where(x => x.PartBillMasterId == detail.Id).ToListAsync();
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

        public async Task<IEnumerable<PartBillList>> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserID);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);

                var _product = await _dbContext.tbl_PartBillMasterList.FromSqlRaw("Stpro_PartBillForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<PartBillList>> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_PartBillMasterList.FromSqlRaw("Stpro_PartBillForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<PartBillList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_PartBillMasterList.FromSqlRaw("Stpro_PartBillForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetbyInvoicedItem(int companyId, int branchId, int projectId, int userId, int divisionId, int financialYearId, int Id)
        {
            try
            {
                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.Stpro_PartBillWithDO";
                cmd.CommandType = CommandType.StoredProcedure;
                var division = JsonConvert.SerializeObject(new
                {
                    divisionId
                });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = division });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });

                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<Dictionary<string, object>>();

                if (await reader.ReadAsync())
                {
                    return reader[0]?.ToString() ?? "{}";
                }

                return "{}";

                //while (await reader.ReadAsync())
                //{
                //    var row = new Dictionary<string, object>();

                //    for (int i = 0; i < reader.FieldCount; i++)
                //    {
                //        var columnName = reader.GetName(i);
                //        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);

                //        // Try to parse JSON columns (i.e. FOR JSON PATH subqueries)
                //        if (value is string stringValue && IsLikelyJson(stringValue))
                //        {
                //            try
                //            {
                //                row[columnName] = JsonConvert.DeserializeObject(stringValue);
                //            }
                //            catch
                //            {
                //                row[columnName] = stringValue; // fallback if invalid JSON
                //            }
                //        }
                //        else
                //        {
                //            row[columnName] = value;
                //        }
                //    }

                //    result.Add(row);
                //}

                //return JsonConvert.SerializeObject(result, new JsonSerializerSettings
                //{
                //    ContractResolver = new DefaultContractResolver()
                //});
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
        public async Task<IEnumerable<PartBillList>> Getforview(BillSearch billSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", billSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);

                var _product = await _dbContext.tbl_PartBillMasterList.FromSqlRaw("Stpro_PartBillForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReport(BillSearch billSearch)
        {
            try
            {
                //    var Id = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                //var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", billSearch.BranchId );
                //var userId = new SqlParameter("@userId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectforReport );

                //var _product = await _dbContext.tbl_PartBillMasterReport.FromSqlRaw("Stpro_PartBill @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                //return _product;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PartBill";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(billSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = billSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = billSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforReport });
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


        public async Task<string> GetDetailsbyid(int Id)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_PartBillDetails
                                  join b in _dbContext.tbl_SpecificationMaster on a.SpecId equals b.Id

                                  select new
                                  {
                                      partBillDetailsId = a.PartBillDetailsId,
                                      partBillMasterId = a.PartBillMasterId,
                                      scheduleNo = a.ScheduleNo,
                                      specId = a.SpecId,
                                      spec_Id = b.Spec_Id,
                                      specNumber = b.SpecNumber,
                                      specName = b.SpecName,
                                      sacCode = b.SacCode,
                                      specDescription = b.SpecDescription,
                                      partRatePerUnit = a.PartRatePerUnit,
                                      scheduledQty = a.ScheduledQty,
                                      previousQty = a.PreviousQty,
                                      currentQty = a.CurrentQty,
                                      tax = a.Tax,
                                      taxAmount = a.TaxAmount,
                                      currentAmount = a.CurrentAmount,
                                      description = a.Description,
                                      scheduleUniqueId = a.ScheduleUniqueId

                                  }).Where(x => x.partBillMasterId == Id).ToListAsync();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId)
        {
            try
            {


                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PartBillDetails";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = blockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectdaetailsBasedonProject });
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

        public async Task<IEnumerable<PartBillMaster>> GetLastBill(int projectId, int unitId, int blockId, int floorId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var ProjectId = new SqlParameter("@ProjectId", projectId);
                var UnitId = new SqlParameter("@UnitId", unitId);
                var BlockId = new SqlParameter("@BlockId", blockId);
                var FloorId = new SqlParameter("@FloorId", floorId);
                var Action = new SqlParameter("@Action", Actions.lastBill);

                var _product = await _dbContext.tbl_PartBillMaster.FromSqlRaw("Stpro_PartBillDetails @Id,@ProjectId,@UnitId,@BlockId,@FloorId,@Action", Id, ProjectId, UnitId, BlockId, FloorId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId, int id)
        {
            try
            {


                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_PartBillDetails";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectId });
                cmd.Parameters.Add(new SqlParameter("@UnitId", SqlDbType.Int) { Value = unitId });
                cmd.Parameters.Add(new SqlParameter("@BlockId", SqlDbType.Int) { Value = blockId });
                cmd.Parameters.Add(new SqlParameter("@FloorId", SqlDbType.Int) { Value = floorId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectdaetailsBasedonProject });
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

        public async Task<IEnumerable<Validation>> GetByProjectId(int ProjectId)
        {
            try
            {
                var Id = new SqlParameter("@ProjectId", ProjectId);

                var validation = await _dbContext.tbl_validation.FromSqlRaw("stPro_PartBillValidation @ProjectId", Id).ToListAsync();
                return validation;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
