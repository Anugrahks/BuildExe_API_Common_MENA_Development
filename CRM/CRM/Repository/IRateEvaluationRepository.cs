using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IRateEvaluationRepository
    {
        Task<string> Getbyproject(SpecificationFilters specificationFilters);
        Task<IEnumerable<Validation>> Update(IEnumerable<ProjectSpecificationMaster> rateEvaluations ) ;
        Task<IEnumerable<Validation>> GetVal(int projectId, int UnitId, int BlockId, int Floorid, int DivisionId, int EnquiryId);
    }
}
