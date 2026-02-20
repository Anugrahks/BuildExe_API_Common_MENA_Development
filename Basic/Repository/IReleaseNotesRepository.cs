using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IReleaseNotesRepository
    {
        Task<string> Get();
        Task<string> Get(string VersionNumber);

    }

}


