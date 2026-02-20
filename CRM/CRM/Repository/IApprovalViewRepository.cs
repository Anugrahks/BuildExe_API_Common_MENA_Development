using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IApprovalViewRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/

        Task<string> Indent(int Id);
        
        Task<string> PurchaseOrder(int Id);
        Task<string> Consumption(int Id);

        Task<string> HR(int Id, int MenuId);

    }
}
