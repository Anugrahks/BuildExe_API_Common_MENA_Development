using BuildExeHR.DBContexts;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class LabourAdvanceMasterRepository : ILabourAdvanceMasterRepository
    {
        private readonly HRContext _dbContext;
        public LabourAdvanceMasterRepository(HRContext dbContext)
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
            SelectforEdit = 6,
            Selectforapproval = 7,
            SelectReportJson = 8,
            Selectforview = 9,
            SelectReport = 10,

        }
        public async Task<IEnumerable<Validation>> Insert(LabourAdvanceMaster labourAdvanceMaster)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");
                var PaymentType = new SqlParameter("@PaymentType", labourAdvanceMaster.PaymentType);
                var Date = new SqlParameter("@Date", labourAdvanceMaster.Date);
                var ProjectId = new SqlParameter("@ProjectId", labourAdvanceMaster.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", labourAdvanceMaster.DivisionId);
                var UnitId = new SqlParameter("@UnitId", labourAdvanceMaster.UnitId);
                var BlockId = new SqlParameter("@BlockId ", labourAdvanceMaster.BlockId);
                var FloorId = new SqlParameter("@FloorId", labourAdvanceMaster.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", labourAdvanceMaster.CompanyId);
                var BranchId = new SqlParameter("@BranchId", labourAdvanceMaster.BranchId);
                var userid = new SqlParameter("@UserId", labourAdvanceMaster.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", labourAdvanceMaster.FinancialYearId);
                var CategoryId = new SqlParameter("@CategoryId", labourAdvanceMaster.CategoryId);
                var EmployeeId = new SqlParameter("@EmployeeId", labourAdvanceMaster.EmployeeId);
                var AdvanceAmount = new SqlParameter("@AdvanceAmount", labourAdvanceMaster.AdvanceAmount);

                if (labourAdvanceMaster.Remarks == null)
                    labourAdvanceMaster.Remarks = "";
                var Remarks = new SqlParameter("@Remarks", labourAdvanceMaster.Remarks);

                var PaymentMode = new SqlParameter("@PaymentMode", labourAdvanceMaster.PaymentMode);
                var PaymentModeId = new SqlParameter("@PaymentModeId", labourAdvanceMaster.PaymentModeId);
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", labourAdvanceMaster.PaymentModeNo);
                var withClear = new SqlParameter("@WithClear", labourAdvanceMaster.withclear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", labourAdvanceMaster.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", labourAdvanceMaster.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", labourAdvanceMaster.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", labourAdvanceMaster.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", labourAdvanceMaster.VoucherTypeId);
                var VoucherNumber = new SqlParameter("@VoucherNumber", labourAdvanceMaster.VoucherNumber);
                var IsDeleted = new SqlParameter("@IsDeleted", labourAdvanceMaster.IsDeleted);


                if (labourAdvanceMaster.ApprovalRemarks == null)
                    labourAdvanceMaster.ApprovalRemarks = "";

                if (labourAdvanceMaster.RejectRemarks == null)
                    labourAdvanceMaster.RejectRemarks = "";

                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", labourAdvanceMaster.ApprovalRemarks);
                var IsReject = new SqlParameter("@IsReject", labourAdvanceMaster.IsReject);

                if (labourAdvanceMaster.TdsAmount == null)
                    labourAdvanceMaster.TdsAmount = 0;
                if (labourAdvanceMaster.PaidRetension == null)
                    labourAdvanceMaster.PaidRetension = 0;
                if (labourAdvanceMaster.BalanceRetension == null)
                    labourAdvanceMaster.BalanceRetension = 0;

                var TdsAmount = new SqlParameter("@TdsAmount", labourAdvanceMaster.TdsAmount);

                var emiamount = new SqlParameter("@emiamount", labourAdvanceMaster.EmiAmount);

                var chequeDate = new SqlParameter("@chequeDate", labourAdvanceMaster.ChequeDate ?? (object)DBNull.Value);
                var paidRete = new SqlParameter("@PaidRetension", labourAdvanceMaster.PaidRetension);
                var balanceRete = new SqlParameter("@BalanceRetension", labourAdvanceMaster.BalanceRetension);
                var SiteLoan = new SqlParameter("@SiteLoan", labourAdvanceMaster.SiteLoan);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", labourAdvanceMaster.SiteLoanAmt);
                
                var Action = new SqlParameter("@Action", Actions.Insert);
                var RejectRemarks = new SqlParameter("@RejectRemarks", labourAdvanceMaster.RejectRemarks);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(labourAdvanceMaster));

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourAdvance @Id,@PaymentType, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @CategoryId, @EmployeeId, @AdvanceAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@IsReject,@TdsAmount,@emiamount,@chequeDate, @PaidRetension, @BalanceRetension,@SiteLoan,@SiteLoanAmt, @Action, @UserId,@RejectRemarks,@json", Id, PaymentType, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, CategoryId, EmployeeId, AdvanceAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, IsReject, TdsAmount, emiamount, chequeDate, paidRete, balanceRete, SiteLoan, SiteLoanAmt, Action, userid, RejectRemarks, json).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(LabourAdvanceMaster labourAdvanceMaster)
        {
            try
            {
                var Id = new SqlParameter("@Id", labourAdvanceMaster.Id);
                var PaymentType = new SqlParameter("@PaymentType", labourAdvanceMaster.PaymentType);
                var Date = new SqlParameter("@Date", labourAdvanceMaster.Date);
                var ProjectId = new SqlParameter("@ProjectId", labourAdvanceMaster.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", labourAdvanceMaster.DivisionId);
                var UnitId = new SqlParameter("@UnitId", labourAdvanceMaster.UnitId);
                var BlockId = new SqlParameter("@BlockId ", labourAdvanceMaster.BlockId);
                var FloorId = new SqlParameter("@FloorId", labourAdvanceMaster.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", labourAdvanceMaster.CompanyId);
                var BranchId = new SqlParameter("@BranchId", labourAdvanceMaster.BranchId);
                var userid = new SqlParameter("@UserId", labourAdvanceMaster.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", labourAdvanceMaster.FinancialYearId);
                var CategoryId = new SqlParameter("@CategoryId", labourAdvanceMaster.CategoryId);
                var EmployeeId = new SqlParameter("@EmployeeId", labourAdvanceMaster.EmployeeId);
                var AdvanceAmount = new SqlParameter("@AdvanceAmount", labourAdvanceMaster.AdvanceAmount);

                var Remarks = new SqlParameter("@Remarks", labourAdvanceMaster.Remarks);

                var PaymentMode = new SqlParameter("@PaymentMode", labourAdvanceMaster.PaymentMode);
                var PaymentModeId = new SqlParameter("@PaymentModeId", labourAdvanceMaster.PaymentModeId);
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", labourAdvanceMaster.PaymentModeNo);
                var withClear = new SqlParameter("@WithClear", labourAdvanceMaster.withclear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", labourAdvanceMaster.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", labourAdvanceMaster.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", labourAdvanceMaster.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", labourAdvanceMaster.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", labourAdvanceMaster.VoucherTypeId);
                var VoucherNumber = new SqlParameter("@VoucherNumber", labourAdvanceMaster.VoucherNumber);
                var IsDeleted = new SqlParameter("@IsDeleted", labourAdvanceMaster.IsDeleted);
                if (labourAdvanceMaster.ApprovalRemarks == null)
                    labourAdvanceMaster.ApprovalRemarks = "";

                if (labourAdvanceMaster.RejectRemarks == null)
                    labourAdvanceMaster.RejectRemarks = "";


                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", labourAdvanceMaster.ApprovalRemarks);
                var IsReject = new SqlParameter("@IsReject", labourAdvanceMaster.IsReject);

                if (labourAdvanceMaster.TdsAmount == null)
                    labourAdvanceMaster.TdsAmount = 0;
                if (labourAdvanceMaster.PaidRetension == null)
                    labourAdvanceMaster.PaidRetension = 0;
                if (labourAdvanceMaster.BalanceRetension == null)
                    labourAdvanceMaster.BalanceRetension = 0;

                var TdsAmount = new SqlParameter("@TdsAmount", labourAdvanceMaster.TdsAmount);
                var emiamount = new SqlParameter("@emiamount", labourAdvanceMaster.EmiAmount);

                var chequeDate = new SqlParameter("@chequeDate", labourAdvanceMaster.ChequeDate ?? (object)DBNull.Value);
                var paidRete = new SqlParameter("@PaidRetension", labourAdvanceMaster.PaidRetension);
                var balanceRete = new SqlParameter("@BalanceRetension", labourAdvanceMaster.BalanceRetension);
                var SiteLoan = new SqlParameter("@SiteLoan", labourAdvanceMaster.SiteLoan);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", labourAdvanceMaster.SiteLoanAmt);
                var Action = new SqlParameter("@Action", Actions.Update);
                var RejectRemarks = new SqlParameter("@RejectRemarks", labourAdvanceMaster.RejectRemarks);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(labourAdvanceMaster));
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourAdvance @Id,@PaymentType, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @CategoryId, @EmployeeId, @AdvanceAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@IsReject,@TdsAmount,@emiamount,@chequeDate,@PaidRetension, @BalanceRetension,@SiteLoan,@SiteLoanAmt, @Action, @UserId,@RejectRemarks,@json", Id, PaymentType, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, CategoryId, EmployeeId, AdvanceAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, IsReject, TdsAmount, emiamount, chequeDate, paidRete, balanceRete, SiteLoan, SiteLoanAmt, Action, userid, RejectRemarks, json).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LabourAdvanceMaster>> Get(int Companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var PaymentType = new SqlParameter("@PaymentType", "");
                var Date = new SqlParameter("@Date", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId ", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var CategoryId = new SqlParameter("@CategoryId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");

                var Remarks = new SqlParameter("@Remarks", "0");

                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", "0");
                var withClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TdsAmount = new SqlParameter("@TdsAmount", "0");
                var emiamount = new SqlParameter("@emiamount", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var paidRete = new SqlParameter("@PaidRetension", "0");
                var balanceRete = new SqlParameter("@BalanceRetension", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_LaboursAdvanceMaster.FromSqlRaw("Stpro_LabourAdvance @Id,@PaymentType, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @CategoryId, @EmployeeId, @AdvanceAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@IsReject,@TdsAmount,@emiamount,@chequeDate,@PaidRetension,@BalanceRetension,@SiteLoan,@SiteLoanAmt,@Action, @UserId,@RejectRemarks,@json", Id, PaymentType, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, CategoryId, EmployeeId, AdvanceAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, IsReject, TdsAmount, emiamount, chequeDate, paidRete, balanceRete, SiteLoan, SiteLoanAmt, Action, userid, RejectRemarks, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<LabourAdvanceMaster>> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var PaymentType = new SqlParameter("@PaymentType", "");
                var Date = new SqlParameter("@Date", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId ", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var CategoryId = new SqlParameter("@CategoryId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");

                var Remarks = new SqlParameter("@Remarks", "0");

                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", "0");
                var withClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TdsAmount = new SqlParameter("@TdsAmount", "0");
                var emiamount = new SqlParameter("@emiamount", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var paidRete = new SqlParameter("@PaidRetension", "0");
                var balanceRete = new SqlParameter("@BalanceRetension", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var json = new SqlParameter("@json", "");
                var _product = await _dbContext.tbl_LaboursAdvanceMaster.FromSqlRaw("Stpro_LabourAdvance @Id,@PaymentType, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @CategoryId, @EmployeeId, @AdvanceAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@IsReject,@TdsAmount,@emiamount,@chequeDate,@PaidRetension,@BalanceRetension,@SiteLoan,@SiteLoanAmt,@Action, @UserId,@RejectRemarks,@json", Id, PaymentType, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, CategoryId, EmployeeId, AdvanceAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, IsReject, TdsAmount, emiamount, chequeDate, paidRete, balanceRete, SiteLoan, SiteLoanAmt, Action, userid, RejectRemarks, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int id, int UserID)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var PaymentType = new SqlParameter("@PaymentType", "");
                var Date = new SqlParameter("@Date", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId ", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var CategoryId = new SqlParameter("@CategoryId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");

                var Remarks = new SqlParameter("@Remarks", "0");

                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentModeNo = new SqlParameter("@PaymentModeNo", "0");
                var withClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", UserID);
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TdsAmount = new SqlParameter("@TdsAmount", "0");
                var emiamount = new SqlParameter("@emiamount", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var paidRete = new SqlParameter("@PaidRetension", "0");
                var balanceRete = new SqlParameter("@BalanceRetension", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var json = new SqlParameter("@json", "");
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_LabourAdvance @Id,@PaymentType, @Date, @ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @CategoryId, @EmployeeId, @AdvanceAmount, @Remarks,@PaymentMode,@PaymentModeId, @PaymentModeNo, @withclear, @ApprovalStatus, @ApprovalLevel, @ApprovedBy, @ApprovedDate, @VoucherTypeId,@VoucherNumber,@IsDeleted,@ApprovalRemarks,@IsReject,@TdsAmount,@emiamount,@chequeDate,@PaidRetension,@BalanceRetension,@SiteLoan,@SiteLoanAmt,@Action, @UserId,@RejectRemarks,@json", Id, PaymentType, Date, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, CategoryId, EmployeeId, AdvanceAmount, Remarks, PaymentMode, PaymentModeId, PaymentModeNo, withClear, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, ApprovalRemarks, IsReject, TdsAmount, emiamount, chequeDate, paidRete, balanceRete, SiteLoan, SiteLoanAmt, Action, userid, RejectRemarks, json).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourAdvanceMasterList>> GetforEdit(int companyid, int branchid, int Menuid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", "0");
                var menuid = new SqlParameter("@menuid", Menuid);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LaboursAdvanceMasterList.FromSqlRaw("Stpro_LabourAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@menuid,@Action", Id, item, CompanyId, BranchId, userId, menuid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourAdvanceMasterList>> GetforEdit(int companyid, int branchid, int UserId, int Menuid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var menuid = new SqlParameter("@menuid", Menuid);
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var _product = await _dbContext.tbl_LaboursAdvanceMasterList.FromSqlRaw("Stpro_LabourAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@menuid,@Action", Id, item, CompanyId, BranchId, userId, menuid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<LabourAdvanceMasterList>> GetforApproval(int companyid, int branchid, int UserId, int Menuid, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", FinancialYearId);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", companyid);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var userId = new SqlParameter("@userId", UserId);
                var menuid = new SqlParameter("@menuid", Menuid);
                var Action = new SqlParameter("@Action", Actions.Selectforapproval);
                var _product = await _dbContext.tbl_LaboursAdvanceMasterList.FromSqlRaw("Stpro_LabourAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@menuid,@Action", Id, item, CompanyId, BranchId, userId, menuid, Action).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<LabourAdvanceMasterList>> Getforview(HRSearch hRSearch)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
                var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId);
                var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId);
                var userId = new SqlParameter("@userId", "0");
                var menuid = new SqlParameter("@menuid", "0");
                var Action = new SqlParameter("@Action", Actions.Selectforview);
                var _product = await _dbContext.tbl_LaboursAdvanceMasterList.FromSqlRaw("Stpro_LabourAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@menuid,@Action", Id, item, CompanyId, BranchId, userId, menuid, Action).ToListAsync();
                return _product;
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

                cmd.CommandText = "dbo.Stpro_LabourAdvanceForApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@menuid", SqlDbType.Int) { Value = 0 });
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
                return purcasedetails;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

            //try { 
            //var Id = new SqlParameter("@Id", "0");
            //var item = new SqlParameter("@item", JsonConvert.SerializeObject(hRSearch));
            //var CompanyId = new SqlParameter("@CompanyId", hRSearch.CompanyId );
            //var BranchId = new SqlParameter("@BranchId", hRSearch.BranchId );
            //var userId = new SqlParameter("@userId", "0");
            //var menuid = new SqlParameter("@menuid", "0");
            //var Action = new SqlParameter("@Action", Actions.SelectReportJson);
            //var _product =await _dbContext.tbl_LaboursAdvanceMasterList.FromSqlRaw("Stpro_LabourAdvanceForApproval @Id,@item,@CompanyId,@BranchId,@userId,@menuid,@Action", Id, item, CompanyId, BranchId, userId, menuid, Action).ToListAsync();
            //return _product;
            //}
            //catch (Exception)
            //{ throw; }

        }
        public async Task<string> Report(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LabourAdvanceForApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@menuid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SelectReport });
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


        public async Task<string> ForReport(HRSearch hRSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_LabourAdvanceForApproval";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(hRSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = hRSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = hRSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@menuid", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 11 });
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


        public async Task<string> GetAdvanceAdjustment(int BranchId,int ProjectId, int EmployeeCategoryId, int EmployeeId, int SupplierId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_AdvanceAdjustment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ProjectId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = EmployeeCategoryId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = SupplierId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
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
