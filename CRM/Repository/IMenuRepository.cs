using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IMenuRepository
    {
        Task  Update(Menu menu );
        Task<IEnumerable<Menu >> GetMenu(int userId);
        Task<IEnumerable<Menu>> GetMenunbymenuid(int menuid);

        Task<IEnumerable<Menu>> getall();
        Task<IEnumerable<Menu>> GetMenuforApprovallevel();
        Task<IEnumerable<Menu>> GetModule();
        Task<IEnumerable<Menu>> GetMenuforReport();

        Task<IEnumerable<Menu>> GetMenuforReprint();
        Task<IEnumerable<Menu>> GetMenuforautomail();
        Task<IEnumerable<Menu>> GetMenuforPrint();
    }
}
