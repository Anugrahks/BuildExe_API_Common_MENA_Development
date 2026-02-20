using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildExeServices.Repository
{
    public class RefundRepository:IRefundRepository 
    {
        private readonly ProductContext _dbContext;

        public RefundRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            statusUpdate = 6,
            SelectForApproval = 7,
            GetRefundAmount = 8,
            GetRefundType = 9
        }
        #region Entity Select
        public async Task<IEnumerable<Refund>> GetByID(int id)
        {
            try
            {
                return await _dbContext.tbl_Refund.Where(x => x.ProjectId == id).ToListAsync ();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Refund>> Get()
        {
            try
            {
                return await _dbContext.tbl_Refund.Where(x => x.IsDeleted == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Refund>> GetByID(int Companyid,int BranchID)
        {
            try
            {

                return await _dbContext.tbl_Refund.Where(x => x.IsDeleted == 0).Where(x => x.CompanyId  == Companyid).Where(x => x.BranchId  == BranchID).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        #endregion

        #region Data Manipulation
        public async Task<IEnumerable<Validation>> Insert(Refund refund)
        {
            try
            {
                if (refund.WithClear == null)
                    refund.WithClear = 0;
                if (refund.ChequeDate == null)
                    refund.ChequeDate = refund.Refunddate;
                if (refund.UnitId == null)
                    refund.UnitId =0;
                if (refund.BlockId == null)
                    refund.BlockId = 0;
                if (refund.FloorId == null)
                    refund.FloorId = 0;
                if (refund.DivisionId == null)
                    refund.DivisionId = 0;
                if (refund.PaymentNo == null)
                    refund.PaymentNo = "";
                if (refund.Narration == null)
                    refund.Narration = "";
                if (refund.performanceguarantee == null)
                    refund.performanceguarantee = 0;

                //var id = new SqlParameter("@id", refund.Id );
                //var ProjectId = new SqlParameter("@ProjectId", refund.ProjectId);
                //var DivisionId = new SqlParameter("@DivisionId", refund.DivisionId);
                //var RefundType = new SqlParameter("@RefundType", refund.RefundType);
                //var Refunddate = new SqlParameter("@Refunddate", refund.Refunddate);
                //var RefundAmount = new SqlParameter("@RefundAmount", refund.RefundAmount);
                //var performanceguarantee = new SqlParameter("@performanceguarantee", refund.performanceguarantee);
                //var Narration = new SqlParameter("@Narration", refund.Narration);
                //var PaymentMode = new SqlParameter("@PaymentMode", refund.PaymentMode);
                //var PaymentModeId = new SqlParameter("@PaymentModeId", refund.PaymentModeId);
                //var PaymentNo = new SqlParameter("@PaymentNo", refund.PaymentNo);
                //var MasterId = new SqlParameter("@MasterId", refund.MasterId);
                //var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                //var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                //var FinancialYearId = new SqlParameter("@FinancialYearId", refund.FinancialYearId);

                //var CompanyId = new SqlParameter("@CompanyId", refund.CompanyId);
                //var BranchId = new SqlParameter("@BranchId", refund.BranchId);
                //var UserId = new SqlParameter("@UserId", refund.UserId);

                //var UnitId = new SqlParameter("@UnitId", refund.UnitId);
                //var BlockId = new SqlParameter("@BlockId", refund.BlockId);
                //var FloorId = new SqlParameter("@FloorId", refund.FloorId);
                //var WithClear = new SqlParameter("@WithClear", refund.WithClear);
                //var ChequeDate = new SqlParameter("@ChequeDate", refund.ChequeDate);

                //var Action = new SqlParameter("@Action", Actions.Insert );

                var Id = new SqlParameter("@Id", refund.Id);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(refund));
                var FinancialYearId = new SqlParameter("@FinancialYearId", refund.FinancialYearId);
                var CompanyId = new SqlParameter("@CompanyId", refund.CompanyId);
                var BranchId = new SqlParameter("@BranchId", refund.BranchId);
                var UserId = new SqlParameter("@UserId", refund.UserId);
                var Action = new SqlParameter("@Action", Actions.Insert);

                //return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Refunding @id, @ProjectId,@DivisionId, @RefundType, @Refunddate, @RefundAmount, @performanceguarantee,@Narration, @PaymentMode, @PaymentModeId, @PaymentNo, @MasterId, @VoucherNumber, @VoucherTypeId, @FinancialYearId, @CompanyId, @BranchId, @UserId,@UnitId,@BlockId,@FloorId,@WithClear,@ChequeDate, @Action", id, ProjectId,DivisionId, RefundType, Refunddate, RefundAmount, performanceguarantee, Narration, PaymentMode, PaymentModeId, PaymentNo, MasterId, VoucherNumber, VoucherTypeId, FinancialYearId, CompanyId, BranchId, UserId, UnitId, BlockId, FloorId, WithClear, ChequeDate, Action).ToListAsync();
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Refunding @Id, @json, @FinancialYearId, @CompanyId, @BranchId, @UserId, @Action", Id, json, FinancialYearId, CompanyId, BranchId, UserId, Action).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Validation>> Delete(int id, int userid)
        {
            try
            {

                //var id = new SqlParameter("@id", Id);
                //var ProjectId = new SqlParameter("@ProjectId", "0");
                //var DivisionId = new SqlParameter("@DivisionId", "0");
                //var RefundType = new SqlParameter("@RefundType", "0");
                //var Refunddate = new SqlParameter("@Refunddate", "2020-01-01");
                //var RefundAmount = new SqlParameter("@RefundAmount", "0");
                //var performanceguarantee = new SqlParameter("@performanceguarantee", "0");
                //var Narration = new SqlParameter("@Narration", "");
                //var PaymentMode = new SqlParameter("@PaymentMode", "0");
                //var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                //var PaymentNo = new SqlParameter("@PaymentNo", "0");
                //var MasterId = new SqlParameter("@MasterId", "0");
                //var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                //var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                //var FinancialYearId = new SqlParameter("@FinancialYearId", "0");

                //var CompanyId = new SqlParameter("@CompanyId", "0");
                //var BranchId = new SqlParameter("@BranchId", "0");
                //var UserId = new SqlParameter("@UserId",userid );




                //var UnitId = new SqlParameter("@UnitId", "0");
                //var BlockId = new SqlParameter("@BlockId", "0");
                //var FloorId = new SqlParameter("@FloorId", "0");
                //var WithClear = new SqlParameter("@WithClear", "0");
                //var ChequeDate = new SqlParameter("@ChequeDate", "2020-01-01");
                var Id = new SqlParameter("@Id", id);
                var json = new SqlParameter("@json", "{}");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Refunding @Id, @json, @FinancialYearId, @CompanyId, @BranchId, @UserId, @Action", Id, json, FinancialYearId, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<Validation>> Update(Refund refund)
        {
            try
            {
                if (refund.WithClear == null)
                    refund.WithClear = 0;
                if (refund.ChequeDate == null)
                    refund.ChequeDate = refund.Refunddate;
                if (refund.UnitId == null)
                    refund.UnitId = 0;
                if (refund.BlockId == null)
                    refund.BlockId = 0;
                if (refund.FloorId == null)
                    refund.FloorId = 0;
                if (refund.DivisionId == null)
                    refund.DivisionId = 0;
                if (refund.PaymentNo == null)
                    refund.PaymentNo = "";
                if (refund.Narration == null)
                    refund.Narration = "";
                if (refund.performanceguarantee == null)
                    refund.performanceguarantee = 0;

                //var id = new SqlParameter("@id", refund.Id);
                //var ProjectId = new SqlParameter("@ProjectId", refund.ProjectId);
                //var DivisionId = new SqlParameter("@DivisionId", refund.DivisionId);
                //var RefundType = new SqlParameter("@RefundType", refund.RefundType);
                //var Refunddate = new SqlParameter("@Refunddate", refund.Refunddate);
                //var RefundAmount = new SqlParameter("@RefundAmount", refund.RefundAmount);
                //var performanceguarantee = new SqlParameter("@performanceguarantee", refund.performanceguarantee);
                //var Narration = new SqlParameter("@Narration", refund.Narration);
                //var PaymentMode = new SqlParameter("@PaymentMode", refund.PaymentMode);
                //var PaymentModeId = new SqlParameter("@PaymentModeId", refund.PaymentModeId);
                //var PaymentNo = new SqlParameter("@PaymentNo", refund.PaymentNo);
                //var MasterId = new SqlParameter("@MasterId", refund.MasterId);
                //var VoucherNumber = new SqlParameter("@VoucherNumber", refund.VoucherNumber);
                //var VoucherTypeId = new SqlParameter("@VoucherTypeId", refund.VoucherTypeId);
                //var FinancialYearId = new SqlParameter("@FinancialYearId", refund.FinancialYearId);

                //var CompanyId = new SqlParameter("@CompanyId", refund.CompanyId);
                //var BranchId = new SqlParameter("@BranchId", refund.BranchId);
                //var UserId = new SqlParameter("@UserId", refund.UserId);

                //var UnitId = new SqlParameter("@UnitId", refund.UnitId);
                //var BlockId = new SqlParameter("@BlockId", refund.BlockId);
                //var FloorId = new SqlParameter("@FloorId", refund.FloorId);
                //var WithClear = new SqlParameter("@WithClear",refund.WithClear);
                //var ChequeDate = new SqlParameter("@ChequeDate", refund.ChequeDate);

                var Id = new SqlParameter("@Id", refund.Id);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(refund));
                var FinancialYearId = new SqlParameter("@FinancialYearId", refund.FinancialYearId);
                var CompanyId = new SqlParameter("@CompanyId", refund.CompanyId);
                var BranchId = new SqlParameter("@BranchId", refund.BranchId);
                var UserId = new SqlParameter("@UserId", refund.UserId);
                var Action = new SqlParameter("@Action", Actions.Update);


                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Refunding @Id, @json, @FinancialYearId, @CompanyId, @BranchId, @UserId, @Action", Id, json, FinancialYearId, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }



        }

        #endregion


        #region Get From Procedure
        public async Task<string> getrefund(int companyId, int branchId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Refunding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectAll });

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

                //var _product = await _dbContext.tbl_RefundList.FromSqlRaw("Stpro_Refunding @Id, @json, @CompanyId, @BranchId, @UserId, @Action", Id, json, CompanyId, BranchId, UserId, Action).ToListAsync();
                //return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        

        public async Task<string> getrefunduser(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Refunding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectAll });

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



        public async Task<string> getforApproval(int companyId, int branchId, int userId, int FinancialYearId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Refunding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.Select });

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


        #endregion

        public async Task<string> GetRefundType(int ProjectId, int RefundType)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Refunding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = RefundType });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetRefundType });

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

        public async Task<string> GetRefundAmount(int ProjectId, int RefundType)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Refunding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "{}" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = RefundType });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetRefundAmount });

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
    }
}
