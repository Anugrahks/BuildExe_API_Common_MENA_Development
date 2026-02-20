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
using System.Collections.Specialized;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections;


namespace BuildExeServices.Repository
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly ProductContext _dbContext;
        public enum Actions
        {
            getEnquirylist = 1,
            SearchEnquiry = 2,
            EnquiryReport = 3,
            EnquiryIdValidation = 4,
            Insert = 5,
            Update = 6,
            Delete = 7,
            DeleteMessage = 12,
            GetLocationData = 13

        }
        public EnquiryRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<IEnumerable<Validation>> InsertEnquiry(Enquiry enquiry)
        {
            try
            {
                if (enquiry.gstno == null)
                    enquiry.gstno = "";
                if (enquiry.Mobile2 == null)
                    enquiry.Mobile2 = "";
                if (enquiry.OfficeAddress == null)
                    enquiry.OfficeAddress = "";
                if (enquiry.OfficePhone == null)
                    enquiry.OfficePhone = "";


                //await _dbContext.AddAsync(enquiry);
                //await _dbContext.SaveChangesAsync();
                //if (enquiry.EnquiryId > 0)
                //{

                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = enquiry.EnquiryId;
                //    userLogs.UserId = Convert.ToInt16(enquiry.UserId);
                //    userLogs.FormName = "ENQUIRY";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(1);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}

                var item = new SqlParameter("@item", JsonConvert.SerializeObject(enquiry));
                var companyId = new SqlParameter("@companyId", "0");
                var branchId = new SqlParameter("@branchId", "0");
                var Userid = new SqlParameter("@Userid", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> UpdateEnquiry(Enquiry enquiry)
        {
            try
            {
                //_dbContext.Entry(enquiry).State = EntityState.Modified;
                //await _dbContext.SaveChangesAsync();
                //if (enquiry.EnquiryId > 0)
                //{

                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = enquiry.EnquiryId;
                //    userLogs.UserId = Convert.ToInt16(enquiry.UserId);
                //    userLogs.FormName = "ENQUIRY";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(2);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(enquiry));
                var companyId = new SqlParameter("@companyId", "0");
                var branchId = new SqlParameter("@branchId", "0");
                var Userid = new SqlParameter("@Userid", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> DeleteEnquiry(int enquiryId, int userid)
        {
            try
            {
                //var enquiry = _dbContext.tbl_Enquiry.Find(enquiryId);
                //if (enquiry != null)
                //{
                //    _dbContext.tbl_Enquiry.Remove(enquiry);
                //    await _dbContext.SaveChangesAsync();

                //    UserLogs userLogs = new UserLogs();
                //    userLogs.MasterId = enquiryId;
                //    userLogs.UserId = Convert.ToInt16(userid);
                //    userLogs.FormName = "ENQUIRY";
                //    userLogs.EntryDate = DateTime.Now;
                //    userLogs.Action = Convert.ToInt32(3);
                //    await _dbContext.AddAsync(userLogs);
                //    await _dbContext.SaveChangesAsync();
                //}

                var item = new SqlParameter("@item", "");
                var companyId = new SqlParameter("@companyId", enquiryId);
                var branchId = new SqlParameter("@branchId", "0");
                var Userid = new SqlParameter("@Userid", userid);
                var Action = new SqlParameter("@Action", Actions.Delete);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<EnquiryForMobile>> GetEnquiry()
        {
            try
            {
                return await _dbContext.tbl_Enquiry.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Enquiry>> GetEnquiry(int CompanyId, int BranchId)
        {
            try
            {
                var item = new SqlParameter("@item", "");
                var companyId = new SqlParameter("@companyId", CompanyId);
                var branchId = new SqlParameter("@branchId", BranchId);
                var Userid = new SqlParameter("@Userid", "0");
                var Action = new SqlParameter("@Action", 9);

                var _product = await _dbContext.tbl_Enquiry1.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
            //try
            //{
            //    return await _dbContext.tbl_Enquiry.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).ToListAsync();
            //}
            //catch (Exception)
            //{ throw; }
        }


        public async Task<string> GetEnquiryByProj(int CompanyId, int BranchId)
        {
            try
            {
                // var item = new SqlParameter("@item", JsonConvert.SerializeObject(additionalBills));
                //var item = new SqlParameter("@item", "");
                //var companyId = new SqlParameter("@companyId", CompanyId);
                //var branchId = new SqlParameter("@branchId", BranchId);
                //var Userid = new SqlParameter("@Userid", "0");
                //var Action = new SqlParameter("@Action", 8);

                //var _product = await _dbContext.tbl_Enquiry1.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                //return _product;
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@branchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<EnquiryList>> GetEnquirylist(int CompanyId, int BranchId)
        {
            try
            {
                // var item = new SqlParameter("@item", JsonConvert.SerializeObject(additionalBills));
                var item = new SqlParameter("@item", "");
                var companyId = new SqlParameter("@companyId", CompanyId);
                var branchId = new SqlParameter("@branchId", BranchId);
                var Userid = new SqlParameter("@Userid", "0");
                var Action = new SqlParameter("@Action", Actions.getEnquirylist);

                var _product = await _dbContext.tbl_Enquirylist.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetEnquirybylist(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@branchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 10 });
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
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetEnquirybylistreport(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@branchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
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
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<EnquiryList>> GetEnquirylistuser(int CompanyId, int BranchId, int UserId)
        //{
        //    try
        //    {
        //        // var item = new SqlParameter("@item", JsonConvert.SerializeObject(additionalBills));
        //        var item = new SqlParameter("@item", "");
        //        var companyId = new SqlParameter("@companyId", CompanyId);
        //        var branchId = new SqlParameter("@branchId", BranchId);
        //        var Userid = new SqlParameter("@Userid", UserId);
        //        var Action = new SqlParameter("@Action", Actions.getEnquirylist);

        //        var _product = await _dbContext.tbl_Enquirylist.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }

        //}

        public async Task<string> GetEnquirylistuser(int CompanyId, int BranchId, int UserId, int Page, int PageSize)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                var json = JsonConvert.SerializeObject(new { Page, PageSize });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.getEnquirylist });

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



        public async Task<string> GetEnquirySearch(EnquirySearch enquirySearch)
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(enquirySearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = enquirySearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = enquirySearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = enquirySearch.UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SearchEnquiry });

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
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetEnquiryReport(EnquiryReportSearch enquiryReportSearch)
        {
            try
            {
                using var conn = _dbContext.Database.GetDbConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(enquiryReportSearch) });
                cmd.Parameters.Add(new SqlParameter("@Companyid", SqlDbType.Int) { Value = enquiryReportSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = enquiryReportSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.EnquiryReport });

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                var result = new List<OrderedDictionary>();

                while (await reader.ReadAsync())
                {
                    var row = new OrderedDictionary();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                        row[reader.GetName(i)] = value;
                    }
                    result.Add(row);
                }

                var orderedList = result.Select(row =>
                    row.Cast<DictionaryEntry>().ToDictionary(entry => (string)entry.Key, entry => entry.Value)
                ).ToList();

                return JsonConvert.SerializeObject(orderedList, new JsonSerializerSettings
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



        public async Task<EnquiryForMobile> GetEnquiryByID(int EnquiryId)
        {
            try
            {
                return await _dbContext.tbl_Enquiry.FindAsync(EnquiryId);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<long> GetEnquiryIdValidation(int Id, string Enquiryid, int companyid, int branchid)
        {
            long id = 1;
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.VarChar) { Value = Enquiryid });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyid });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = Id });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.EnquiryIdValidation });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count > 0)
                    id = Convert.ToInt64(dataTable.Rows[0][0].ToString());

            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                id = 1;
            }
            return id;
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id, int type)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var Type = new SqlParameter("@Type", type);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckEquiryRelatedEditDelete @Id, @Type", Id, Type).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
         public async Task ImportEnquiriesFromCsv(string csvFilePath)
        {
            try
            {
                var lines = File.ReadAllLines(csvFilePath).Skip(1); // Skip header row
                foreach (var line in lines)
                {
                    var values = line.Split(',');

                    // Map CSV columns to Enquiry properties
                    var enquiry = new Enquiry
                    {
                        Enquiry_For = values[0],
                        ModeofEnquiryid = await GetOrCreateMasterValueAsync("tbl_enquirymode", "Mode", values[1]),
                        Status = await GetOrCreateMasterValueAsync("tbl_enquirystatus", "Status", values[2]),
                        FirstName = values[3],
                        LastName = values[4],
                        Address = values[5],
                        // Map other columns here as needed
                    };

                    await InsertEnquiry(enquiry);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        private void SetDefaultValues(Enquiry enquiry)
        {
            enquiry.gstno = enquiry.gstno ?? string.Empty;
            enquiry.Mobile2 = enquiry.Mobile2 ?? string.Empty;
            enquiry.OfficeAddress = enquiry.OfficeAddress ?? string.Empty;
            enquiry.OfficePhone = enquiry.OfficePhone ?? string.Empty;
        }

        private async Task<int> GetOrCreateMasterValueAsync(string tableName, string columnName, string value)
        {
            var query = $"SELECT Id FROM {tableName} WHERE {columnName} = @Value";
            var id = await _dbContext.Database.ExecuteSqlRawAsync(query, new SqlParameter("@Value", value));

            if (id == 0)
            {
                var insertQuery = $"INSERT INTO {tableName} ({columnName}) OUTPUT INSERTED.Id VALUES (@Value)";
                id = await _dbContext.Database.ExecuteSqlRawAsync(insertQuery, new SqlParameter("@Value", value));
            }

            return id;
        }

        //public async Task<IEnumerable<Validation>> DeleteMessage(int enquiryId, int userid)
        //{
        //    try
        //    {

        //        var item = new SqlParameter("@item", "");
        //        var companyId = new SqlParameter("@companyId", enquiryId);
        //        var branchId = new SqlParameter("@branchId", "0");
        //        var Userid = new SqlParameter("@Userid", userid);
        //        var Action = new SqlParameter("@Action", Actions.Delete);

        //        var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Enquiry @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

        //        return _product;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<string> DeleteMessage(int enquiryId, int userid)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = enquiryId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userid });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.DeleteMessage });
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

        public async Task<string> GetLocationData()
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.GetLocationData });
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


        public async Task<string> UnqProspectName(int BranchId)
        {


            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Enquiry";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 14 });
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
