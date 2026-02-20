using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IAccountGroupRepository
    {
        Task<IEnumerable<account_group_>> Get();
        Task<IEnumerable<account_group_>> GetByID(int accounttypeid);
    }
}
