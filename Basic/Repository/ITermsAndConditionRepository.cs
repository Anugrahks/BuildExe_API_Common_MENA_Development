using BuildExeBasic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface ITermsAndConditionRepository
    {
        Task Post(TermsAndConditons termsAndConditons);

        Task<TermsAndConditons> GetTermsAndConditonsById(int Id);



        Task<List<TermsAndConditons>> GetTermsAndConditionsListByCompanyIdAndBranchId(int CompanyId, int BranchId);

        Task<TermsAndConditons> GetTermsAndConditonsByPrintableConfigurationId(int Id);

        Task Update(TermsAndConditons termsAndConditons);
        Task<TermsAndConditons> GetTermsAndConditonsListByPrintableConfigurationId(int Id);
        Task DeleteTermsAndCondition(int Id);


 //---------------Dynamic Content-------------------------------------------------------------------------------//

        Task Post(DynamicContent dynamicContent);

        Task<DynamicContent> GetdynamicContentById(int Id);

        Task Update(DynamicContent dynamicContent);
        Task<List<DynamicContent>> GetDynamicContentListByCompanyIdAndBranchId(int CompanyId, int BranchId);
        Task<DynamicContent> GetDynamicContentByPrintableConfigurationId(int Id);
        Task<DynamicContent> GetdynamicContentListById(int Id);
        Task Deletedynamiccontent(int Id);


        //---------------Signature-------------------------------------------------------------------------------//

        Task PostSignature(Signature dynamicContent);

        Task<Signature> GetSignatureById(int Id);

        Task UpdateSignature(Signature dynamicContent);
        Task<List<Signature>> GetSignatureListByCompanyIdAndBranchId(int CompanyId, int BranchId);
        Task<Signature> GetSignatureByPrintableConfigurationId(int Id);
        Task<Signature> GetSignatureListById(int Id);
        Task DeleteSignature(int Id);

    }
}
