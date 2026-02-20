using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;
using System.Data.Common;
using System.Data;

namespace BuildExeServices.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ProductContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }

        public TeamRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> InsertTeam(IEnumerable<Team> teams)
        {
            try
            {
                var TeamId = new SqlParameter("@Teamid", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", JsonConvert.SerializeObject(teams));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Team @Teamid,@EntryUserId,@team, @CompanyId, @BranchId, @Action", TeamId, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int enquiryId, int userId)
        {
            try
            {
                var TeamId = new SqlParameter("@Teamid", enquiryId);
                var EntryUserId = new SqlParameter("@EntryUserId", userId);
                var team = new SqlParameter("@team", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Team @Teamid,@EntryUserId,@team, @CompanyId, @BranchId, @Action", TeamId, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<Team> teams)
        {
            try
            {
                var TeamId = new SqlParameter("@Teamid", "0");
                var EntryUserId = new SqlParameter("@EntryUserId", "0");
                var team = new SqlParameter("@team", JsonConvert.SerializeObject(teams));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                var purchaseList = await _dbContext.tbl_validation.FromSqlRaw("Stpro_Team @Teamid,@EntryUserId,@team, @CompanyId, @BranchId, @Action", TeamId, EntryUserId, team, CompanyId, BranchId, Action).ToListAsync();
                return purchaseList;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetTeam()
        {
            try
            {
                // List<Team> nestedclass = new List<Team>();
                var list = await _dbContext.tbl_TeamMaster.ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_TeamDetails.Where(x => x.TeamId == detail.Id).ToListAsync();
                }

                return list;

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        //public async Task<IEnumerable<Team>> GetTeam(int CompanyId, int BranchId)
        //{
        //    try
        //    {
        //        var list = await _dbContext.tbl_TeamMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).OrderByDescending(x => x.Id).ToListAsync();
        //        foreach (var detail in list)
        //        {
        //            var detaillist = await _dbContext.tbl_TeamDetails.Where(x => x.TeamId == detail.Id).ToListAsync();
        //        }

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<Team>> GetTeam(int CompanyId, int BranchId)
        {
            try
            {
                // Fetch the list of teams based on CompanyId and BranchId
                var list = await _dbContext.tbl_TeamMaster
                    .Where(x => x.CompanyId == CompanyId && x.BranchId == BranchId)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                // Populate the TeamDetails for each team
                foreach (var detail in list)
                {
                    detail.TeamDetails = await _dbContext.tbl_TeamDetails
                        .Where(x => x.TeamId == detail.Id)
                        .ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        //public async Task<IEnumerable<Team>> GetTeam(int CompanyId, int BranchId)
        //{
        //    try
        //    {
        //        var list = await _dbContext.tbl_TeamMaster
        //            .Where(x => x.CompanyId == CompanyId && x.BranchId == BranchId)
        //            .Include(x => x.TeamDetails)  // Ensure related TeamDetails are fetched
        //            .OrderByDescending(x => x.Id)
        //            .ToListAsync();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<Team>> GetTeambyID(int teamId)
        {
            try
            {
                //  List<Team> nestedclass = new List<Team>();
                var list = await _dbContext.tbl_TeamMaster.Where(x => x.Id == teamId).ToListAsync();
                var detaillist = await _dbContext.tbl_TeamDetails.Where(x => x.TeamId == teamId).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetTeamuser(int CompanyId, int BranchId, int UserId)
        {
            try
            {
                var list = await _dbContext.tbl_TeamMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_TeamDetails.Where(x => x.TeamId == detail.Id).ToListAsync();
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetUsers(int teamId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_TeamDetails
                                   join c in _dbContext.tbl_Users on a.UserId equals c.Id
                                  select new
                                  {
                                      teamDetailsId = a.TeamDetailsId,
                                      teamId = a.TeamId,
                                      userId = a.UserId,
                                      fullName = c.FullName,


                                  }).Where(x => x.teamId == teamId).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;

            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetUsersStage(int teamid, int EnquiryId, int OrderId, int Status)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = teamid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = EnquiryId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = OrderId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = Status });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 10 });
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

        public async Task<string> GetUsersStageFor(int teamid, int EnquiryFor)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_Division";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = teamid });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = EnquiryFor });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
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

        public async Task<IEnumerable<TeamsUsers>> GetMembers(int compid, int BranId)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", compid);
                var BranchId = new SqlParameter("@BranchId", BranId);

                var userslist = await _dbContext.tbl_TeamUsers.FromSqlRaw("Stpro_TeamUsers @CompanyId, @BranchId", CompanyId, BranchId).ToListAsync();
                return userslist;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}