using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class SupplierAdvanceRepository : ISupplierAdvanceRepository
    {
        private readonly MaterialContext _dbContext;

        public SupplierAdvanceRepository(MaterialContext dbContext)
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
            SelectforEdit = 6,
            SelectforApproval = 7,
            SelectforReport = 8
        }
        public async Task<IEnumerable<Validation>> Insert(SupplierAdvance supplierAdvance)
        {
            try
            {
                if (supplierAdvance.ApprovalRemarks == null)
                    supplierAdvance.ApprovalRemarks = "";

                if (supplierAdvance.RejectRemarks == null)
                    supplierAdvance.RejectRemarks = "";

                var Id = new SqlParameter("@Id", "1");
                var PaymentDate = new SqlParameter("@PaymentDate", supplierAdvance.PaymentDate);
                var SupplierId = new SqlParameter("@SupplierId", supplierAdvance.SupplierId);

                var ProjectId = new SqlParameter("@ProjectId", supplierAdvance.ProjectId);
                var UnitId = new SqlParameter("@UnitId", supplierAdvance.UnitId);
                var BlockId = new SqlParameter("@BlockId", supplierAdvance.BlockId);
                var FloorId = new SqlParameter("@FloorId", supplierAdvance.FloorId);


                var PaymentMode = new SqlParameter("@PaymentMode", supplierAdvance.PaymentMode);
                var PaymentBy = new SqlParameter("@PaymentBy", supplierAdvance.PaymentBy);
                var PaymentNo = new SqlParameter("@PaymentNo", supplierAdvance.PaymentNo);

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", supplierAdvance.AdvanceAmount);
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", supplierAdvance.AdvanceRecoveryBalance);
                var CompanyId = new SqlParameter("@CompanyId", supplierAdvance.CompanyId);
                var BranchId = new SqlParameter("@BranchId", supplierAdvance.BranchId);
                var userId = new SqlParameter("@UserId", supplierAdvance.UserId);
                var FinancialYearId = new SqlParameter("@FinancialYearId", supplierAdvance.FinancialYearId);
                var Narration = new SqlParameter("@Narration", supplierAdvance.Narration);
                var WithClear = new SqlParameter("@WithClear", supplierAdvance.WithClear);
                var sitemanagerid = new SqlParameter("@sitemanagerId", supplierAdvance.SitemanagerId);

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", supplierAdvance.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", supplierAdvance.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", supplierAdvance.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", supplierAdvance.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                //var chequeDate = new SqlParameter("@chequeDate", supplierAdvance.chequeDate);
                var chequeDate = new SqlParameter("@chequeDate", supplierAdvance.chequeDate ?? (object)DBNull.Value);

                var Action = new SqlParameter("@Action", Actions.Insert);
                var IsReject = new SqlParameter("@IsReject", "0");

                var SiteLoan = new SqlParameter("@SiteLoan", supplierAdvance.SiteLoan);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", supplierAdvance.SiteLoanAmt);
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", supplierAdvance.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", supplierAdvance.RejectRemarks);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(supplierAdvance));

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject,@SiteLoan, @SiteLoanAmt,@ApprovalRemarks,@RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> getvalidation(SupplierAdvance supplieradvance)
        {
            try
            {
                var CompanyId = new SqlParameter("@CompanyId", supplieradvance.CompanyId);
                var BranchId = new SqlParameter("@BranchId", supplieradvance.BranchId);
                var FinYearId = new SqlParameter("@FinancialYearId", supplieradvance.FinancialYearId);
                var SiteManagerId = new SqlParameter("@EmployeeId", supplieradvance.SitemanagerId);
                var Amount = new SqlParameter("@Advance", supplieradvance.AdvanceAmount);

                var _product = await _dbContext.tbl_validation.FromSqlRaw("Stpro_SupplierAdvanceValidation  @CompanyId,@BranchId,@FinancialYearId,@EmployeeId, @Advance", CompanyId, BranchId, FinYearId, SiteManagerId, Amount).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(SupplierAdvance supplierAdvance)
        {
            try
            {
                if (supplierAdvance.ApprovalRemarks == null)
                    supplierAdvance.ApprovalRemarks = "";

                if (supplierAdvance.RejectRemarks == null)
                    supplierAdvance.RejectRemarks = "";


                var Id = new SqlParameter("@Id", supplierAdvance.Id);
                var PaymentDate = new SqlParameter("@PaymentDate", supplierAdvance.PaymentDate);
                var SupplierId = new SqlParameter("@SupplierId", supplierAdvance.SupplierId);

                var ProjectId = new SqlParameter("@ProjectId", supplierAdvance.ProjectId);
                var UnitId = new SqlParameter("@UnitId", supplierAdvance.UnitId);
                var BlockId = new SqlParameter("@BlockId", supplierAdvance.BlockId);
                var FloorId = new SqlParameter("@FloorId", supplierAdvance.FloorId);

                var PaymentMode = new SqlParameter("@PaymentMode", supplierAdvance.PaymentMode);
                var PaymentBy = new SqlParameter("@PaymentBy", supplierAdvance.PaymentBy);
                var PaymentNo = new SqlParameter("@PaymentNo", supplierAdvance.PaymentNo);

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", supplierAdvance.AdvanceAmount);
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", supplierAdvance.AdvanceRecoveryBalance);
                var CompanyId = new SqlParameter("@CompanyId", supplierAdvance.CompanyId);
                var BranchId = new SqlParameter("@BranchId", supplierAdvance.BranchId);
                var userId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", supplierAdvance.FinancialYearId);
                var Narration = new SqlParameter("@Narration", supplierAdvance.Narration);
                var WithClear = new SqlParameter("@WithClear", supplierAdvance.WithClear);
                var sitemanagerid = new SqlParameter("@sitemanagerId", supplierAdvance.SitemanagerId);

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", supplierAdvance.ApprovalStatus);
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", supplierAdvance.ApprovalLevel);
                var ApprovedBy = new SqlParameter("@ApprovedBy", supplierAdvance.ApprovedBy);
                var ApprovedDate = new SqlParameter("@ApprovedDate", supplierAdvance.ApprovedDate);
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", supplierAdvance.VoucherTypeId);
                var VoucherNumber = new SqlParameter("@VoucherNumber", supplierAdvance.VoucherNumber);
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                // var chequeDate = new SqlParameter("@chequeDate",supplierAdvance.chequeDate);
                var chequeDate = new SqlParameter("@chequeDate", supplierAdvance.chequeDate ?? (object)DBNull.Value);
                var Action = new SqlParameter("@Action", Actions.Update);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", supplierAdvance.SiteLoan);
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", supplierAdvance.SiteLoanAmt);
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", supplierAdvance.ApprovalRemarks);
                var RejectRemarks = new SqlParameter("@RejectRemarks", supplierAdvance.RejectRemarks);
                var json = new SqlParameter("@json", JsonConvert.SerializeObject(supplierAdvance));

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierAdvance>> Get(int Companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "1");

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");


                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.SelectAll);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");
                var json = new SqlParameter("@json", "");


                var _product = await _dbContext.tbl_SupplierAdvanceMaster.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SupplierAdvance>> GetByID(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");

                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.Select);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");

                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_SupplierAdvanceMaster.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int id, int Userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");

                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var userId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", Userid);
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.Delete);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");

                var json = new SqlParameter("@json", "");

                return await _dbContext.tbl_validations.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierAdvanceList>> GetForEdit(int Companyid, int Branchid)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");


                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@UserId", "0");
                var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");

                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_SupplierAdvanceMasterList.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SupplierAdvanceList>> GetForEdituser(int Companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");


                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var userId = new SqlParameter("@UserId", UserId);
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.SelectforEdit);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");

                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_SupplierAdvanceMasterList.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan, @SiteLoanAmt, @ApprovalRemarks, @RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, financialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, userId, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierAdvanceList>> GetForApproval(int Companyid, int Branchid, int userId, int FinancialYearId)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");

                var PaymentDate = new SqlParameter("@PaymentDate", "");
                var SupplierId = new SqlParameter("@SupplierId", "0");

                var ProjectId = new SqlParameter("@ProjectId", "0");
                var UnitId = new SqlParameter("@UnitId", "0");
                var BlockId = new SqlParameter("@BlockId", "0");
                var FloorId = new SqlParameter("@FloorId", "0");


                var PaymentMode = new SqlParameter("@PaymentMode", "0");
                var PaymentBy = new SqlParameter("@PaymentBy", "0");
                var PaymentNo = new SqlParameter("@PaymentNo", "0");

                var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
                var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
                var CompanyId = new SqlParameter("@CompanyId", Companyid);
                var BranchId = new SqlParameter("@BranchId", Branchid);
                var Userid = new SqlParameter("@UserId", "0");
                var financialYearId = new SqlParameter("@FinancialYearId", FinancialYearId);
                var Narration = new SqlParameter("@Narration", "0");
                var WithClear = new SqlParameter("@WithClear", "0");
                var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

                var ApprovalStatus = new SqlParameter("@ApprovalStatus", "0");
                var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
                var ApprovedBy = new SqlParameter("@ApprovedBy", userId);
                var ApprovedDate = new SqlParameter("@ApprovedDate", "");
                var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
                var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
                var IsDeleted = new SqlParameter("@IsDeleted", "0");
                var chequeDate = new SqlParameter("@chequeDate", "");
                var Action = new SqlParameter("@Action", Actions.SelectforApproval);
                var IsReject = new SqlParameter("@IsReject", "0");
                var SiteLoan = new SqlParameter("@SiteLoan", "0");
                var SiteLoanAmt = new SqlParameter("@SiteLoanAmt", "0");
                var ApprovalRemarks = new SqlParameter("@ApprovalRemarks", "");
                var RejectRemarks = new SqlParameter("@RejectRemarks", "");

                var json = new SqlParameter("@json", "");

                var _product = await _dbContext.tbl_SupplierAdvanceMasterList.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action, @UserId, @IsReject, @SiteLoan,@SiteLoanAmt,@ApprovalRemarks,@RejectRemarks,@json", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, financialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action, Userid, IsReject, SiteLoan, SiteLoanAmt, ApprovalRemarks, RejectRemarks, json).ToListAsync();

                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetforReport(MaterialSearch materialSearch)
        {
            try
            {
                int supplierId = 0, projectid = 0, approvalstatus = 10;
                string Frompaymentdate = null, Topaymentdate = null;
                if (materialSearch.SupplierId != null)
                    supplierId = Convert.ToInt32(materialSearch.SupplierId);
                if (materialSearch.ProjectId != null)
                    projectid = Convert.ToInt32(materialSearch.ProjectId);
                if (materialSearch.ApprovalStatus != null)
                    approvalstatus = Convert.ToInt32(materialSearch.ApprovalStatus);
                if (materialSearch.FromDate != null)
                    Frompaymentdate = Convert.ToDateTime(materialSearch.FromDate).ToString();
                if (materialSearch.ToDate != null)
                    Topaymentdate = Convert.ToDateTime(materialSearch.ToDate).ToString();

                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.StPro_SupplierAdvanceReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(materialSearch) });
                //cmd.Parameters.Add(new SqlParameter("@PaymentDate", SqlDbType.Date) { Value = Frompaymentdate });
                //cmd.Parameters.Add(new SqlParameter("@SupplierId", SqlDbType.Int) { Value = supplierId });
                //cmd.Parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = projectid });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = materialSearch.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = materialSearch.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = materialSearch.FinancialYearId });
                //cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", SqlDbType.Int) { Value = approvalstatus });
                //cmd.Parameters.Add(new SqlParameter("@ApprovedDate", SqlDbType.Date) { Value = Topaymentdate });
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

            //try
            //{


            //    //if (materialSearch.FromDate != null)

            //    //Frompaymentdate = materialSearch.FromDate.ToString();
            //    //if (materialSearch.ToDate != null)
            //    // Topaymentdate = materialSearch.ToDate.ToString();


            //    var Id = new SqlParameter("@Id", "0");
            //    var PaymentDate = new SqlParameter();
            //    if (materialSearch.FromDate != null)
            //        PaymentDate = new SqlParameter("@PaymentDate", materialSearch.FromDate);
            //    else
            //        PaymentDate = new SqlParameter("@PaymentDate", "");


            //    var SupplierId = new SqlParameter("@SupplierId", supplierId);

            //    var ProjectId = new SqlParameter("@ProjectId", projectid);
            //    var UnitId = new SqlParameter("@UnitId", "0");
            //    var BlockId = new SqlParameter("@BlockId", "0");
            //    var FloorId = new SqlParameter("@FloorId", "0");


            //    var PaymentMode = new SqlParameter("@PaymentMode", "0");
            //    var PaymentBy = new SqlParameter("@PaymentBy", "0");
            //    var PaymentNo = new SqlParameter("@PaymentNo", "0");

            //    var AdvanceAmount = new SqlParameter("@AdvanceAmount", "0");
            //    var AdvanceRecoveryBalance = new SqlParameter("@AdvanceRecoveryBalance", "0");
            //    var CompanyId = new SqlParameter("@CompanyId", materialSearch.CompanyId);
            //    var BranchId = new SqlParameter("@BranchId", materialSearch.BranchId);
            //    var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            //    var Narration = new SqlParameter("@Narration", "0");
            //    var WithClear = new SqlParameter("@WithClear", "0");
            //    var sitemanagerid = new SqlParameter("@sitemanagerId", "0");

            //    var ApprovalStatus = new SqlParameter("@ApprovalStatus", approvalstatus);
            //    var ApprovalLevel = new SqlParameter("@ApprovalLevel", "0");
            //    var ApprovedBy = new SqlParameter("@ApprovedBy", "0");
            //    var ApprovedDate = new SqlParameter();

            //    if (materialSearch.ToDate != null)
            //        ApprovedDate = new SqlParameter("@ApprovedDate", materialSearch.ToDate);
            //    else
            //        ApprovedDate = new SqlParameter("@ApprovedDate", "");

            //    var VoucherTypeId = new SqlParameter("@VoucherTypeId", "0");
            //    var VoucherNumber = new SqlParameter("@VoucherNumber", "0");
            //    var IsDeleted = new SqlParameter("@IsDeleted", "0");
            //    var chequeDate = new SqlParameter("@chequeDate", "");
            //    var Action = new SqlParameter("@Action", Actions.SelectforReport);
            //    var _product = await _dbContext.tbl_SupplierAdvanceMasterList.FromSqlRaw("Stpro_SupplierAdvance @Id, @PaymentDate, @SupplierId,@ProjectId,@UnitId,@BlockId,@FloorId, @PaymentMode,@PaymentBy, @PaymentNo, @AdvanceAmount, @AdvanceRecoveryBalance, @CompanyId, @BranchId, @FinancialYearId, @Narration, @WithClear,@sitemanagerId, @ApprovalStatus, @ApprovalLevel,  @ApprovedBy, @ApprovedDate, @VoucherTypeId, @VoucherNumber, @IsDeleted,@chequeDate, @Action", Id, PaymentDate, SupplierId, ProjectId, UnitId, BlockId, FloorId, PaymentMode, PaymentBy, PaymentNo, AdvanceAmount, AdvanceRecoveryBalance, CompanyId, BranchId, FinancialYearId, Narration, WithClear, sitemanagerid, ApprovalStatus, ApprovalLevel, ApprovedBy, ApprovedDate, VoucherTypeId, VoucherNumber, IsDeleted, chequeDate, Action).ToListAsync();

            //    return _product;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
