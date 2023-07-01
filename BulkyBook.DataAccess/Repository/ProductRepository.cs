using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var product = _db.Products.FirstOrDefault(a => a.Id == obj.Id);
            if (product != null)
            {
                product.Name = obj.Name;
                product.Description = obj.Description;
                product.Price = obj.Price;
                product.ListPrice = obj.ListPrice;
                product.Price50 = obj.Price;
                product.Price100 = obj.Price;
                product.ISBN = obj.ISBN;
                product.Author = obj.Author;
                product.CategoryId = obj.CategoryId;
                product.CoverTypeId = obj.CoverTypeId;
                if (obj.ImageUrl != null)
                {
                    product.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
