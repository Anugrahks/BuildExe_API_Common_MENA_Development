using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;

namespace BuildExeServices.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ProductContext _dbContext;

        public BranchRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void InsertBranch(Branch branch)
        {
            _dbContext.Add(branch);
            Save();
        }
        public void UpdateBranch(Branch branch)
        {
            _dbContext.Entry(branch).State = EntityState.Modified;
            Save();
        }
        public void DeleteBranch(int branchId)
        {
            var Branch = _dbContext.tbl_Branch.Find(branchId);

            _dbContext.tbl_Branch.Remove(Branch);
            Save();
        }

        public IEnumerable <Branch> GetBranch()
        {
            return _dbContext.tbl_Branch.ToList();
        }
        public Branch GetBranchByID(int branchId)
        {

            return _dbContext.tbl_Branch.Find(branchId);

            
        }
        public IEnumerable<Branch> GetBranchBycompanyid(int companyId)
        {
            var Branch = _dbContext.tbl_Branch.Where(p => p.CompanyId  == companyId);

            return Branch.ToList();


        }

    }
}
