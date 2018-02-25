using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ProductRepos : IRepository<Product>
    {
        private LayerContext db;

        public ProductRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product p = db.Products.Find(id);
            if (p != null)
                db.Products.Remove(p);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate);
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public void Update(Product item)
        {
            db.Products.Update(item);
        }
    }
}
