using BuildExeBasic.DBContexts;
using BuildExeBasic.Interfaces;
using BuildExeBasic.Models;
using BuildExeBasic.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class BatchRepository : IBatchRepository
    {
        private readonly BasicContext _context;
        private readonly ISitemanagerRepository _sitemanagerRepository;
        public BatchRepository(BasicContext context, ISitemanagerRepository sitemanagerRepository)
        {
            _context = context;
            _sitemanagerRepository = sitemanagerRepository;
        }

        public async Task<string> GenerateBatchNumberAsync(int companyId, int branchid, int sitemanagerId, bool batchvalidate)
        {
            var outputParam = new SqlParameter
            {
                ParameterName = "@BatchNo",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output
            };
            var messageoutputParam = new SqlParameter
            {
                ParameterName = "@ResultMessage",
                SqlDbType = SqlDbType.NVarChar,
                Size = 250,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Stpro_GenerateBatchNumber @CompanyId, @BranchId, @SitemanagerId,@BatchValidate, @BatchNo OUTPUT,@ResultMessage OUTPUT",
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@BranchId", branchid),
                new SqlParameter("@SitemanagerId", sitemanagerId),
                new SqlParameter("@BatchValidate", batchvalidate),
                outputParam, messageoutputParam
            );

            var batchno = outputParam.Value?.ToString();
            var message = messageoutputParam.Value?.ToString();
            return string.IsNullOrEmpty(batchno) ? $" {message}" : $"{batchno}";
        }

        public async Task<Batch> AddBatchAsync(Batch batch)
        {
            try
            {
                batch.BatchNo = await GenerateBatchNumberAsync(batch.CompanyId, batch.BranchId, batch.SitemanagerId, batch.BatchValidate);
                batch.GuId = Guid.NewGuid();

                _context.tbl_Batch.Add(batch);
                await _context.SaveChangesAsync();
                if (batch.BatchValidate)
                {
                    await UpdateBatchRelatedDataAsync(batch);
                }
                return batch;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task UpdateBatchRelatedDataAsync(Batch batch)
        {
            try
            {
                var batchIdParam = new SqlParameter("@batchid", batch.Id);
                var empIdParam = new SqlParameter("@SitemanagerId", batch.SitemanagerId);
                var compIdParam = new SqlParameter("@CompanyId", batch.CompanyId);
                var branchIdParam = new SqlParameter("@BranchId", batch.BranchId);

                // Update Transactions
                await _context.Database.ExecuteSqlRawAsync(@"
                UPDATE tbl_SitemanagersTransactions SET BatchID = @batchid
                WHERE EmployeeId = @SitemanagerId AND CompanyId = @CompanyId AND BranchId = @BranchId;",
                    batchIdParam, empIdParam, compIdParam, branchIdParam
                );

                // Update Expenses
                await _context.Database.ExecuteSqlRawAsync(@"
                UPDATE tbl_SitemanagersExpense SET BatchID = @batchid
                WHERE EmployeeId = @SitemanagerId AND CompanyId = @CompanyId;",
                    batchIdParam, empIdParam, compIdParam
                );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Batch>> GetAllBatchesAsync(int CompanyId, int Branchid)
        {
            try
            {
                return await _context.tbl_Batch
                    .Include(b => b.SiteManager)
                    .Where(b => b.CompanyId == CompanyId && b.BranchId == Branchid)
                    .OrderByDescending(b => b.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BatchDTO> GetBatchByIdAsync(long id)
        {
            var batch = await _context.tbl_Batch
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BatchDTO
                {
                    Id = b.Id,
                    SitemanagerId = b.SitemanagerId,
                    BatchNo = b.BatchNo,
                    Status = b.Status,
                    CloseState = b.CloseState
                })
                .FirstOrDefaultAsync();

            return batch;
        }

        public async Task<bool> UpdateCloseStateAsync(long batchId)
        {
            var batch = await _context.tbl_Batch
                .FirstOrDefaultAsync(b => b.Id == batchId);

            if (batch == null)
                return false;

            
            bool hasPending = await _context.tbl_SitemanagersTransactions
                .AnyAsync(t =>
                    t.EmployeeId == batch.SitemanagerId &&
                    (t.BatchID == null || t.BatchID == 0) &&
                    t.ApprovalStatus == 0 &&
                    t.IsDeleted == 0);

            if (hasPending)
                return false;  
            batch.CloseState = true;
            _context.tbl_Batch.Update(batch);
            await _context.SaveChangesAsync();

            return true;
        }

        // Repository method signature change
        public async Task<List<Batch>> GetBatchBySiteManagerAsync(int siteManagerId, int transactionType)
        {
            var batches = await _context.tbl_Batch
                .Where(b =>
                    b.SitemanagerId == siteManagerId &&
                    (b.CloseState == false || b.CloseState == null))
                .Select(b => new Batch
                {
                    Id = b.Id,
                    BatchNo = b.BatchNo,
                    SitemanagerId = b.SitemanagerId
                })
                .ToListAsync();

            bool hasPending = await _context.tbl_SitemanagersTransactions
                .AnyAsync(t =>
                    t.EmployeeId == siteManagerId &&
                    t.BatchID != null &&
                    t.BatchID != 0 &&
                    t.ApprovalStatus == 0 &&
                    t.IsDeleted == 0 &&
                    t.TransactionType == transactionType);

            foreach (var batch in batches)
            {
                batch.BatchValidate = hasPending;
            }

            return batches;
        }

        public async Task<IEnumerable<Batch>> GetAllBatchesBySiteManagerAsync(int siteManagerId)
        {
            try
            {
                var sql = @"
                    SELECT *
                    FROM tbl_Batch
                    WHERE SitemanagerId = @SiteManagerId
                    ORDER BY BatchNo";

                return await _context.tbl_Batch
                    .FromSqlRaw(sql, new SqlParameter("@SiteManagerId", siteManagerId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}