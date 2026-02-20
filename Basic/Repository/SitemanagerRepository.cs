using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using iText.StyledXmlParser.Node;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BuildExeBasic.Repository
{
    public class SitemanagerRepository : ISitemanagerRepository
    {
        private readonly BasicContext _dbContext;
        public SitemanagerRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5,
            SelectForEdit = 6,
            SelectForApproval = 7,
            SiteManagerBalance = 1
        }
        public async Task<IEnumerable<Validation>> Insert(Sitemanager sitemanager)
        {
            try
            {
                if (sitemanager.ApprovalRemarks == null)
                    sitemanager.ApprovalRemarks = "";

                if (sitemanager.RejectRemarks == null)
                    sitemanager.RejectRemarks = "";

                var id = new SqlParameter("@id", "1");
                var TransactionType = new SqlParameter("@TransactionType", sitemanager.TransactionType);
                var TransactionDate = new SqlParameter("@TransactionDate", sitemanager.TransactionDate);
                var ProjectId = new SqlParameter("@ProjectId", sitemanager.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", sitemanager.DivisionId);
                var UnitId = new SqlParameter("@UnitId", sitemanager.UnitId);
                var BlockId = new SqlParameter("@BlockId", sitemanager.BlockId);
                var FloorId = new SqlParameter("@FloorId", sitemanager.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", sitemanager.CompanyId);
                var BranchId = new SqlParameter("@BranchId", sitemanager.BranchId);
                var Userid = new SqlParameter("@UserId", sitemanager.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", sitemanager.FinancialYearId);
                var EmployeeId = new SqlParameter("@EmployeeId", sitemanager.EmployeeId);
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", sitemanager.TransferedEmployeeId);
                var Narration = new SqlParameter("@Narration", sitemanager.Narration);
                var Category = new SqlParameter("@Category", sitemanager.Category);
                var Amount = new SqlParameter("@Amount", sitemanager.Amount);
                var Action = new SqlParameter("@Action", Actions.Insert);
                var CreditHeadId = new SqlParameter("@CreditHeadId", sitemanager.CreditHeadId);
                var DebitHeadId = new SqlParameter("@DebitHeadId", sitemanager.DebitHeadId);
                var PaymentModeId = new SqlParameter();
                var PaymentMode = new SqlParameter();
                var PaymentNo = new SqlParameter();
                if (sitemanager.TransactionType == 1)
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", sitemanager.PaymentModeId);
                    PaymentMode = new SqlParameter("@PaymentMode", sitemanager.PaymentMode);
                    PaymentNo = new SqlParameter("@PaymentNo", sitemanager.PaymentNo);

                }
                else if (sitemanager.TransactionType == 2)
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", sitemanager.PaymentModeId);
                    PaymentMode = new SqlParameter("@PaymentMode", sitemanager.PaymentMode);
                    PaymentNo = new SqlParameter("@PaymentNo", sitemanager.PaymentNo);

                }
                else
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                    PaymentMode = new SqlParameter("@PaymentMode", "CASH");
                    PaymentNo = new SqlParameter("@PaymentNo", "");

                }


                var WithClear = new SqlParameter("@WithClear", sitemanager.WithClear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", sitemanager.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", sitemanager.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", sitemanager.ApprovedBy);


                var PaymentDate = new SqlParameter("@PaymentDate", sitemanager.paymentDate ?? (object)DBNull.Value);
                var workNameId = new SqlParameter("@workNameId", sitemanager.WorkNameId ?? (object)DBNull.Value);
                var IsReject = new SqlParameter("@IsReject", "0");
                if (sitemanager.TaxArea == null)
                    sitemanager.TaxArea = "";
                if (sitemanager.RoundOff == null)
                    sitemanager.RoundOff = 0;
                var TaxArea = new SqlParameter("@TaxArea", sitemanager.TaxArea);
                var GSTper = new SqlParameter("@GSTper", sitemanager.GSTper);
                var SGST = new SqlParameter("@SGST", sitemanager.SGST);
                var CGST = new SqlParameter("@CGST", sitemanager.CGST);
                var IGST = new SqlParameter("@IGST", sitemanager.IGST);
                var RoundOff = new SqlParameter("@RoundOff", sitemanager.RoundOff);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", sitemanager.SiteLoanAmt);
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", sitemanager.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", sitemanager.RejectRemarks);
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", sitemanager.FundTransferVoucher);
                var BatchID = new SqlParameter("@BatchID", sitemanager.BatchID);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks,@FundTransferVoucher,@BatchID", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject,TaxArea,GSTper,SGST,CGST,IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher,BatchID).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(Sitemanager sitemanager)
        {
            try
            {
                if (sitemanager.ApprovalRemarks == null)
                    sitemanager.ApprovalRemarks = "";

                if (sitemanager.RejectRemarks == null)
                    sitemanager.RejectRemarks = "";

                var id = new SqlParameter("@id", sitemanager.Id);
                var TransactionType = new SqlParameter("@TransactionType", sitemanager.TransactionType);
                var TransactionDate = new SqlParameter("@TransactionDate", sitemanager.TransactionDate);
                var ProjectId = new SqlParameter("@ProjectId", sitemanager.ProjectId);
                var DivisionId = new SqlParameter("@DivisionId", sitemanager.DivisionId);
                var UnitId = new SqlParameter("@UnitId", sitemanager.UnitId);
                var BlockId = new SqlParameter("@BlockId", sitemanager.BlockId);
                var FloorId = new SqlParameter("@FloorId", sitemanager.FloorId);
                var CompanyId = new SqlParameter("@CompanyId", sitemanager.CompanyId);
                var BranchId = new SqlParameter("@BranchId", sitemanager.BranchId);
                var Userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", sitemanager.FinancialYearId);
                var EmployeeId = new SqlParameter("@EmployeeId", sitemanager.EmployeeId);
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", sitemanager.TransferedEmployeeId);
                var Narration = new SqlParameter("@Narration", sitemanager.Narration);
                var Category = new SqlParameter("@Category", sitemanager.Category);
                var Amount = new SqlParameter("@Amount", sitemanager.Amount);
                var Action = new SqlParameter("@Action", Actions.Update);
                var CreditHeadId = new SqlParameter("@CreditHeadId", sitemanager.CreditHeadId);
                var DebitHeadId = new SqlParameter("@DebitHeadId", sitemanager.DebitHeadId);

                var PaymentModeId = new SqlParameter();
                var PaymentMode = new SqlParameter();
                var PaymentNo = new SqlParameter();
                if (sitemanager.TransactionType == 1)
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", sitemanager.PaymentModeId);
                    PaymentMode = new SqlParameter("@PaymentMode", sitemanager.PaymentMode);
                    PaymentNo = new SqlParameter("@PaymentNo", sitemanager.PaymentNo);

                }
                else if (sitemanager.TransactionType == 2)
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", sitemanager.PaymentModeId);
                    PaymentMode = new SqlParameter("@PaymentMode", sitemanager.PaymentMode);
                    PaymentNo = new SqlParameter("@PaymentNo", sitemanager.PaymentNo);

                }
                else
                {

                    PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                    PaymentMode = new SqlParameter("@PaymentMode", "CASH");
                    PaymentNo = new SqlParameter("@PaymentNo", "");

                }


                var WithClear = new SqlParameter("@WithClear", sitemanager.WithClear);
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", sitemanager.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", sitemanager.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", sitemanager.ApprovedBy);

                var PaymentDate = new SqlParameter("@PaymentDate", sitemanager.paymentDate ?? (object)DBNull.Value);
                var workNameId = new SqlParameter("@workNameId", sitemanager.WorkNameId ?? (object)DBNull.Value);
                var IsReject = new SqlParameter("@IsReject", sitemanager.IsReject);
                if (sitemanager.TaxArea == null)
                    sitemanager.TaxArea = "";
                if (sitemanager.RoundOff == null)
                    sitemanager.RoundOff = 0;
                var TaxArea = new SqlParameter("@TaxArea", sitemanager.TaxArea);
                var GSTper = new SqlParameter("@GSTper", sitemanager.GSTper);
                var SGST = new SqlParameter("@SGST", sitemanager.SGST);
                var CGST = new SqlParameter("@CGST", sitemanager.CGST);
                var IGST = new SqlParameter("@IGST", sitemanager.IGST);
                var RoundOff = new SqlParameter("@RoundOff", sitemanager.RoundOff);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", sitemanager.SiteLoanAmt);
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", sitemanager.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", sitemanager.RejectRemarks);
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", sitemanager.FundTransferVoucher);
                var BatchID = new SqlParameter("@BatchID", sitemanager.BatchID);
                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks,@FundTransferVoucher,@BatchID", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher, BatchID).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int ID, int userId)
        {
            try
            {

                var id = new SqlParameter("@id", ID);
                var TransactionType = new SqlParameter("@TransactionType", "0");
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", userId);
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff","0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Sitemanager>> GetByID(int iD)
        {
            try
            {
                var id = new SqlParameter("@id", iD);
                var TransactionType = new SqlParameter("@TransactionType", "0");
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.Select);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff","0" );
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");
                var _product =  await _dbContext.tbl_SitemanagersTransactions.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher", id, TransactionType, TransactionDate, ProjectId,DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher).ToListAsync();


                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Sitemanager>> Get(int companyId, int branchid)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var TransactionType = new SqlParameter("@TransactionType", "0");
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");
                var BatchID = new SqlParameter("@BatchID", "0");

                var _product = await _dbContext.tbl_SitemanagersTransactions.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher,@BatchID", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher,BatchID).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SitemanagerList>> GetForEdit(int companyId, int branchid, int transactionType)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var TransactionType = new SqlParameter("@TransactionType", transactionType);
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.SelectForEdit);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");
                var _product = await _dbContext.tbl_SitemanagersTransactionsList.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, FinancialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SitemanagerList>> GetForEdituser(int companyId, int branchid, int transactionType, int UserId, int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var TransactionType = new SqlParameter("@TransactionType", transactionType);
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", UserId);
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.SelectForEdit);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");
                var BatchID= new SqlParameter("@BatchID", "0");
                var BatchName = new SqlParameter("@BatchName", "0");


                var _product = await _dbContext.tbl_SitemanagersTransactionsList.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher,@BatchID,@BatchName", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, financialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher, BatchID, BatchName).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SitemanagerList>> GetForapproval(int companyId, int branchid, int transactionType, int userid, int FinancialYearId)
        {
            try
            {
                var id = new SqlParameter("@id", "0");
                var TransactionType = new SqlParameter("@TransactionType", transactionType);
                var TransactionDate = new SqlParameter("@TransactionDate", "");
                var ProjectId = new SqlParameter("@ProjectId", "0");
                var DivisionId = new SqlParameter("@DivisionId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");
                var CompanyId = new SqlParameter("@CompanyId", companyId);
                var BranchId = new SqlParameter("@BranchId", branchid);
                var Userid = new SqlParameter("@UserId", "0");
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var EmployeeId = new SqlParameter("@EmployeeId", "0");
                var TransferedEmployeeId = new SqlParameter("@TransferedEmployeeId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var Category = new SqlParameter("@Category", "0");
                var Amount = new SqlParameter("@Amount", "0");
                var Action = new SqlParameter("@Action", Actions.SelectForApproval);
                var CreditHeadId = new SqlParameter("@CreditHeadId", "0");
                var DebitHeadId = new SqlParameter("@DebitHeadId", "0");
                var PaymentModeId = new SqlParameter("@PaymentModeId", "0");
                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var WithClear = new SqlParameter("@WithClear", "0");
                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", userid);
                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var workNameId = new SqlParameter("@workNameId", "0");
                var IsReject = new SqlParameter("@IsReject", "0");
                var TaxArea = new SqlParameter("@TaxArea", "");
                var GSTper = new SqlParameter("@GSTper", "0");
                var SGST = new SqlParameter("@SGST", "0");
                var CGST = new SqlParameter("@CGST", "0");
                var IGST = new SqlParameter("@IGST", "0");
                var RoundOff = new SqlParameter("@RoundOff", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "0");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "0");
                var FundTransferVoucher = new SqlParameter("@FundTransferVoucher", "0");
                var _product = await _dbContext.tbl_SitemanagersTransactionsList.FromSqlRaw("Stpro_SitemanagerTransaction @id,@TransactionType,@TransactionDate,@ProjectId,@DivisionId, @UnitId, @BlockId, @FloorId, @CompanyId, @BranchId, @FinancialYearId, @DebitHeadId , @CreditHeadId, @Amount,@EmployeeId,@TransferedEmployeeId,@Narration,@Category,@WithClear ,@ApprovalStatus,@ApprovalLevel,@ApprovedBy,@PaymentModeId,@PaymentMode,@PaymentNo,@PaymentDate,@workNameId, @Action, @UserId, @IsReject,@TaxArea, @GSTper,@SGST,@CGST,@IGST,@RoundOff,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks, @FundTransferVoucher", id, TransactionType, TransactionDate, ProjectId, DivisionId, UnitId, BlockId, FloorId, CompanyId, BranchId, financialYearId, DebitHeadId, CreditHeadId, Amount, EmployeeId, TransferedEmployeeId, Narration, Category, WithClear, ApprovalStatus, ApprovalLevel, ApprovedBy, PaymentModeId, PaymentMode, PaymentNo, PaymentDate, workNameId, Action, Userid, IsReject, TaxArea, GSTper, SGST, CGST, IGST, RoundOff, SiteLoanAmt, ApprovalRemarks, RejectRemarks, FundTransferVoucher).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public decimal SitemanagerBalance(int sitemanagerId, int FinancialYearID)
        {
            try
            {
                decimal BalanceAmount = 0;
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SitemanagerBalance";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteManagerId", SqlDbType.Int) { Value = sitemanagerId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.VarChar) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearID });

                cmd.Parameters.Add(new SqlParameter("@BalanceAmount", SqlDbType.Decimal) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SiteManagerBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                cmd.ExecuteNonQuery();
                BalanceAmount = (Decimal)cmd.Parameters["@BalanceAmount"].Value;

                return BalanceAmount;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> SitemanagerBalance(int sitemanagerid, int Companyid, int Branchid, int FinancialYearIdt)
        {
            try
            {
                var sitemanagerId = new SqlParameter("@SiteManagerId", sitemanagerid);
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var FinancialYearId = new SqlParameter("@FinancialYearId", FinancialYearIdt);
                var Action = new SqlParameter("@Action", Actions.SiteManagerBalance);
                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerBalance @SiteManagerId,@CompanyId,@BranchId,@FinancialYearId, @Action", sitemanagerId, CompanyId, BranchId, FinancialYearId, Action).ToListAsync();
                return "";
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }

        public async Task<string> SitemanagerBalance_Final(int sitemanagerId, int CompanyId, int BranchId, int FinancialYearIDt)
        {
            string amt = "0";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_SitemanagerBalance";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add(new SqlParameter("@SiteManagerId", SqlDbType.Int) { Value = sitemanagerId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearIDt });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SiteManagerBalance });



                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);

                string balance = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    balance = balance + dataTable.Rows[i][0].ToString();
                }
                balance.Replace("{[", "").Replace("]}", "");
                return balance;

            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);

                amt = "0";
            }
            return amt;

        }
        private DataTable ExecuteQuery(string sqlQuery)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = CommandType.Text;
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }

        public async Task<string> SitemanagerBalance_Finals(int sitemanagerId, int CompanyId, int BranchId, int FinancialYearIDt)
        {
            string BalanceAmount = "";
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.Stpro_SitemanagerBalance";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@SiteManagerId", SqlDbType.Int) { Value = sitemanagerId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = FinancialYearIDt });
                //cmd.Parameters.Add(new SqlParameter("@BalanceAmount", SqlDbType.VarChar) { Direction = ParameterDirection.Output });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = Actions.SiteManagerBalance });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //cmd.ExecuteNonQuery();
                //BalanceAmount = cmd.Parameters["@BalanceAmount"].Value.ToString();

                //return BalanceAmount;
                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }


            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
            }
            return BalanceAmount;
        }


        public async Task<string> GetLedger(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_SiteManagerReportAndLedger";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetReport(BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch.ApprovalStatus == null)
                {
                    basicSearch.ApprovalStatus = 2;
                }

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_SiteManagerReportAndLedger";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = basicSearch.ApprovalStatus });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 2 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }

            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetAdvanceLedger(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_SiteManagerReportAndLedger";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(basicSearch) });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = basicSearch.WithOpening });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 3 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetLoanLedger(BasicSearch basicSearch)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_LoanLedger";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = basicSearch.EmployeeId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = basicSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = basicSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@financialyearId", SqlDbType.Int) { Value = basicSearch.FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@withopening", SqlDbType.Int) { Value = 1 });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = basicSearch.FromDate });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = basicSearch.ToDate });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string det = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    det = det + dataTable.Rows[i][0].ToString();
                }
                return det == string.Empty ? "[]" : det;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        /*--------------------------------------------------------------Site Expense -----------------------------------------------------------------------------*/
        public async Task<IEnumerable<Validation>> InsertSiteExpense(IEnumerable<SiteExpense> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerExpense @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }



        public async Task<IEnumerable<Validation>> UpdateSiteExpense(IEnumerable<SiteExpense> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@userId", "0");
                var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerExpense @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        public async Task<IEnumerable<Validation>> DeleteSiteExpense(int ID, int userId)
        {
            try
            {
                var Id = new SqlParameter("@Id", ID);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userid = new SqlParameter("@userId", userId);
                var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SitemanagerExpense @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userid, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }




        public async Task<string> getusersiteexpense(int CompanyId, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SitemanagerExpense";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 4 });
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



        public async Task<string> getforApprovalsiteexpense(int CompanyId, int Branchid, int userid, int FinancialYearId)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SitemanagerExpense";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = FinancialYearId });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = Branchid });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userid });
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


        public async Task<string> Getbyidsiteexpense(int id)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_SitemanagerExpense";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = "" });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 });
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

    }
}
