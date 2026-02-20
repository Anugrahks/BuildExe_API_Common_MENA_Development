using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public interface IPurchaseForPaymentRepository
    {
        Task<string> Get(int SupplierId,int sitemanagerid, int financialyearId);
        Task<string> Getforedit(int SupplierId, int sitemanagerid, int financialyearId,int id);
    }
}
