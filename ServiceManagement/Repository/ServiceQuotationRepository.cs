using BuildExeServiceManagement.DBContexts;
using BuildExeServiceManagement.Models;
using BuildExeServiceManagement.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuildExeServiceManagement.Repository
{
    public class ServiceQuotationRepository : IServiceQuotationRepository
    {
        private readonly ServiceManagementContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectforData = 4,
        }

        public ServiceQuotationRepository(ServiceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<ServiceQuotation> dat)
        {
            try
            {
                var invoiceId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(dat));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);
                var serviceList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_ServiceInvoice @Id, @json, @CompanyId, @BranchId ,@UserId, @FinancialYearId, @Action", invoiceId, item, CompanyId, BranchId, UserId, FinancialYearId, Action).ToListAsync();
                return serviceList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<ServiceQuotation> dat)
        {
            try
            {
                var invoiceId = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@json", JsonConvert.SerializeObject(dat));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);
                var serviceList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_ServiceInvoice @Id, @json, @CompanyId, @BranchId ,@UserId, @FinancialYearId, @Action", invoiceId, item, CompanyId, BranchId, UserId, FinancialYearId, Action).ToListAsync();
                return serviceList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> Delete(int Id, int UserID)
        {
            try
            {
                var invoiceId = new SqlParameter("@Id", Id);
                var item = new SqlParameter("@json", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", UserID);
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var serviceList = await _dbContext.tbl_validations.FromSqlRaw("Stpro_ServiceInvoice @Id, @json, @CompanyId, @BranchId ,@UserId, @FinancialYearId, @Action", invoiceId, item, CompanyId, BranchId, UserId, FinancialYearId,  Action).ToListAsync();
                return serviceList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetData(int CompanyId, int Branchid, int UserId, int FinancialYearId,int CusId)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ServiceInvoice";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = CusId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectforData });
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
        //public async Task<IEnumerable<ServiceQuotation>> GetbyID(int Idworkorder)
        //{
        //    try
        //    {

        //        var list = await _dbContext.tbl_ServiceInvoiceMaster.Where(x => x.InvoiceNo == Idworkorder).ToListAsync();
        //        var detaillist = await _dbContext.tbl_ServiceInvoiceDetails.Where(x => x.InvoiceId == Idworkorder).ToListAsync();
                
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<object> GetbyID(int Idworkorder)
        {
            try
            {
                var list = await _dbContext.tbl_ServiceInvoiceMaster.Where(x => x.InvoiceNo == Idworkorder).ToListAsync();
                var detaillist = await _dbContext.tbl_ServiceInvoiceDetails.Where(x => x.InvoiceId == Idworkorder).ToListAsync();

                var result = new
                {
                    Master = list,
                    Details = detaillist
                };

                return result;
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
