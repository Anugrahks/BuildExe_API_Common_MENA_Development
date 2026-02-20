
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IUniqueIdRepository
    {
        Task<string> GetId(GetUniqueId getUniqueId);

        Task<string> Get(GetUniqueId getUniqueId);

    }
}
