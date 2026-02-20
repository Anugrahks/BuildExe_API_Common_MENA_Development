using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class TermsAndConditionRepository : ITermsAndConditionRepository
    {
        private readonly BasicContext _dbContext;
        public TermsAndConditionRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }


        
        public async Task Post(TermsAndConditons termsAndConditons)
        {
            try
            {
                var termsAndConditionEntity = await _dbContext.tbl_TermsAndConditionMaster.AddAsync(termsAndConditons);
                await _dbContext.SaveChangesAsync();

                //foreach (var item in termsAndConditons.TermsAndCondtionDetails)
                //{
                //    item.TermsAndConditonMasterId = termsAndConditionEntity.Entity.Id;
                //    await _dbContext.tbl_TermsAndConditionDetails.AddAsync(item);
                //}

                //await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }

        
        public async Task<TermsAndConditons> GetTermsAndConditonsById(int Id)
        {
            var result = await _dbContext.tbl_TermsAndConditionMaster
                .Include(tc => tc.TermsAndCondtionDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            return result;
        }



        public async Task<TermsAndConditons> GetTermsAndConditonsByPrintableConfigurationId(int Id)
        {
            var result = await _dbContext.tbl_TermsAndConditionMaster
                .Include(tc => tc.PrintableReportConfiguration)
                .Include(tc => tc.TermsAndCondtionDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }


        public async Task<TermsAndConditons> GetTermsAndConditonsListByPrintableConfigurationId(int Id)
        {
            var result = await _dbContext.tbl_TermsAndConditionMaster
                .Include(tc => tc.TermsAndCondtionDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }


        public async Task DeleteTermsAndCondition(int Id)
        {
            var result = await _dbContext.tbl_TermsAndConditionMaster            
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            var details = await _dbContext.tbl_TermsAndConditionDetails
               .Where(tc => tc.Id == Id).ToListAsync();

            if (result != null)
            {
                _dbContext.tbl_TermsAndConditionDetails.RemoveRange(details);
                _dbContext.tbl_TermsAndConditionMaster.Remove(result);

                await _dbContext.SaveChangesAsync();
            }          
        }



        public async Task<List<TermsAndConditons>> GetTermsAndConditionsListByCompanyIdAndBranchId(int CompanyId, int BranchId)
        {
            return await _dbContext.tbl_TermsAndConditionMaster
                  .Include(tc => tc.TermsAndCondtionDetails)
                  .Include(x => x.PrintableReportConfiguration)
                  .OrderByDescending(x=>x.Id)
                  .Where(tc => tc.BranchId == BranchId && tc.CompanyId == CompanyId).ToListAsync();
        }



        
        public async Task Update(TermsAndConditons termsandconditions)
        {
            try
            {
                var result = await _dbContext.tbl_TermsAndConditionMaster
                   .Include(tc => tc.TermsAndCondtionDetails)
                   .FirstOrDefaultAsync(tc => tc.Id == termsandconditions.Id);
                _dbContext.tbl_TermsAndConditionDetails.RemoveRange(result.TermsAndCondtionDetails);
                await _dbContext.SaveChangesAsync();
                foreach (var termsandconditiondetail in termsandconditions.TermsAndCondtionDetails)
                {
                    termsandconditiondetail.TermsAndConditonMasterId = termsandconditions.Id;

                    await _dbContext.tbl_TermsAndConditionDetails.AddAsync(termsandconditiondetail);
                }


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


        //----------------------------------------------------Dynamic Content---------------------------------------------------------------------------//
        public async Task Post(DynamicContent dynamicContent)
        {
            try
            {
                var termsAndConditionEntity = await _dbContext.tbl_DynamicContentMaster.AddAsync(dynamicContent);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }

        public async Task<DynamicContent> GetdynamicContentById(int Id)
        {
            var result = await _dbContext.tbl_DynamicContentMaster
                .Include(tc => tc.DynamicContentDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            return result;
        }

        public async Task<DynamicContent> GetDynamicContentByPrintableConfigurationId(int Id)
        {
            var result = await _dbContext.tbl_DynamicContentMaster
                .Include(tc => tc.PrintableReportConfiguration)
                .Include(tc => tc.DynamicContentDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }

        public async Task<DynamicContent> GetdynamicContentListById(int Id)
        {
            var result = await _dbContext.tbl_DynamicContentMaster
                .Include(tc => tc.DynamicContentDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }

        public async Task Deletedynamiccontent(int Id)
        {
            var result = await _dbContext.tbl_DynamicContentMaster
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            var details = await _dbContext.tbl_DynamicContentDetails
               .Where(tc => tc.Id == Id).ToListAsync();

            if (result != null)
            {
                _dbContext.tbl_DynamicContentDetails.RemoveRange(details);
                _dbContext.tbl_DynamicContentMaster.Remove(result);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<DynamicContent>> GetDynamicContentListByCompanyIdAndBranchId(int CompanyId, int BranchId)
        {
            return await _dbContext.tbl_DynamicContentMaster
                  .Include(tc => tc.DynamicContentDetails)
                  .Include(x => x.PrintableReportConfiguration)
                  .OrderByDescending(x => x.Id)
                  .Where(tc => tc.BranchId == BranchId && tc.CompanyId == CompanyId).ToListAsync();
        }

        public async Task Update(DynamicContent dynamicContent)
        {
            try
            {
                var result = await _dbContext.tbl_DynamicContentMaster
                   .Include(tc => tc.DynamicContentDetails)
                   .FirstOrDefaultAsync(tc => tc.Id == dynamicContent.Id);
                _dbContext.tbl_DynamicContentDetails.RemoveRange(result.DynamicContentDetails);
                await _dbContext.SaveChangesAsync();
                foreach (var dynamiccontentdetail in dynamicContent.DynamicContentDetails)
                {
                    dynamiccontentdetail.DynamicContentMasterId = dynamicContent.Id;

                    await _dbContext.tbl_DynamicContentDetails.AddAsync(dynamiccontentdetail);
                }


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        //----------------------------------------------------Signature---------------------------------------------------------------------------//
        public async Task PostSignature(Signature dynamicContent)
        {
            try
            {
                var termsAndConditionEntity = await _dbContext.tbl_SignatureMaster.AddAsync(dynamicContent);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }


        }

        public async Task<Signature> GetSignatureById(int Id)
        {
            var result = await _dbContext.tbl_SignatureMaster
                .Include(tc => tc.SignatureDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            return result;
        }

        public async Task<Signature> GetSignatureByPrintableConfigurationId(int Id)
        {
            var result = await _dbContext.tbl_SignatureMaster
                .Include(tc => tc.PrintableReportConfiguration)
                .Include(tc => tc.SignatureDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }

        public async Task<Signature> GetSignatureListById(int Id)
        {
            var result = await _dbContext.tbl_SignatureMaster
                .Include(tc => tc.SignatureDetails)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(tc => tc.PrintableConfigurationId == Id);

            return result;
        }

        public async Task DeleteSignature(int Id)
        {
            var result = await _dbContext.tbl_SignatureMaster
                .FirstOrDefaultAsync(tc => tc.Id == Id);

            var details = await _dbContext.tbl_SignatureDetails
               .Where(tc => tc.Id == Id).ToListAsync();

            if (result != null)
            {
                _dbContext.tbl_SignatureDetails.RemoveRange(details);
                _dbContext.tbl_SignatureMaster.Remove(result);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Signature>> GetSignatureListByCompanyIdAndBranchId(int CompanyId, int BranchId)
        {
            return await _dbContext.tbl_SignatureMaster
                  .Include(tc => tc.SignatureDetails)
                  .Include(x => x.PrintableReportConfiguration)
                  .OrderByDescending(x => x.Id)
                  .Where(tc => tc.BranchId == BranchId && tc.CompanyId == CompanyId).ToListAsync();
        }

        public async Task UpdateSignature(Signature dynamicContent)
        {
            try
            {
                var result = await _dbContext.tbl_SignatureMaster
                   .Include(tc => tc.SignatureDetails)
                   .FirstOrDefaultAsync(tc => tc.Id == dynamicContent.Id);
                _dbContext.tbl_SignatureDetails.RemoveRange(result.SignatureDetails);
                await _dbContext.SaveChangesAsync();
                foreach (var dynamiccontentdetail in dynamicContent.SignatureDetails)
                {
                    dynamiccontentdetail.SignatureMasterId = dynamicContent.Id;

                    await _dbContext.tbl_SignatureDetails.AddAsync(dynamiccontentdetail);
                }


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }


    }
}
