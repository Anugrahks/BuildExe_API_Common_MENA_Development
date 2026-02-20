using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using Microsoft.Data.SqlClient;

using Newtonsoft.Json;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json.Serialization;

namespace BuildExeServices.Repository
{
    public class FollowupRepository:IFollowupRepository 
    {
        private readonly ProductContext _dbContext;

        public FollowupRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> DeleteFollowup(int followupId, int userid)
        {
            try
            {
                var json = new SqlParameter("@json", followupId);
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", userid);
                var Action = new SqlParameter("@Action", 6);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Followup @json, @CompanyId, @BranchId, @UserId, @Action", json, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<Followup> GetFollowupByID(int followupId)
        {
            try
            {
                return await _dbContext.tbl_Followup.FindAsync(followupId);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetEnquiryId(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.stPro_EnquiryFor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 5 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string division = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    division = division + dataTable.Rows[i][0].ToString();
                }

                return division;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Followup>> GetFollowup()
        {
            try
            {
                return await _dbContext.tbl_Followup.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFollowupsearch(EnquirySearch enquirySearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Followup";
                cmd.CommandType = CommandType.StoredProcedure;

             
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(enquirySearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = enquirySearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = enquirySearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
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
        public async Task<string> GetFollowup(int CompanyId, int BranchId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Followup";
                cmd.CommandType = CommandType.StoredProcedure;
               
                EnquirySearch enquirySearch = new EnquirySearch();
                enquirySearch.CompanyId = CompanyId;
                enquirySearch.BranchId = BranchId;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(enquirySearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
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
                //var data = await (from a in _dbContext.tbl_Followup
                //                  join b in _dbContext.tbl_Enquiry on a.EnquiryId equals b.EnquiryId
                //                  join c in _dbContext.tbl_Users on a.Attendedstaff equals c.Id
                //                  select new
                //                  {
                //                      followupId = a.FollowupId,
                //                      enquiryId = a.EnquiryId,
                //                      enquiryNo = b.EnquiryNo,
                //                      firstName = b.FirstName,
                //                      lastName = b.LastName,
                //                      mobile = b.Mobile,
                //                      followupdate = a.Followupdate,
                //                      attendedstaff = a.Attendedstaff,
                //                      fullName = c.FullName,
                //                      feedback = a.feedback,
                //                      remarks = a.Remarks,
                //                      nextfollowup = a.nextfollowup,
                //                      status = a.status,
                //                      companyId = b.CompanyId,
                //                      branchId = b.BranchId

                //                  }).Where(x => x.companyId == CompanyId).Where(x => x.branchId == BranchId).ToListAsync();

                //string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                //return jsonString;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFollowupuser(int CompanyId, int BranchId, int UserId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Followup";
                cmd.CommandType = CommandType.StoredProcedure;

                EnquirySearch enquirySearch = new EnquirySearch();
                enquirySearch.CompanyId = CompanyId;
                enquirySearch.BranchId = BranchId;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(enquirySearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
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
                //var data = await (from a in _dbContext.tbl_Followup
                //                  join b in _dbContext.tbl_Enquiry on a.EnquiryId equals b.EnquiryId
                //                  join c in _dbContext.tbl_Users on a.Attendedstaff equals c.Id
                //                  select new
                //                  {
                //                      followupId = a.FollowupId,
                //                      enquiryId = a.EnquiryId,
                //                      enquiryNo = b.EnquiryNo,
                //                      firstName = b.FirstName,
                //                      lastName = b.LastName,
                //                      mobile = b.Mobile,
                //                      followupdate = a.Followupdate,
                //                      attendedstaff = a.Attendedstaff,
                //                      fullName = c.FullName,
                //                      feedback = a.feedback,
                //                      remarks = a.Remarks,
                //                      nextfollowup = a.nextfollowup,
                //                      status = a.status,
                //                      companyId = b.CompanyId,
                //                      branchId = b.BranchId

                //                  }).Where(x => x.companyId == CompanyId).Where(x => x.branchId == BranchId).ToListAsync();

                //string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                //return jsonString;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> InsertFollowup(Followup followup)
        {

            try
            {
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(followup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 1);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Followup @json, @CompanyId, @BranchId, @UserId, @Action", json, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> InsertFollowupBulk(BillSearch followup)
        {

            try
            {
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(followup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 1);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_FollowupBulk @json, @CompanyId, @BranchId, @UserId, @Action", json, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Save()
        {
           
            _dbContext.SaveChanges();

        }

        public async Task<IEnumerable<Validation>> UpdateFollowup(Followup followup)
        {
            try
            {
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(followup));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var UserId = new SqlParameter("@UserId", "0");
                var Action = new SqlParameter("@Action", 2);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_Followup @json, @CompanyId, @BranchId, @UserId, @Action", json, CompanyId, BranchId, UserId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetFollowupForReport(FollowupSearch followupSearch  )
        {
            try
            {
                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Followup";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(followupSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = followupSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = followupSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = "0" });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });

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

        private bool IsLikelyJson(string input)
        {
            input = input?.Trim();
            return !string.IsNullOrEmpty(input) &&
                   ((input.StartsWith("{") && input.EndsWith("}")) ||
                    (input.StartsWith("[") && input.EndsWith("]")));
        }


        public async Task<IEnumerable<FollowUpList>> GetFollowupbyEnquiry(int EnquiryId, int userId)
        {
            try
            {
                var item = new SqlParameter("@item", "");

                var companyId = new SqlParameter("@companyId", EnquiryId);
                var branchId = new SqlParameter("@branchId", "0");
                var Userid = new SqlParameter("@Userid", userId);
                var Action = new SqlParameter("@Action", 4);
                var _product = await _dbContext.tbl_FollowupList.FromSqlRaw("Stpro_Followup @item,@companyId,@branchId,@UserId,@Action ", item, companyId, branchId, Userid, Action).ToListAsync();

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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckFollowUpEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> DeleteFollowupEnquiry(int EnqId
            , int FowId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Followup";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = EnqId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = FowId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 2 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 8 });
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
