using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
  public  interface IVoucherRepository
    {
        void Insert(IEnumerable<Voucher > voucher);
        void Update(IEnumerable<Voucher> voucher);
        IEnumerable<Voucher> GetbyID(int Id);
        IEnumerable<Voucher> Get();
        void Delete(int id);
    }
}
