using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace BuildExeServices.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;


        public enum Actions
        { 
              Insert = 1,
              Update = 2,
              Delete = 3,
              Select = 4
        }
        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            
            _dbContext.Products.Remove(product);
            Save();
        }

        public List<Product> GetProductByID(int productId)
        {
            //List<Product> _product = new List<Product>();
            // return _dbContext.Products.Find(productId);

            var id = new SqlParameter("@id", productId);
            var action = new SqlParameter("@action", Actions.Select);
            var name = new SqlParameter("@name", "");
            var description = new SqlParameter("@Description", "");
            var price = new SqlParameter("@Price", "1");
            var CategoryId = new SqlParameter("@CategoryId", "1");

            // var products = _dbContext.Products.FromSqlRaw("GetProducts @p0, @p1,@p2,@p3,@p4", parameters: new[] { productId, "Gates", "test", "2", "2" });
            var _product = _dbContext.Products.FromSqlRaw("GetProducts @id, @action,@name,@Description,@Price,@CategoryId",  id, action, name, description, price, CategoryId).ToList();
            return _product;
           // return null;
          //  return _dbContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void InsertProduct(Product product)
        {
            //_dbContext.Add(product);
            //Save();
             var id = new SqlParameter("@id", 1);
            var action = new SqlParameter("@action", Actions.Insert );
            var name = new SqlParameter("@name", product.Name);
            var description = new SqlParameter("@Description", product.Description);
            var price = new SqlParameter("@Price", product.Price);
            var CategoryId = new SqlParameter("@CategoryId", product.CategoryId);
            _dbContext.Database.ExecuteSqlRaw("GetProducts @id,@action,@name,@Description,@Price,@CategoryId ",  id, action, name, description, price, CategoryId);
           
       
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            Save();
        }
    }
}
