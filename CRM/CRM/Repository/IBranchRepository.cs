using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
  public  interface IBranchRepository
    {
        IEnumerable<Branch> GetBranch();
        Branch GetBranchByID(int branchId);
        IEnumerable<Branch> GetBranchBycompanyid(int companyid);
        void InsertBranch(Branch branch);
        void DeleteBranch(int branchId);
        void UpdateBranch(Branch branch);
        void Save();
    }
}
