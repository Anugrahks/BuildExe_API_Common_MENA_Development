using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public interface IAccountTypeRepository
    {
        Task<IEnumerable<account_type_>> Get();
        Task<account_type_ > GetByID(int Id);
    }
}
