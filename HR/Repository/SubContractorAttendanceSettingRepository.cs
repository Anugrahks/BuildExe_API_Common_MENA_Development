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
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class SubContractorAttendanceSettingRepository:ISubContractorAttendanceSettingRepository 
    {
        private readonly HRContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }

        public SubContractorAttendanceSettingRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorAttendanceSetting > subContractorAttendanceSettings )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorAttendanceSettings));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorAttendanceSetting @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Delete(int Idworkorder,int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", userid);
            var Action = new SqlParameter("@Action", Actions.Delete);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorAttendanceSetting @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorAttendanceSetting> subContractorAttendanceSettings)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(subContractorAttendanceSettings));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SubContractorAttendanceSetting @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<IEnumerable<SubContractorAttendanceSetting>> Get(int companyid,int branchid)
        {
            try
            {
                //List<SubContractorAttendanceSetting> nestedclass = new List<SubContractorAttendanceSetting>();
            if (branchid == 0)
            {
                var list =await _dbContext.tbl_SubContractorAttendanceSettingMaster.Where(x => x.CompanyId == companyid ).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist =await _dbContext.tbl_SubContractorAttendanceSettingDetails.Where(x => x.SubContractorAttendanceSettingId == detail.Id).ToListAsync();
                }

                return list;
            }
            else
            {
                var list = await _dbContext.tbl_SubContractorAttendanceSettingMaster.Where(x => x.CompanyId == companyid).Where(x => x.BranchId  == branchid).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_SubContractorAttendanceSettingDetails.Where(x => x.SubContractorAttendanceSettingId == detail.Id).ToListAsync();
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
        public async Task<IEnumerable<SubContractorAttendanceSetting>> GetbyID(int Idworkorder)
        {
            try
            {
                //List<SubContractorAttendanceSetting> nestedclass = new List<SubContractorAttendanceSetting>();
            var list =await _dbContext.tbl_SubContractorAttendanceSettingMaster.Where(x => x.Id == Idworkorder).ToListAsync();
            var detaillist =await _dbContext.tbl_SubContractorAttendanceSettingDetails.Where(x => x.SubContractorAttendanceSettingId == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SubContractorAttendanceSetting>> Get(int ProjectId, int UnitId, int BlockId, int FloorId, int subconID)
        {
            try
            {
                var list = await _dbContext.tbl_SubContractorAttendanceSettingMaster
                .Where(x => x.ProjectId == ProjectId)
                .Where(x => x.UnitId == UnitId)
                .Where(x => x.BlockId == BlockId)
                .Where(x => x.FloorId == FloorId)
                .Where(x => x.SubContractorId == subconID || x.ContractorId == subconID)
                .ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetOneDetails(int Id)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_SubContractorAttendanceSettingDetails 
                        join b in _dbContext.tbl_LabourWorkRate  on a.LabourWorkId equals b.Id
                        select new
                        {
                            attendanceSettingDetailsId = a.AttendanceSettingDetailsId,
                            subContractorAttendanceSettingId = a.SubContractorAttendanceSettingId,
                            labourWorkId = a.LabourWorkId,
                            labourWorkName = b.LabourWorkName ,
                            workRate = a.WorkRate,
                            oTRate = a.OTRate,
                           
                        }).Where(x => x.subContractorAttendanceSettingId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetdetsilsforBill(int Id)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_SubContractorAttendanceSettingDetails
                        join b in _dbContext.tbl_LabourWorkRate on a.LabourWorkId equals b.Id
                        select new
                        {
                            attendanceSettingDetailsId = a.AttendanceSettingDetailsId,
                            subContractorAttendanceSettingId = a.SubContractorAttendanceSettingId,
                            labourWorkId = a.LabourWorkId,
                            labourWorkName = b.LabourWorkName,
                            wage = a.WorkRate,
                            oTRate = a.OTRate,

                        }).Where(x => x.subContractorAttendanceSettingId == Id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> GetForEdit(int companyId, int branchid)
        {
            try
            {
                var data =await (from a in _dbContext.tbl_SubContractorAttendanceSettingMaster 
                        join b in _dbContext.tbl_EmployeeMaster
                        on (a.SubContractorId == 0 ? a.ContractorId : 0) equals b.Id
                        join w in _dbContext.tbl_WorkType on a.WorkTypeId  equals w.Id 
                        join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                        join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                        from d in ds.DefaultIfEmpty()
                        join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                        from e in es.DefaultIfEmpty()
                        join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                        from f in fs.DefaultIfEmpty()

                        select new
                        {
                            id = a.Id,
                            workName=a.WorkName,
                            dateOrdered = a.DateOrdered ,
                            subContractorId = a.SubContractorId,
                            contractorId = a.ContractorId,
                            fullName = b.FullName ,
                            workTypeId=a.WorkTypeId,
                            workTypeName = w.WorkTypeName ,
                           
                            projectName = c.ProjectName,
                            projectId = a.ProjectId,
                            blockId = a.BlockId,
                            blockName = d == null ? " " : d.BlockName,
                            floorId = a.FloorId,
                            floorName = e == null ? " " : e.FloorName,
                            unitId = a.UnitId,
                            unitName = f == null ? " " : f.UnitId,
                            description=a.Description,
                            companyId=a.CompanyId,
                            branchId=a.BranchId,
                            workStatus=a.WorkStatus
                        }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<string> GetForEdituser(int companyId, int branchid, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_SubContractorAttendanceSettingMaster
                                  join b in _dbContext.tbl_EmployeeMaster
                                   on new { Id = (a.SubContractorId == 0 ? a.ContractorId : a.SubContractorId) }
                                 equals new { Id = b.Id }
                                  join w in _dbContext.tbl_WorkType on a.WorkTypeId equals w.Id
                                  join c in _dbContext.tbl_ProjectMaster on a.ProjectId equals c.id
                                  join d in _dbContext.tbl_Block on a.BlockId equals d.BlockId into ds
                                  from d in ds.DefaultIfEmpty()
                                  join e in _dbContext.tbl_Floors on a.FloorId equals e.FloorId into es
                                  from e in es.DefaultIfEmpty()
                                  join g in _dbContext.tbl_Division on a.DivisionId equals g.DivisionId into gs
                                  from g in gs.DefaultIfEmpty()
                                  join f in _dbContext.tbl_OwnProjectDetails on a.UnitId equals f.Id into fs
                                  from f in fs.DefaultIfEmpty()

                                  select new
                                  {
                                      id = a.Id,
                                      workName = a.WorkName,
                                      dateOrdered = a.DateOrdered,
                                      subContractorId = a.SubContractorId,
                                      fullName = b.FullName,
                                      workTypeId = a.WorkTypeId,
                                      workTypeName = w.WorkTypeName,
                                      contractorId = a.ContractorId,
                                      projectName = c.ProjectName,
                                      projectId = a.ProjectId,
                                      blockId = a.BlockId,
                                      blockName = d == null ? " " : d.BlockName,
                                      floorId = a.FloorId,
                                      floorName = e == null ? " " : e.FloorName,
                                      divisionId = a.DivisionId,
                                      divisionName = g == null ? " " : g.DivisionShortName,
                                      unitId = a.UnitId,
                                      unitName = f == null ? " " : f.UnitId,
                                      description = a.Description,
                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      workStatus = a.WorkStatus,
                                      userId = a.UserId
                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).Where(x => x.userId == UserId).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
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
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSubContractorSettingEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
    }
}
