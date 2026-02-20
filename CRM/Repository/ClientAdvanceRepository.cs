using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BuildExeServices.Repository;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace BuildExeServices.Repository
{
    public class ClientAdvanceRepository:IClientAdvanceRepository 
    {

        private readonly ProductContext _dbContext;
        public ClientAdvanceRepository(ProductContext dbContext)
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
            SelectforReport = 6
        }
        public async Task<IEnumerable<Validation>> Insert(ClientAdvance clientAdvance)
        {
            try
            {
                if (clientAdvance.PaymentDate == null)
                    clientAdvance.PaymentDate = clientAdvance.Date;
                if (clientAdvance.ApprovalRemarks == null)
                    clientAdvance.ApprovalRemarks = "";
                if (clientAdvance.RejectRemarks == null)
                    clientAdvance.RejectRemarks = "";

                var Id = new SqlParameter("@Id", "1");

                var Date = new SqlParameter("@Date", clientAdvance.Date);
                var ProjectId = new SqlParameter("@ProjectId", clientAdvance.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", clientAdvance.DivisionId);
                var UnitId = new SqlParameter("@UnitId", clientAdvance.UnitId);
                var BlockId = new SqlParameter("@BlockId ", clientAdvance.BlockId);
                var FloorId = new SqlParameter("@FloorId", clientAdvance.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", clientAdvance.CompanyId);
                var BranchId = new SqlParameter("@BranchId", clientAdvance.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", clientAdvance.FinancialYearId);

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", clientAdvance.AdvanceAmount);
                var TDSAmount = new SqlParameter("@TDSAmount", clientAdvance.TDSAmount);
                var Remarks = new SqlParameter("@Remarks", clientAdvance.Remarks);

                var PaymentMode = new SqlParameter("@PaymentMode", clientAdvance.PaymentMode);
                var PaymentModeId = new SqlParameter("@PaymentModeId", clientAdvance.PaymentModeId);
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", clientAdvance.PaymentModeNo);
                var withClear = new SqlParameter("@WithClear", clientAdvance.withclear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", clientAdvance.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", clientAdvance.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", clientAdvance.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", clientAdvance.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", clientAdvance.VoucherTypeId);
                var VoucherNumber = new SqlParameter("@VoucherNumber", clientAdvance.VoucherNumber);
                var IsDeleted = new SqlParameter("@IsDeleted", clientAdvance.IsDeleted);


                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", clientAdvance.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", clientAdvance.RejectRemarks);
                var IsReject = new SqlParameter("@IsReject", clientAdvance.IsReject);
                var userid = new SqlParameter("@UserId", clientAdvance.UserId);
                var PaymentDate = new SqlParameter("@PaymentDate", clientAdvance.PaymentDate);
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ClientAdvance @Id, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @AdvanceAmount,@TDSAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@RejectRemarks,@IsReject,@UserId, @PaymentDate,@Action", Id, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, AdvanceAmount, TDSAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, RejectRemarks, IsReject, userid, PaymentDate, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(ClientAdvance clientAdvance)
        {
            try
            {
                if (clientAdvance.PaymentDate == null)
                    clientAdvance.PaymentDate = clientAdvance.Date;
                if (clientAdvance.ApprovalRemarks == null)
                    clientAdvance.ApprovalRemarks = "";
                if (clientAdvance.RejectRemarks == null)
                    clientAdvance.RejectRemarks = "";

                var Id = new SqlParameter("@Id", clientAdvance.Id);

                var Date = new SqlParameter("@Date", clientAdvance.Date);
                var ProjectId = new SqlParameter("@ProjectId", clientAdvance.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", clientAdvance.DivisionId);
                var UnitId = new SqlParameter("@UnitId", clientAdvance.UnitId);
                var BlockId = new SqlParameter("@BlockId ", clientAdvance.BlockId);
                var FloorId = new SqlParameter("@FloorId", clientAdvance.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", clientAdvance.CompanyId);
                var BranchId = new SqlParameter("@BranchId", clientAdvance.BranchId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", clientAdvance.FinancialYearId);

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", clientAdvance.AdvanceAmount);
                var TDSAmount = new SqlParameter("@TDSAmount", clientAdvance.TDSAmount);
                var Remarks = new SqlParameter("@Remarks", clientAdvance.Remarks);

                var PaymentMode = new SqlParameter("@PaymentMode", clientAdvance.PaymentMode);
                var PaymentModeId = new SqlParameter("@PaymentModeId", clientAdvance.PaymentModeId);
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", clientAdvance.PaymentModeNo);
                var withClear = new SqlParameter("@WithClear", clientAdvance.withclear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", clientAdvance.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", clientAdvance.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", clientAdvance.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", clientAdvance.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", clientAdvance.VoucherTypeId);
                var VoucherNumber = new SqlParameter("@VoucherNumber", clientAdvance.VoucherNumber);
                var IsDeleted = new SqlParameter("@IsDeleted", clientAdvance.IsDeleted);
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", clientAdvance.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", clientAdvance.RejectRemarks);
                var IsReject = new SqlParameter("@IsReject", clientAdvance.IsReject);
                var userid = new SqlParameter("@UserId", clientAdvance.UserId);
                var PaymentDate = new SqlParameter("@PaymentDate", clientAdvance.PaymentDate);
                var Action = new SqlParameter("@Action", Actions.Update);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ClientAdvance @Id, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @AdvanceAmount,@TDSAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@RejectRemarks,@IsReject, @UserId, @PaymentDate,@Action", Id, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, AdvanceAmount, TDSAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, RejectRemarks, IsReject,userid, PaymentDate, Action).ToListAsync(); ;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<IEnumerable<Validation>> Delete(int id, int UserID)
        {
            try { 
            var Id = new SqlParameter("@Id", id);

            var Date = new SqlParameter("@Date","");
            var ProjectId = new SqlParameter("@ProjectId", "0");
             var DivisionId = new SqlParameter("@DivisionId", "0");
             var UnitId = new SqlParameter("@UnitId", "0");
            var BlockId = new SqlParameter("@BlockId ", "0");
            var FloorId = new SqlParameter("@FloorId", "0");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");

            var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
            var TDSAmount = new SqlParameter("@TDSAmount", "0");
            var Remarks = new SqlParameter("@Remarks", "");

            var PaymentMode = new SqlParameter("@PaymentMode", "");
            var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
            var PaymentModeNo = new SqlParameter("@PaymentModeNo", "");
            var withClear = new SqlParameter("@WithClear", "0");
            var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
            var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
            var ApprovedBy = new SqlParameter("@ApprovedBy", UserID);
            var ApprovedDate = new SqlParameter("@ApprovedDate", "");
            var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
            var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
            var IsDeleted = new SqlParameter("@IsDeleted","0");
            var ApprovalRemarks = new SqlParameter("@ApprovalRemarks","0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var userid = new SqlParameter("@UserId", "0");
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var Action = new SqlParameter("@Action", Actions.Delete );
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_ClientAdvance @Id, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @AdvanceAmount,@TDSAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@RejectRemarks,@IsReject,@UserId,@PaymentDate,@Action", Id, Date, ProjectId,DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, AdvanceAmount, TDSAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, RejectRemarks, IsReject,userid, PaymentDate, Action).ToListAsync(); 
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ClientAdvance>> GetByID(int id)
        { 
            try { 
            return  await _dbContext.tbl_ClientAdvanceMaster .Where(x => x.Id == id).Where(x => x.IsDeleted == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ClientAdvance >> Get(int CompanyId, int BranchId)
        {
            try { 
            if (BranchId == 0)
                return await  _dbContext.tbl_ClientAdvanceMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.IsDeleted == 0).ToListAsync();
            else
                return await _dbContext.tbl_ClientAdvanceMaster.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).Where(x => x.IsDeleted == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<ClientAdvanceList >> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId)
        {
            try { 
            var Id = new SqlParameter("@Id", FinancialYearId);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", UserID);
            var Action = new SqlParameter("@Action", Actions.Selectforapproval);

            var _product = await  _dbContext.tbl_ClientAdvanceMasterList .FromSqlRaw("Stpro_ClientAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync ();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<ClientAdvanceList>> GetforEdit(int companyId, int branchid)
        {
            try { 
            var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", companyId);
            var BranchId = new SqlParameter("@BranchId", branchid);
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.SelectforEdit);

            var _product = await  _dbContext.tbl_ClientAdvanceMasterList.FromSqlRaw("Stpro_ClientAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<ClientAdvanceList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);

                var _product = await _dbContext.tbl_ClientAdvanceMasterList.FromSqlRaw("Stpro_ClientAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetReport(BillSearch billSearch )
        {
            try {
                //var Id = new SqlParameter("@Id", "0");
                //var item = new SqlParameter("@item", JsonConvert.SerializeObject(billSearch));
                //var CompanyId = new SqlParameter("@CompanyId", billSearch.CompanyId );
                //var BranchId = new SqlParameter("@BranchId", billSearch.BranchId );
                //var userId = new SqlParameter("@userId", "0");
                //var Action = new SqlParameter("@Action", Actions.SelectforReport );

                //var _product = await _dbContext.tbl_ClientAdvanceMasterReport.FromSqlRaw("Stpro_ClientAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
                //return _product;

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_ClientAdvanceForApproval";
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

    }
}
